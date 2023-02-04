using AutoMapper;
using CarWash.DTOs;
using CarWash.Entities;
using CarWash.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Controllers
{
    [Route("api/services")]
    [ApiController]
    [EnableCors(PolicyName = "AllowAPIRequestIO")]
    public class ServicesController : ControllerBase
    {
        private readonly ILogger<ServicesController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IDataProtector _protector;

        public ServicesController(ILogger<ServicesController> logger, ApplicationDbContext context, IMapper mapper, IDataProtectionProvider protectionProvider)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            _protector = protectionProvider.CreateProtector("value_secret_and_unique");
        }

        /*
         * returns a list of all services and accompanying carwashes with pagination
         */
        [HttpGet()]
        public async Task<ActionResult<List<CarWashServicesEntityDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.Services
                .Include(x => x.CarWashEntityServices).ThenInclude(x => x.CarWashEntity)
                .AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var carwash = await queryable.Paginate(pagination).ToListAsync();
            return mapper.Map<List<CarWashServicesEntityDTO>>(carwash);

        }

        /*
         * returns a list of services owned by user
         */
        [HttpGet("myservices")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<CarWashServiceDTO>>> GetMyServices()
        {
            var checkOwner = context.Services.Select(x => new CarWashService { Id = x.Id, Owner = x.Owner }).ToList();
            var listIds = new List<int>();
            foreach (var check in checkOwner)
            {
                if (_protector.Unprotect(check.Owner) == HttpContext.User.Identity.Name)
                {
                    listIds.Add(check.Id);
                }
            }
            var listMyServices = new List<CarWashService>();
            foreach (var clr in listIds)
            {
                CarWashService cw = context.Services.Where(x => x.Id == clr).Single<CarWashService>();
                listMyServices.Add(cw);
            }

            var serviceMy = mapper.Map<List<CarWashServiceDTO>>(listMyServices);
            return serviceMy;
        }

        /*
         * returns a service with a list of supported carwashes
         */
        [HttpGet("{Id:int}", Name = "getService")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<CarWashServicesEntityDTO>> Get(int Id)
        {
            var checkId = context.Services.Where(x => x.Id.Equals(Id)).ToList();
            if (checkId.Count == 0)
            {
                return BadRequest("You entered wrong ID.");
            }

            var serviceDB = await context.Services.Include(x => x.CarWashEntityServices).ThenInclude(x => x.CarWashEntity).FirstOrDefaultAsync(x => x.Id == Id);

            if (serviceDB == null)
            {
                return NotFound();
            }

            return mapper.Map<CarWashServicesEntityDTO>(serviceDB);

        }

        /*
         * making a new service and adding it to a carwash (including a check if a user/owner of a new service is an owner of carwash)
         */
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] CarWashServiceCreationDTO carWashServiceCreationDTO)
        {
            foreach (var cw in carWashServiceCreationDTO.CarWashEntityId)
            {
                var cwid = context.CarWashes.Select(x => x.Id).ToList();
                for (int i = 0; i < carWashServiceCreationDTO.CarWashEntityId.Count; i++)
                {
                    if (!cwid.Contains(cw))
                    {
                        return BadRequest("Selected carwash doesn't exist.");
                    }
                }

                CarWashEntity cwe = context.CarWashes.Where(x => x.Id == cw).Single<CarWashEntity>();
                if (_protector.Unprotect(cwe.Owner) != HttpContext.User.Identity.Name)
                {
                    return BadRequest("Access denied! You are not an owner of the selected carwash.");
                }
            }

            foreach (var cow in carWashServiceCreationDTO.CarWashEntityId)
            {
                var checkName = context.CarWashEntityServices.Where(x => x.CarWashEntityId == cow).Select(x => x.CarWashServiceId).ToList();
                foreach (var cn in checkName)
                {
                    var checkS = context.Services.Where(x => x.Id == cn).Select(x => x.Name).ToList();

                    foreach (var name in checkS)
                    {
                        if (name.ToLower() == carWashServiceCreationDTO.Name.ToLower())
                        {
                            return BadRequest("Service with the same name already exist at the carwash shop.");
                        }
                    }
                }
            }

            if (carWashServiceCreationDTO.Price == 0)
            {
                return BadRequest("You need to enter a price greater then 0");
            }

            var service = mapper.Map<CarWashService>(carWashServiceCreationDTO);

            service.Owner = _protector.Protect(HttpContext.User.Identity.Name);

            context.Add(service);
            await context.SaveChangesAsync();

            var carwashserviceDTO = mapper.Map<CarWashServiceDTO>(service);
            return new CreatedAtRouteResult("getService", new { carwashserviceDTO.Id }, carwashserviceDTO);
        }

        /*
         * editing service by owner
         */
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(int id, [FromBody] CarWashServiceEditDTO carWashServiceEditDTO)
        {
            var checkId = context.Services.Where(x => x.Id.Equals(id)).ToList();
            if (checkId.Count == 0)
            {
                return BadRequest("You entered wrong ID.");
            }

            var servicesDB = await context.Services.FirstOrDefaultAsync(x => x.Id == id);

            if (_protector.Unprotect(servicesDB.Owner) != HttpContext.User.Identity.Name)
            {

                return BadRequest("Access denied!");
            }

            if (servicesDB == null)
            {
                return NotFound("Carwash can't be found.");
            }

            if (servicesDB.Name != carWashServiceEditDTO.Name)
            {
                var input = context.CarWashEntityServices.Where(x => x.CarWashServiceId == id).Select(x => x.CarWashEntityId).ToList();
                foreach (var cow in input)
                {
                    var checkName = context.CarWashEntityServices.Where(x => x.CarWashEntityId == cow).Select(x => x.CarWashServiceId).ToList();
                    foreach (var cn in checkName)
                    {
                        var checkS = context.Services.Where(x => x.Id == cn).Select(x => x.Name).ToList();

                        foreach (var name in checkS)
                        {
                            if (name.ToLower() == carWashServiceEditDTO.Name.ToLower())
                            {
                                return BadRequest("Service with the same name already exist at the carwash shop.");
                            }
                        }
                    }
                }
            }

            if (carWashServiceEditDTO.Price == 0)
            {
                return BadRequest("You need to enter a price greater then 0.");
            }

            servicesDB = mapper.Map(carWashServiceEditDTO, servicesDB);
            await context.SaveChangesAsync();
            return Ok("Service was successfully edited.");

        }

        /*
         * delete a service by owner
         */
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int id)
        {
            var checkId = context.Services.Where(x => x.Id.Equals(id)).ToList();
            if (checkId.Count == 0)
            {
                return BadRequest("You entered wrong ID.");
            }

            CarWashService cw = context.Services.Where(x => x.Id == id).Single<CarWashService>();

            if (cw == null)
            {
                NotFound();
            }

            if (_protector.Unprotect(cw.Owner) != HttpContext.User.Identity.Name)
            {
                return BadRequest("Access denied!");
            }

            context.Services.Remove(cw);
            context.SaveChanges();
            return Ok("Service was successfully deleted.");
        }
    }
}
