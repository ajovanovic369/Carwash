using AutoMapper;
using CarWash.DTOs;
using CarWash.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Controllers
{
    [Route("api/booking")]
    [ApiController]
    [EnableCors(PolicyName = "AllowAPIRequestIO")]
    public class SchedulingController : ControllerBase
    {
        private readonly ILogger<CarWashController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IDataProtector _protector;

        public SchedulingController(ILogger<CarWashController> logger, ApplicationDbContext context, IMapper mapper, IDataProtectionProvider protectionProvider)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            _protector = protectionProvider.CreateProtector("value_secret_and_unique");
        }

        /*
         * returns a list of all user reservations
         */
        [HttpGet("myreservations")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<SchedulingDTO>>> GetMyServices()
        {
            var checkOwner = context.Schedulings.Select(x => new Scheduling { Id = x.Id, UserReservation = x.UserReservation }).ToList();
            var listIds = new List<int>();
            foreach (var check in checkOwner)
            {
                var proba = _protector.Unprotect(check.UserReservation);
                if (_protector.Unprotect(check.UserReservation) == HttpContext.User.Identity.Name)
                {
                    listIds.Add(check.Id);
                }
            }

            if (!listIds.Any())
            {
                return NotFound("There are no reservations for your account yet.");
            }
            var listMyScheduling = new List<Scheduling>();
            foreach (var clr in listIds)
            {
                Scheduling cw = context.Schedulings.Where(x => x.Id == clr).Single<Scheduling>();
                listMyScheduling.Add(cw);
            }
            var carWashMy = mapper.Map<List<SchedulingDTO>>(listMyScheduling);
            return carWashMy;
        }

        /*
         * returns a booking appointment with accompanying carwash and service
         */
        [HttpGet("{Id:int}", Name = "getScheduling")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<SchedulingEntityDTO>> Get(int Id)
        {
            var checkId = context.Schedulings.Where(x => x.Id.Equals(Id)).ToList();
            if (checkId.Count == 0)
            {
                return BadRequest("You entered wrong ID.");
            }

            var schedulingDB = await context.Schedulings
                .Include(x => x.SchedulingEntity).ThenInclude(x => x.CarWashEntity)
                .Include(x => x.SchedulingServices).ThenInclude(x => x.CarWashService)
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (_protector.Unprotect(schedulingDB.UserReservation) != HttpContext.User.Identity.Name)
            {
                return BadRequest("You don't have access to view this reservation.");
            }

            return mapper.Map<SchedulingEntityDTO>(schedulingDB);

        }

        /*
         *  make an appointment
         */
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] SchedulingCreationDTO schedulingCreationDTO)
        {
            var scheduling = mapper.Map<Scheduling>(schedulingCreationDTO);
            context.Add(scheduling);

            CarWashService cw = context.Services.Where(x => x.Id == schedulingCreationDTO.CarWashServiceId.First()).Single<CarWashService>();
            scheduling.Price = cw.Price;

            scheduling.CurrentDate = DateTime.Now;

            scheduling.Status = "Pending";

            scheduling.UserReservation = _protector.Protect(HttpContext.User.Identity.Name);

            if (scheduling.Appointment < DateTime.Now)
            {
                return BadRequest("Incorrect date!");
            }

            if ((scheduling.Appointment - DateTime.Now) <= new TimeSpan(0, 15, 0))
            {

                return BadRequest("You can't make an reservation 15 minutes before appointment");
            }

            if (scheduling.Appointment.DayOfWeek.ToString() == "Sunday")
            {
                return BadRequest("Carwash is not open on sunday.");
            }

            var checkCarWashNo = schedulingCreationDTO.CarWashEntityId.Count;
            if (checkCarWashNo > 1)
            {
                return BadRequest("Choose only one carwash to make an reservation per one request.");
            }

            var checkServiceNo = schedulingCreationDTO.CarWashServiceId.Count;
            if (checkServiceNo > 1)
            {
                return BadRequest("Please choose only one service.");
            }

            var checkFullMinute = scheduling.Appointment.Minute;
            if (checkFullMinute != 00)
            {
                return BadRequest("Reservations can be made by full hour only.");
            }

            var checkFullSecond = scheduling.Appointment.Second;
            if (checkFullSecond != 00)
            {
                return BadRequest("Reservations can be made by full hour only.");
            }

            var inputCarWashId = schedulingCreationDTO.CarWashEntityId.First();
            CarWashEntity cwe = context.CarWashes.Where(x => x.Id == inputCarWashId).Single<CarWashEntity>();

            if (cwe.OpeningHours > schedulingCreationDTO.Appointment.Hour || cwe.ClosingHours <= schedulingCreationDTO.Appointment.Hour)
            {
                return BadRequest("Outside carwash working hours.");
            }

            var checkServiceCarWashInput = new List<int> { schedulingCreationDTO.CarWashEntityId.First(), schedulingCreationDTO.CarWashServiceId.First() };
            var checkServiceCarWash = await context.CarWashes.Where(x => x.Id == inputCarWashId)
                                                                         .SelectMany(x => x.CarWashEntityServices)
                                                                         .ToListAsync();
            var checkServiceCarWashDB = new List<int> { };
            foreach (var cwas in checkServiceCarWash)
            {
                checkServiceCarWashDB.Add(cwas.CarWashEntityId);
                checkServiceCarWashDB.Add(cwas.CarWashServiceId);
            }

            var checkServiceTrue = false;
            for (int i = 0; i < checkServiceCarWashDB.Count - 1; i++)
            {
                if (checkServiceCarWashInput.ElementAt(0) == checkServiceCarWashDB.ElementAt(i) && checkServiceCarWashInput.ElementAt(1) == checkServiceCarWashDB.ElementAt(i + 1))
                {
                    checkServiceTrue = true;
                }
            }
            if (checkServiceTrue == false)
            {
                return BadRequest("Carwash doesn't offer selected service.");
            }

            var carShopIdNumber = schedulingCreationDTO.CarWashEntityId.First();
            var checkReservationCarShop = await context.CarWashes.Where(x => x.Id == carShopIdNumber)
                                                                         .SelectMany(x => x.SchedulingEntity)
                                                                         .ToListAsync();
            var checkReservationList = new List<int> { };
            foreach (var crl in checkReservationCarShop)
            {
                checkReservationList.Add(crl.SchedulingId);
            }
            var checkReservation = false;
            foreach (var clr in checkReservationList)
            {
                Scheduling cwc = context.Schedulings.Where(x => x.Id == clr).Single<Scheduling>();
                if (cwc.Appointment == schedulingCreationDTO.Appointment)
                {
                    checkReservation = true;
                    break;
                }
            }
            if (checkReservation == true)
            {
                return BadRequest("There is already an accepted reservation for carwash during time period you chosen.");
            }

            await context.SaveChangesAsync();
            var schedulingDTO = mapper.Map<SchedulingDTO>(scheduling);
            return new CreatedAtRouteResult("getScheduling", new { schedulingDTO.Id }, schedulingDTO);
        }

        /*
         *  editing reservation status by owner of the carwash
         */
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(int id, [FromBody] SchedulingEditDTO schedulingEditDTO)
        {
            var checkId = context.Schedulings.Where(x => x.Id.Equals(id)).ToList();
            if (checkId.Count == 0)
            {
                return BadRequest("You entered wrong ID.");
            }

            var schedulingDB = await context.Schedulings.FirstOrDefaultAsync(x => x.Id == id);
            var schedulingEntityDB = await context.SchedulingEntity.FirstOrDefaultAsync(x => x.SchedulingId == id);
            var entityDB = await context.CarWashes.FirstOrDefaultAsync(x => x.Id == schedulingEntityDB.CarWashEntityId);

            if (_protector.Unprotect(entityDB.Owner) != HttpContext.User.Identity.Name)
            {
                return BadRequest("Access denied!");
            }

            if (schedulingDB == null)
            {
                return NotFound("Carwash can't be found.");
            }

            schedulingDB = mapper.Map(schedulingEditDTO, schedulingDB);

            schedulingDB.Status = schedulingDB.Status.ToLower();
            if (schedulingDB.Status != "accepted" && schedulingDB.Status != "rejected")
            {
                return BadRequest("You need to choose accepted or rejected option.");
            }

            await context.SaveChangesAsync();
            return Ok("Appointment was successfully edited.");
        }

        /*
         * deletion of user posted reservation, by user, only available for deletion if time > 15 minutes
         */
        [HttpDelete("cancellation/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeleteByUser(int id)
        {
            var checkId = context.Schedulings.Where(x => x.Id.Equals(id)).ToList();
            if (checkId.Count == 0)
            {
                return BadRequest("You entered wrong ID.");
            }

            Scheduling cw = context.Schedulings.Where(x => x.Id == id).Single<Scheduling>();

            if (cw == null)
            {
                return NotFound();
            }

            if (_protector.Unprotect(cw.UserReservation) != HttpContext.User.Identity.Name)
            {
                return BadRequest("Access denied!");
            }

            var checkDateResult = cw.Appointment - DateTime.Now;
            var checkDateMinutes = checkDateResult.TotalMinutes;

            if (checkDateMinutes < 15)
            {
                return BadRequest("Sorry you are not allowed to cancel your reservation anymore.");
            }

            context.Schedulings.Remove(cw);
            context.SaveChanges();
            return Ok("Reservation was successfully deleted.");
        }

        /*
         * deletion of user posted reservation, by carwash owner, only available for deletion if time > 1 hour
         */
        [HttpDelete("cancelbyowner/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeleteByAdmin(int id)
        {
            var checkId = context.Schedulings.Where(x => x.Id.Equals(id)).ToList();
            if (checkId.Count == 0)
            {
                return BadRequest("You entered wrong ID.");
            }

            Scheduling cw = context.Schedulings.Where(x => x.Id == id).Single<Scheduling>();

            if (cw == null)
            {
                return NotFound();
            }

            SchedulingEntity se = context.SchedulingEntity.Where(x => x.SchedulingId == id).Single<SchedulingEntity>();
            CarWashEntity cwe = context.CarWashes.Where(x => x.Id == se.CarWashEntityId).Single<CarWashEntity>();
            if (_protector.Unprotect(cwe.Owner) != HttpContext.User.Identity.Name)
            {
                return BadRequest("You are not an owner of the carwash!");
            }

            var checkDateResult = cw.Appointment - DateTime.Now;
            var checkDateHour = checkDateResult.TotalHours;

            if (checkDateHour < 1)
            {
                return BadRequest("Sorry you are not allowed to cancel reservation anymore.");
            }

            context.Schedulings.Remove(cw);
            context.SaveChanges();
            return Ok("Reservation was successfully deleted.");
        }
    }
}
