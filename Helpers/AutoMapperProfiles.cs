using AutoMapper;
using CarWash.DTOs;
using CarWash.Entities;
using Microsoft.AspNetCore.Identity;

namespace CarWash.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<IdentityUser, UserDTO>()
                .ForMember(x => x.UserName, options => options.MapFrom(x => x.UserName))
                .ForMember(x => x.EmailAddress, options => options.MapFrom(x => x.Email))
                .ForMember(x => x.UserId, options => options.MapFrom(x => x.Id));

            CreateMap<CarWashEntity, CarWashDTO>();
            CreateMap<CarWashCreationDTO, CarWashEntity>()
                .ForMember(x => x.CarWashOpen, options => options.Ignore())
                .ForMember(x => x.Owner, options => options.Ignore())
                .ForMember(x => x.Picture, options => options.Ignore())
                .ForMember(x => x.CarWashEntityServices, options => options.MapFrom(CarWashEntityServicesCreate));
            CreateMap<CarWashEntity, CarWashCreationDTO>();
            CreateMap<CarWashEditDTO, CarWashEntity>()
                .ForMember(x => x.CarWashOpen, options => options.Ignore())
                .ForMember(x => x.Owner, options => options.Ignore())
                .ForMember(x => x.Picture, options => options.Ignore());
            CreateMap<CarWashEntity, CarWashEditDTO>();
            CreateMap<CarWashEntity, CarWashEntityServicesDTO>()
                .ForMember(x => x.Services, options => options.MapFrom(CarWashEntityServicesGet));
            CreateMap<CarWashServiceEditDTO, CarWashEntityServices>();
            CreateMap<CarWashEntityServices, CarWashServiceEditDTO>();
            CreateMap<CarWashService, CarWashServiceDTO>().ReverseMap();
            CreateMap<CarWashService, CarWashServiceCreationDTO>();
            CreateMap<CarWashServiceCreationDTO, CarWashService>()
                .ForMember(x => x.Owner, options => options.Ignore())
                .ForMember(x => x.CarWashEntityServices, options => options.MapFrom(CarWashServiceCreation));
            CreateMap<CarWashServiceEditDTO, CarWashService>().ReverseMap()
                .ForMember(x => x.Owner, options => options.Ignore());
            CreateMap<CarWashService, CarWashServiceEditDTO>().ReverseMap()
                .ForMember(x => x.Owner, options => options.Ignore());
            CreateMap<CarWashService, CarWashServicesEntityDTO>()
                .ForMember(x => x.CarWashes, options => options.MapFrom(MapServiceCarWashes));

            CreateMap<Scheduling, SchedulingDTO>().ReverseMap();
            CreateMap<Scheduling, SchedulingCreationDTO>()
                .ForMember(x => x.Price, options => options.Ignore())
                .ForMember(x => x.Status, options => options.Ignore());
            CreateMap<SchedulingCreationDTO, Scheduling>()
                .ForMember(x => x.UserReservation, options => options.Ignore())
                .ForMember(x => x.Price, options => options.Ignore())
                .ForMember(x => x.Status, options => options.Ignore())
                .ForMember(x => x.CurrentDate, options => options.Ignore())
                .ForMember(x => x.SchedulingEntity, options => options.MapFrom(CarWashSchedulingEntities))
                .ForMember(x => x.SchedulingServices, options => options.MapFrom(CarWashSchedulingServices));
            CreateMap<Scheduling, SchedulingEntityDTO>()
                .ForMember(x => x.Carwashes, options => options.MapFrom(MapSchedulingEntityGet))
                .ForMember(x => x.Services, options => options.MapFrom(MapSchedulingServiceGet));
            CreateMap<SchedulingEditDTO, Scheduling>().ReverseMap();

            CreateMap<Earnings, EarningsDTO>().ReverseMap();
            CreateMap<Earnings, EarningsServiceCountDTO>().ReverseMap();

        }

        private List<CarWashEntityServices> CarWashEntityServicesCreate(CarWashCreationDTO carWashCreationDTO, CarWashEntity carWashEntity)
        {
            var result = new List<CarWashEntityServices>();
            foreach (var id in carWashCreationDTO.CarWashServiceId)
            {
                result.Add(new CarWashEntityServices() { CarWashServiceId = id });
            }
            return result;
        }

        private List<CarWashServiceDTO> CarWashEntityServicesGet(CarWashEntity carWashEntity, CarWashEntityServicesDTO carWashEntityServicesDTO)
        {
            var result = new List<CarWashServiceDTO>();
            foreach (var carwashservices in carWashEntity.CarWashEntityServices)
            {
                result.Add(new CarWashServiceDTO() { Id = carwashservices.CarWashServiceId, Name = carwashservices.CarWashService.Name, Price = carwashservices.CarWashService.Price });
            }
            return result;
        }

        private List<CarWashEntityServices> CarWashServiceCreation(CarWashServiceCreationDTO carWashServiceCreationDTO, CarWashService carWashService)
        {
            var result = new List<CarWashEntityServices>();
            foreach (var id in carWashServiceCreationDTO.CarWashEntityId)
            {
                result.Add(new CarWashEntityServices() { CarWashEntityId = id });
            }
            return result;
        }

        private List<CarWashDTO> MapServiceCarWashes(CarWashService service, CarWashServicesEntityDTO carWashServicesEntityDTO)
        {
            var result = new List<CarWashDTO>();
            foreach (var servicecarwash in service.CarWashEntityServices)
            {
                result.Add(new CarWashDTO() { Id = servicecarwash.CarWashEntityId, Name = servicecarwash.CarWashEntity.Name, Address = servicecarwash.CarWashEntity.Address });
            }
            return result;
        }

        private List<SchedulingEntity> CarWashSchedulingEntities(SchedulingCreationDTO schedulingCreationDTO, Scheduling scheduling)
        {
            var result = new List<SchedulingEntity>();
            foreach (var id in schedulingCreationDTO.CarWashEntityId)
            {
                result.Add(new SchedulingEntity() { CarWashEntityId = id });
            }
            return result;
        }

        private List<SchedulingServices> CarWashSchedulingServices(SchedulingCreationDTO schedulingCreationDTO, Scheduling scheduling)
        {
            var result = new List<SchedulingServices>();
            foreach (var id in schedulingCreationDTO.CarWashServiceId)
            {
                result.Add(new SchedulingServices() { CarWashServiceId = id });
            }
            return result;
        }

        private List<CarWashDTO> MapSchedulingEntityGet(Scheduling scheduling, SchedulingEntityDTO schedulingEntityDTO)
        {
            var result = new List<CarWashDTO>();
            foreach (var schedulingEntity in scheduling.SchedulingEntity)
            {
                result.Add(new CarWashDTO()
                {
                    Id = schedulingEntity.CarWashEntityId,
                    Name = schedulingEntity.CarWashEntity.Name,
                    Address = schedulingEntity.CarWashEntity.Address,
                    OpeningHours = schedulingEntity.CarWashEntity.OpeningHours,
                    ClosingHours = schedulingEntity.CarWashEntity.ClosingHours,
                    CarWashOpen = schedulingEntity.CarWashEntity.CarWashOpen
                });
            }
            return result;
        }

        private List<CarWashServiceDTO> MapSchedulingServiceGet(Scheduling scheduling, SchedulingEntityDTO schedulingEntityDTO)
        {
            var result = new List<CarWashServiceDTO>();
            foreach (var schedulingService in scheduling.SchedulingServices)
            {
                result.Add(new CarWashServiceDTO()
                {
                    Id = schedulingService.CarWashService.Id,
                    Name = schedulingService.CarWashService.Name,
                    Price = schedulingService.CarWashService.Price
                });
            }
            return result;
        }
    }
}
