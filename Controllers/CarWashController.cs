using AutoMapper;
using CarWash.DTOs;
using CarWash.Entities;
using CarWash.Helpers;
using CarWash.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace CarWash.Controllers
{
    [Route("api/carwashes")]
    [ApiController]
    [EnableCors(PolicyName = "AllowAPIRequestIO")]
    public class CarWashController : ControllerBase
    {
        private readonly ILogger<CarWashController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorageService;
        private readonly IDataProtector _protector;
        private readonly string containerName = "carwashentity";

        public CarWashController(ILogger<CarWashController> logger, ApplicationDbContext context, IMapper mapper, IFileStorageService fileStorageService,
                                 IDataProtectionProvider protectionProvider)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            this.fileStorageService = fileStorageService;
            _protector = protectionProvider.CreateProtector("value_secret_and_unique");
        }

        /*
         * returns a list of all carwashes and accompanying services with pagination
         */
        [HttpGet]
        public async Task<ActionResult<List<CarWashEntityServicesDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.CarWashes
                .Include(x => x.CarWashEntityServices).ThenInclude(x => x.CarWashService)
                .AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var carwash = await queryable.Paginate(pagination).ToListAsync();
            return mapper.Map<List<CarWashEntityServicesDTO>>(carwash);

        }

        /*
         * filtering by Name,Address,CarWashService,IsCarWashOpen
         */
        [HttpGet("filters")]
        public async Task<ActionResult<List<CarWashDTO>>> Filter([FromQuery] CarWashFilterDTO carWashFilterDTO)
        {
            var carWashQueryable = context.CarWashes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(carWashFilterDTO.Name))
            {
                carWashQueryable = carWashQueryable.Where(x => x.Name.Contains(carWashFilterDTO.Name));
            }

            if (!string.IsNullOrWhiteSpace(carWashFilterDTO.Address))
            {
                carWashQueryable = carWashQueryable.Where(x => x.Address.Contains(carWashFilterDTO.Address));
            }

            if (carWashFilterDTO.CarWashServiceId != 0)
            {
                carWashQueryable = carWashQueryable.Where(x => x.CarWashEntityServices.Select(y => y.CarWashServiceId).Contains(carWashFilterDTO.CarWashServiceId));
            }

            if (carWashFilterDTO.CarWashOpen != null)
            {
                carWashQueryable = carWashQueryable.Where(x => x.CarWashOpen == carWashFilterDTO.CarWashOpen);
            }

            await HttpContext.InsertPaginationParametersInResponse(carWashQueryable, carWashFilterDTO.RecordsPerPage);
            var carwashes = await carWashQueryable.Paginate(carWashFilterDTO.Pagination).ToListAsync();
            return mapper.Map<List<CarWashDTO>>(carwashes);
        }

        /*
         * returns a carwash with a list of offered services
         */
        [HttpGet("{Id:int}", Name = "getCarWash")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<CarWashEntityServicesDTO>> Get(int Id)
        {
            var checkId = context.CarWashes.Where(x => x.Id.Equals(Id)).ToList();
            if (checkId.Count == 0)
            {
                return BadRequest("You entered wrong ID.");
            }

            var carwashDB = await context.CarWashes
                .Include(x => x.CarWashEntityServices).ThenInclude(x => x.CarWashService)
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (carwashDB == null)
            {
                return NotFound();
            }

            return mapper.Map<CarWashEntityServicesDTO>(carwashDB);

        }

        /*
         * returns a list of carwashes owned by user
         */
        [HttpGet("mycarwashes")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<CarWashDTO>>> GetMyCarwashes()
        {
            var checkOwner = context.CarWashes.Select(x => new CarWashEntity { Id = x.Id, Owner = x.Owner }).ToList();
            var listIds = new List<int>();
            foreach (var check in checkOwner)
            {
                if (_protector.Unprotect(check.Owner) == HttpContext.User.Identity.Name)
                {
                    listIds.Add(check.Id);
                }
            }
            var listMyCarwash = new List<CarWashEntity>();
            foreach (var clr in listIds)
            {
                CarWashEntity cw = context.CarWashes.Where(x => x.Id == clr).Single<CarWashEntity>();
                listMyCarwash.Add(cw);
            }

            var carWashMy = mapper.Map<List<CarWashDTO>>(listMyCarwash);
            return carWashMy;
        }

        /*
         * posting new carwash
         */
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] CarWashCreationDTO carWashCreation)
        {
            var carwash = mapper.Map<CarWashEntity>(carWashCreation);

            if (carWashCreation.Picture != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await carWashCreation.Picture.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(carWashCreation.Picture.FileName);
                    carwash.Picture = await fileStorageService.SaveFile(content, extension, containerName, carWashCreation.Picture.ContentType);
                }
            }

            foreach (var cw in carWashCreation.CarWashServiceId)
            {
                var cwid = context.Services.Select(x => x.Id).ToList();
                for (int i = 0; i < carWashCreation.CarWashServiceId.Count; i++)
                {
                    if (!cwid.Contains(cw))
                    {
                        return BadRequest("Selected service doesn't exist.");
                    }
                }

                CarWashService cwe = context.Services.Where(x => x.Id == cw).Single<CarWashService>();
                if (_protector.Unprotect(cwe.Owner) != HttpContext.User.Identity.Name)
                {
                    return BadRequest("Access denied! You are not an owner of selected service.");
                }
            }

            var checkName = context.CarWashes.Select(x => x.Name).ToList();
            foreach (var cn in checkName)
            {
                if (cn.ToLower() == carWashCreation.Name.ToLower())
                {
                    return BadRequest("There is already a carwash with the same name, please choose another one.");
                }
            }
            if (!Regex.Match(carWashCreation.Address, @"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$").Success)
            {
                return BadRequest("Valid address contains only letters and numbers");
            }

            if (carWashCreation.OpeningHours <= 0 || carWashCreation.ClosingHours <= 0 || carWashCreation.ClosingHours <= carWashCreation.OpeningHours)
            {
                return BadRequest("You need to enter valid working hours.");
            }

            carwash.Owner = _protector.Protect(HttpContext.User.Identity.Name);

            context.Add(carwash);
            await context.SaveChangesAsync();

            var carwashDTO = mapper.Map<CarWashDTO>(carwash);

            return new CreatedAtRouteResult("getCarWash", new { carwashDTO.Id }, carwashDTO);
        }

        /*
         * editing a carwash by owner
         */
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(int id, [FromBody] CarWashEditDTO carWashEditDTO)
        {
            var checkId = context.CarWashes.Where(x => x.Id.Equals(id)).ToList();
            if (checkId.Count == 0)
            {
                return BadRequest("You entered wrong ID.");
            }

            var carWashDB = await context.CarWashes.FirstOrDefaultAsync(x => x.Id == id);

            if (_protector.Unprotect(carWashDB.Owner) != HttpContext.User.Identity.Name)
            {
                return BadRequest("Access denied!");
            }

            if (carWashDB == null)
            {
                return NotFound("Carwash can't be found.");
            }

            if (!Regex.Match(carWashEditDTO.Address, @"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$").Success)
            {
                return BadRequest("Valid address contains only letters and numbers");
            }

            if (carWashEditDTO.OpeningHours <= 0 || carWashEditDTO.ClosingHours <= 0 || carWashEditDTO.ClosingHours <= carWashEditDTO.OpeningHours)
            {
                return BadRequest("You need to enter valid working hours.");
            }

            if (carWashDB.Name != carWashEditDTO.Name)
            {
                var checkName = context.CarWashes.Select(x => x.Name).ToList();
                foreach (var cn in checkName)
                {
                    if (cn.ToLower() == carWashEditDTO.Name.ToLower())
                    {
                        return BadRequest("There is already a carwash with the same name, please choose another one.");
                    }
                }
            }

            carWashDB = mapper.Map(carWashEditDTO, carWashDB);
            await context.SaveChangesAsync();
            return Ok("Carwash was successfully edited.");

        }

        /*
        * deleting a carwash by owner
        */
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int id)
        {
            var checkId = context.CarWashes.Where(x => x.Id.Equals(id)).ToList();
            if (checkId.Count == 0)
            {
                return BadRequest("You entered wrong ID.");
            }

            CarWashEntity cw = context.CarWashes.Where(x => x.Id == id).Single<CarWashEntity>();

            if (cw == null)
            {
                return NotFound();
            }

            if (_protector.Unprotect(cw.Owner) != HttpContext.User.Identity.Name)
            {
                return BadRequest("Access denied!");
            }

            context.CarWashes.Remove(cw);
            context.SaveChanges();
            return Ok("Carwash was successfully deleted.");
        }
    }
}
