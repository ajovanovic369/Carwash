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
    [Route("api/earnings")]
    [ApiController]
    [EnableCors(PolicyName = "AllowAPIRequestIO")]
    public class EarningsController : ControllerBase
    {
        private readonly ILogger<CarWashController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IDataProtector _protector;

        public EarningsController(ILogger<CarWashController> logger, ApplicationDbContext context, IMapper mapper, IDataProtectionProvider protectionProvider)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            _protector = protectionProvider.CreateProtector("value_secret_and_unique");
        }

        /*
         * returns a carwash list of all bookings & earnings
         */
        [HttpGet("allbookingbycarwash/{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<EarningsDTO>>> GetBBC(int Id, [FromQuery] PaginationDTO pagination)
        {
            var checkId = context.CarWashes.Where(x => x.Id.Equals(Id)).ToList();
            if (checkId.Count == 0)
            {
                return BadRequest("You entered wrong ID.");
            }

            var carWashAuth = context.CarWashes.Where(x => x.Id == Id).SingleOrDefault();
            if (_protector.Unprotect(carWashAuth.Owner) != HttpContext.User.Identity.Name)
            {
                return BadRequest("Access Denied!");
            }
            var queryable = context.Earnings.Where(x => x.CarWashId == Id).AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var earnings = await queryable.Paginate(pagination).ToListAsync();
            return mapper.Map<List<EarningsDTO>>(earnings);
        }

        /*
         * returns bookings and earnings for a carwash filtered by YY or YY/MM or YY/MM/DD
         */
        [HttpGet("carwashearning/{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<EarningsDTO>>> GetCBD(int Id, [FromQuery] EarningsPaginationDTO pagination)
        {
            var checkId = context.CarWashes.Where(x => x.Id.Equals(Id)).ToList();
            if (checkId.Count == 0)
            {
                return BadRequest("You entered wrong ID.");
            }

            var carWashAuth = context.CarWashes.Where(x => x.Id == Id).SingleOrDefault();
            if (_protector.Unprotect(carWashAuth.Owner) != HttpContext.User.Identity.Name)
            {
                return BadRequest("Access Denied!");
            }

            var queryable = context.Earnings.Where(x => x.CarWashId == Id).Where(x => x.Appointment.Year == pagination.year)
                                                                          .Where(x => x.Appointment.Month == pagination.month)
                                                                          .Where(x => x.Appointment.Day == pagination.day).AsQueryable();
            if (pagination.day == 0)
            {
                queryable = context.Earnings.Where(x => x.CarWashId == Id).Where(x => x.Appointment.Year == pagination.year)
                                            .Where(x => x.Appointment.Month == pagination.month).AsQueryable();
            }
            if (pagination.day == 0 && pagination.month == 0)
            {
                queryable = context.Earnings.Where(x => x.CarWashId == Id).Where(x => x.Appointment.Year == pagination.year).AsQueryable();

            }
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var earnings = await queryable.Paginate(pagination).ToListAsync();
            return mapper.Map<List<EarningsDTO>>(earnings);
        }

        /*
         *  returns a list of bookings and earnings for a specific time period
         */
        [HttpGet("carwashaggregate/{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<EarningsDTO>>> GetCWA(int Id, [FromQuery] EarningsPaginationDTO pagination)
        {
            var checkId = context.CarWashes.Where(x => x.Id.Equals(Id)).ToList();
            if (checkId.Count == 0)
            {
                return BadRequest("You entered wrong ID.");
            }

            var carWashAuth = context.CarWashes.Where(x => x.Id == Id).SingleOrDefault();
            if (_protector.Unprotect(carWashAuth.Owner) != HttpContext.User.Identity.Name)
            {
                return BadRequest("Access Denied!");
            }

            if (pagination.month == 0 && pagination.day == 0 && pagination.dayT == 0 && pagination.monthT == 0 && pagination.year == 0 && pagination.yearT == 0)
            {
                return BadRequest("Enter some values!");
            }

            if (pagination.day == 0)
            {
                pagination.day = 1;
            }
            if (pagination.dayT == 0)
            {
                pagination.dayT = 1;
            }
            if (pagination.month == 0)
            {
                pagination.month = 1;
            }
            if (pagination.monthT == 0)
            {
                pagination.monthT = 1;
            }

            var input = new DateTime(pagination.year, pagination.month, pagination.day, 0, 0, 0);
            var inputT = new DateTime(pagination.yearT, pagination.monthT, pagination.dayT, 0, 0, 0);

            var queryable = context.Earnings.Where(x => x.CarWashId == Id).Where(x => x.Appointment >= input).AsQueryable();
            var queryable2 = queryable.Where(x => x.CarWashId == Id).Where(x => x.Appointment <= inputT).AsQueryable();

            await HttpContext.InsertPaginationParametersInResponse(queryable2, pagination.RecordsPerPage);
            var earnings = await queryable2.Paginate(pagination).ToListAsync();
            return mapper.Map<List<EarningsDTO>>(earnings);

        }

        [HttpGet("servicesearnings/{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<EarningsServiceCountDTO>>> GetSE(int Id)
        {
            var checkId = context.CarWashes.Where(x => x.Id.Equals(Id)).ToList();
            if (checkId.Count == 0)
            {
                return BadRequest("You entered wrong ID.");
            }

            var carWashAuth = context.CarWashes.Where(x => x.Id == Id).SingleOrDefault();
            if (_protector.Unprotect(carWashAuth.Owner) != HttpContext.User.Identity.Name)
            {
                return BadRequest("Access Denied!");
            }

            var services = new List<Earnings>();
            var listServices = context.Earnings.Where(x => x.CarWashId == Id).Select(x => x.ServiceId).ToList();
            var listServicesCleared = listServices.Distinct().ToList();
            foreach (var ls in listServicesCleared)
            {
                var servicesAdd = context.Earnings.Where(x => x.CarWashId == Id).Where(x => x.ServiceId == ls).ToList();
                services.AddRange(servicesAdd);
            }

            var servicesMy = mapper.Map<List<EarningsServiceCountDTO>>(services);
            return servicesMy;
        }
    }
}
