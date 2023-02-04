using CarWash.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace CarWash
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly IDataProtector _protector;

        public ApplicationDbContext(IDataProtectionProvider protectionProvider, [NotNullAttribute] DbContextOptions options) : base(options)
        {
            _protector = protectionProvider.CreateProtector("value_secret_and_unique");
        }

        public DbSet<CarWashEntity> CarWashes { get; set; }
        public DbSet<CarWashService> Services { get; set; }
        public DbSet<CarWashEntityServices> CarWashEntityServices { get; set; }
        public DbSet<Scheduling> Schedulings { get; set; }
        public DbSet<SchedulingEntity> SchedulingEntity { get; set; }
        public DbSet<SchedulingServices> SchedulingServices { get; set; }
        public DbSet<Earnings> Earnings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarWashEntityServices>().HasKey(x => new { x.CarWashServiceId, x.CarWashEntityId });
            modelBuilder.Entity<CarWashEntityServices>().HasKey(x => new { x.CarWashEntityId, x.CarWashServiceId });
            modelBuilder.Entity<SchedulingEntity>().HasKey(x => new { x.CarWashEntityId, x.SchedulingId });
            modelBuilder.Entity<SchedulingEntity>().HasKey(x => new { x.SchedulingId, x.CarWashEntityId });
            modelBuilder.Entity<SchedulingServices>().HasKey(x => new { x.CarWashServiceId, x.SchedulingId });
            modelBuilder.Entity<SchedulingServices>().HasKey(x => new { x.SchedulingId, x.CarWashServiceId });

            SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<IdentityUser>();


            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "fab4fac1-c546-41de-aebc-a14da6895711",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                ConcurrencyStamp= "1"
            });


            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@bla.com",
                NormalizedEmail = "ADMIN@BLA.COM",
                PasswordHash = hasher.HashPassword(null, "123Aa!")
            });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "fab4fac1-c546-41de-aebc-a14da6895711",
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            });
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                UserName = "aleksandar",
                NormalizedUserName = "ALEKSANDAR",
                Email = "aleksandar@bla.com",
                NormalizedEmail = "ALEKSANDAR@BLA.COM",
                PasswordHash = hasher.HashPassword(null, "123Aa!")
            });
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                UserName = "john",
                NormalizedUserName = "JOHN",
                Email = "john@yup.com",
                NormalizedEmail = "JOHN@YUP.COM",
                PasswordHash = hasher.HashPassword(null, "aA!123")
            });
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                UserName = "wick",
                NormalizedUserName = "WICK",
                Email = "wick@yup.com",
                NormalizedEmail = "WICK@YUP.COM",
                PasswordHash = hasher.HashPassword(null, "wick!123")
            });
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                UserName = "trent",
                NormalizedUserName = "TRENT",
                Email = "trent@yup.com",
                NormalizedEmail = "TRENT@YUP.COM",
                PasswordHash = hasher.HashPassword(null, "trent!123")
            });
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                UserName = "billy",
                NormalizedUserName = "BILLY",
                Email = "billy@yup.com",
                NormalizedEmail = "BILLY@YUP.COM",
                PasswordHash = hasher.HashPassword(null, "billy!123")
            });
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                UserName = "cartman",
                NormalizedUserName = "CARTMAN",
                Email = "cartman@yup.com",
                NormalizedEmail = "CARTMAN@YUP.COM",
                PasswordHash = hasher.HashPassword(null, "cartman!123")
            });
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                UserName = "kenny",
                NormalizedUserName = "KENNY",
                Email = "kenny@omg.com",
                NormalizedEmail = "KENNY@OMG.COM",
                PasswordHash = hasher.HashPassword(null, "kenny!123")
            });

            modelBuilder.Entity<CarWashEntity>().HasData(new CarWashEntity
            {
                Id = 1,
                Name = "Bili CarWash",
                Address = "Tamo Daleko 21",
                OpeningHours = 9,
                ClosingHours = 17,
                CarWashOpen = false,
                Picture = "",
                Owner = _protector.Protect("aleksandar")
            });
            modelBuilder.Entity<CarWashEntity>().HasData(new CarWashEntity
            {
                Id = 2,
                Name = "Miki Doo Pranje",
                Address = "Fiu 69",
                OpeningHours = 8,
                ClosingHours = 16,
                CarWashOpen = false,
                Picture = "",
                Owner = _protector.Protect("aleksandar")
            });
            modelBuilder.Entity<CarWashEntity>().HasData(new CarWashEntity
            {
                Id = 3,
                Name = "Mosa Komerc Wash",
                Address = "Bulevar Puteva 37",
                OpeningHours = 10,
                ClosingHours = 18,
                CarWashOpen = false,
                Picture = "",
                Owner = _protector.Protect("john")
            });
            modelBuilder.Entity<CarWashEntity>().HasData(new CarWashEntity
            {
                Id = 4,
                Name = "Carwash Infinity",
                Address = "Over the rock 123",
                OpeningHours = 9,
                ClosingHours = 17,
                CarWashOpen = false,
                Picture = "",
                Owner = _protector.Protect("aleksandar")
            });
            modelBuilder.Entity<CarWashEntity>().HasData(new CarWashEntity
            {
                Id = 5,
                Name = "Poor's Man Carwash",
                Address = "Bulevar Puteva 37",
                OpeningHours = 8,
                ClosingHours = 20,
                CarWashOpen = false,
                Picture = "",
                Owner = _protector.Protect("john")
            });
            modelBuilder.Entity<CarWashEntity>().HasData(new CarWashEntity
            {
                Id = 6,
                Name = "Something",
                Address = "Bulevar 37",
                OpeningHours = 8,
                ClosingHours = 20,
                CarWashOpen = false,
                Picture = "",
                Owner = _protector.Protect("aleksandar")
            });
            modelBuilder.Entity<CarWashEntity>().HasData(new CarWashEntity
            {
                Id = 7,
                Name = "Carwasha",
                Address = "Puteva 37",
                OpeningHours = 10,
                ClosingHours = 18,
                CarWashOpen = false,
                Picture = "",
                Owner = _protector.Protect("wick")
            });
            modelBuilder.Entity<CarWashEntity>().HasData(new CarWashEntity
            {
                Id = 8,
                Name = "Carwashb",
                Address = "Bulevar Necega 37",
                OpeningHours = 9,
                ClosingHours = 17,
                CarWashOpen = false,
                Picture = "",
                Owner = _protector.Protect("trent")
            });
            modelBuilder.Entity<CarWashEntity>().HasData(new CarWashEntity
            {
                Id = 9,
                Name = "Carwashc",
                Address = "Nesto Tamo 3",
                OpeningHours = 8,
                ClosingHours = 17,
                CarWashOpen = false,
                Picture = "",
                Owner = _protector.Protect("billy")
            });
            modelBuilder.Entity<CarWashEntity>().HasData(new CarWashEntity
            {
                Id = 10,
                Name = "Carwashq",
                Address = "Blafa 21",
                OpeningHours = 8,
                ClosingHours = 20,
                CarWashOpen = false,
                Picture = "",
                Owner = _protector.Protect("cartman")
            });
            modelBuilder.Entity<CarWashEntity>().HasData(new CarWashEntity
            {
                Id = 11,
                Name = "Carwashw",
                Address = "Iha Adresa 9",
                OpeningHours = 6,
                ClosingHours = 18,
                CarWashOpen = false,
                Picture = "",
                Owner = _protector.Protect("kenny")
            });
            modelBuilder.Entity<CarWashEntity>().HasData(new CarWashEntity
            {
                Id = 12,
                Name = "Carwashy",
                Address = "Polet Trg 3",
                OpeningHours = 9,
                ClosingHours = 20,
                CarWashOpen = false,
                Picture = "",
                Owner = _protector.Protect("aleksandar")
            });
            modelBuilder.Entity<CarWashEntity>().HasData(new CarWashEntity
            {
                Id = 13,
                Name = "Carwashu",
                Address = "Ada 2",
                OpeningHours = 10,
                ClosingHours = 20,
                CarWashOpen = false,
                Picture = "",
                Owner = _protector.Protect("aleksandar")
            });

            modelBuilder.Entity<CarWashService>().HasData(new CarWashService { Id = 1, Name = "Regular", Price = 500.0m, Owner = _protector.Protect("aleksandar") });
            modelBuilder.Entity<CarWashService>().HasData(new CarWashService { Id = 2, Name = "Extended", Price = 1000.0m, Owner = _protector.Protect("aleksandar") });
            modelBuilder.Entity<CarWashService>().HasData(new CarWashService { Id = 3, Name = "Premium", Price = 2000.0m, Owner = _protector.Protect("aleksandar") });
            modelBuilder.Entity<CarWashService>().HasData(new CarWashService { Id = 4, Name = "PoorService", Price = 100.0m, Owner = _protector.Protect("john") });
            modelBuilder.Entity<CarWashService>().HasData(new CarWashService { Id = 5, Name = "GlobalOne", Price = 10000.0m, Owner = _protector.Protect("wick") });
            modelBuilder.Entity<CarWashService>().HasData(new CarWashService { Id = 6, Name = "GlobalTwo", Price = 10000.0m, Owner = _protector.Protect("trent") });
            modelBuilder.Entity<CarWashService>().HasData(new CarWashService { Id = 7, Name = "GlobalThree", Price = 10000.0m, Owner = _protector.Protect("billy") });
            modelBuilder.Entity<CarWashService>().HasData(new CarWashService { Id = 8, Name = "GlobalFour", Price = 10000.0m, Owner = _protector.Protect("cartman") });
            modelBuilder.Entity<CarWashService>().HasData(new CarWashService { Id = 9, Name = "GlobalFive", Price = 10000.0m, Owner = _protector.Protect("kenny") });
            

            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 1, CarWashServiceId = 1 });
            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 1, CarWashServiceId = 2 });
            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 1, CarWashServiceId = 3 });
            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 2, CarWashServiceId = 1 });
            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 2, CarWashServiceId = 2 });
            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 3, CarWashServiceId = 5 });
            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 4, CarWashServiceId = 2 });
            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 4, CarWashServiceId = 3 });
            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 5, CarWashServiceId = 4 });
            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 6, CarWashServiceId = 1 });
            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 6, CarWashServiceId = 3 });
            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 7, CarWashServiceId = 5 });
            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 8, CarWashServiceId = 6 });
            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 9, CarWashServiceId = 7 });
            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 10, CarWashServiceId = 8 });
            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 11, CarWashServiceId = 9 });
            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 12, CarWashServiceId = 1 });
            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 12, CarWashServiceId = 2 });
            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 13, CarWashServiceId = 1 });
            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 13, CarWashServiceId = 2 });
            modelBuilder.Entity<CarWashEntityServices>().HasData(new CarWashEntityServices { CarWashEntityId = 13, CarWashServiceId = 3 });


            /*
             *  added only reservations, no earnings, IHosted "AddingEarnings" will take old accepted reservations and move then to earnings and delete them from schedulings db
             */

            modelBuilder.Entity<Scheduling>().HasData(new Scheduling
            {
                Id = 1,
                Appointment = new DateTime(2020, 02, 12, 11, 00, 00),
                Status = "accepted",
                Price = 500.00m,
                UserReservation = _protector.Protect("wick"),
                CurrentDate = new DateTime(2020, 02, 08, 11, 00, 00)
            });
            modelBuilder.Entity<SchedulingEntity>().HasData(new SchedulingEntity { SchedulingId = 1, CarWashEntityId = 1 });
            modelBuilder.Entity<SchedulingServices>().HasData(new SchedulingServices { SchedulingId = 1, CarWashServiceId = 1 });

            modelBuilder.Entity<Scheduling>().HasData(new Scheduling
            {
                Id = 2,
                Appointment = new DateTime(2021, 05, 20, 12, 00, 00),
                Status = "accepted",
                Price = 1000.00m,
                UserReservation = _protector.Protect("cartman"),
                CurrentDate = new DateTime(2021, 05, 19, 11, 00, 00)
            });
            modelBuilder.Entity<SchedulingEntity>().HasData(new SchedulingEntity { SchedulingId = 2, CarWashEntityId = 1 });
            modelBuilder.Entity<SchedulingServices>().HasData(new SchedulingServices { SchedulingId = 2, CarWashServiceId = 2 });

            modelBuilder.Entity<Scheduling>().HasData(new Scheduling
            {
                Id = 3,
                Appointment = new DateTime(2022, 05, 14, 11, 00, 00),
                Status = "accepted",
                Price = 2000.00m,
                UserReservation = _protector.Protect("trent"),
                CurrentDate = new DateTime(2022, 05, 12, 13, 00, 00)
            });
            modelBuilder.Entity<SchedulingEntity>().HasData(new SchedulingEntity { SchedulingId = 3, CarWashEntityId = 1 });
            modelBuilder.Entity<SchedulingServices>().HasData(new SchedulingServices { SchedulingId = 3, CarWashServiceId = 3 });

            modelBuilder.Entity<Scheduling>().HasData(new Scheduling
            {
                Id = 4,
                Appointment = new DateTime(2022, 05, 24, 15, 00, 00),
                Status = "accepted",
                Price = 500.00m,
                UserReservation = _protector.Protect("john"),
                CurrentDate = new DateTime(2020, 05, 08, 09, 00, 00)
            });
            modelBuilder.Entity<SchedulingEntity>().HasData(new SchedulingEntity { SchedulingId = 4, CarWashEntityId = 1 });
            modelBuilder.Entity<SchedulingServices>().HasData(new SchedulingServices { SchedulingId = 4, CarWashServiceId = 1 });

            modelBuilder.Entity<Scheduling>().HasData(new Scheduling
            {
                Id = 5,
                Appointment = new DateTime(2022, 06, 15, 13, 00, 00),
                Status = "accepted",
                Price = 500.00m,
                UserReservation = _protector.Protect("wick"),
                CurrentDate = new DateTime(2020, 06, 10, 11, 00, 00)
            });
            modelBuilder.Entity<SchedulingEntity>().HasData(new SchedulingEntity { SchedulingId = 5, CarWashEntityId = 2 });
            modelBuilder.Entity<SchedulingServices>().HasData(new SchedulingServices { SchedulingId = 5, CarWashServiceId = 1 });

            modelBuilder.Entity<Scheduling>().HasData(new Scheduling
            {
                Id = 6,
                Appointment = new DateTime(2023, 06, 18, 14, 00, 00),
                Status = "accepted",
                Price = 10000.00m,
                UserReservation = _protector.Protect("aleksandar"),
                CurrentDate = new DateTime(2022, 06, 08, 11, 00, 00)
            });
            modelBuilder.Entity<SchedulingEntity>().HasData(new SchedulingEntity { SchedulingId = 6, CarWashEntityId = 3 });
            modelBuilder.Entity<SchedulingServices>().HasData(new SchedulingServices { SchedulingId = 6, CarWashServiceId = 5 });

            modelBuilder.Entity<Scheduling>().HasData(new Scheduling
            {
                Id = 7,
                Appointment = new DateTime(2023, 07, 12, 12, 00, 00),
                Status = "accepted",
                Price = 1000.00m,
                UserReservation = _protector.Protect("aleksandar"),
                CurrentDate = new DateTime(2022, 07, 01, 11, 00, 00)
            });
            modelBuilder.Entity<SchedulingEntity>().HasData(new SchedulingEntity { SchedulingId = 7, CarWashEntityId = 4 });
            modelBuilder.Entity<SchedulingServices>().HasData(new SchedulingServices { SchedulingId = 7, CarWashServiceId = 2 });

            modelBuilder.Entity<Scheduling>().HasData(new Scheduling
            {
                Id = 8,
                Appointment = new DateTime(2022, 07, 19, 15, 00, 00),
                Status = "accepted",
                Price = 100.00m,
                UserReservation = _protector.Protect("billy"),
                CurrentDate = new DateTime(2022, 07, 18, 11, 00, 00)
            });
            modelBuilder.Entity<SchedulingEntity>().HasData(new SchedulingEntity { SchedulingId = 8, CarWashEntityId = 5 });
            modelBuilder.Entity<SchedulingServices>().HasData(new SchedulingServices { SchedulingId = 8, CarWashServiceId = 4 });

            modelBuilder.Entity<Scheduling>().HasData(new Scheduling
            {
                Id = 9,
                Appointment = new DateTime(2022, 09, 20, 15, 00, 00),
                Status = "accepted",
                Price = 500.00m,
                UserReservation = _protector.Protect("john"),
                CurrentDate = new DateTime(2022, 09, 18, 11, 00, 00)
            });
            modelBuilder.Entity<SchedulingEntity>().HasData(new SchedulingEntity { SchedulingId = 9, CarWashEntityId = 1 });
            modelBuilder.Entity<SchedulingServices>().HasData(new SchedulingServices { SchedulingId = 9, CarWashServiceId = 1 });

            modelBuilder.Entity<Scheduling>().HasData(new Scheduling
            {
                Id = 10,
                Appointment = new DateTime(2022, 09, 22, 12, 00, 00),
                Status = "accepted",
                Price = 1000.00m,
                UserReservation = _protector.Protect("cartman"),
                CurrentDate = new DateTime(2022, 09, 12, 10, 00, 00)
            });
            modelBuilder.Entity<SchedulingEntity>().HasData(new SchedulingEntity { SchedulingId = 10, CarWashEntityId = 2 });
            modelBuilder.Entity<SchedulingServices>().HasData(new SchedulingServices { SchedulingId = 10, CarWashServiceId = 2 });

            modelBuilder.Entity<Scheduling>().HasData(new Scheduling
            {
                Id = 11,
                Appointment = new DateTime(2022, 09, 23, 15, 00, 00),
                Status = "accepted",
                Price = 500.00m,
                UserReservation = _protector.Protect("wick"),
                CurrentDate = new DateTime(2022, 09, 21, 13, 00, 00)
            });
            modelBuilder.Entity<SchedulingEntity>().HasData(new SchedulingEntity { SchedulingId = 11, CarWashEntityId = 2 });
            modelBuilder.Entity<SchedulingServices>().HasData(new SchedulingServices { SchedulingId = 11, CarWashServiceId = 1 });

            modelBuilder.Entity<Scheduling>().HasData(new Scheduling
            {
                Id = 12,
                Appointment = new DateTime(2022, 09, 05, 12, 00, 00),
                Status = "accepted",
                Price = 10000.00m,
                UserReservation = _protector.Protect("kenny"),
                CurrentDate = new DateTime(2022, 09, 01, 11, 00, 00)
            });
            modelBuilder.Entity<SchedulingEntity>().HasData(new SchedulingEntity { SchedulingId = 12, CarWashEntityId = 3 });
            modelBuilder.Entity<SchedulingServices>().HasData(new SchedulingServices { SchedulingId = 12, CarWashServiceId = 5 });

            modelBuilder.Entity<Scheduling>().HasData(new Scheduling
            {
                Id = 13,
                Appointment = new DateTime(2022, 09, 06, 12, 00, 00),
                Status = "accepted",
                Price = 1000.00m,
                UserReservation = _protector.Protect("john"),
                CurrentDate = new DateTime(2022, 09, 01, 11, 00, 00)
            });
            modelBuilder.Entity<SchedulingEntity>().HasData(new SchedulingEntity { SchedulingId = 13, CarWashEntityId = 4 });
            modelBuilder.Entity<SchedulingServices>().HasData(new SchedulingServices { SchedulingId = 13, CarWashServiceId = 2 });

            modelBuilder.Entity<Scheduling>().HasData(new Scheduling
            {
                Id = 14,
                Appointment = new DateTime(2022, 10, 04, 13, 00, 00),
                Status = "accepted",
                Price = 100.00m,
                UserReservation = _protector.Protect("aleksandar"),
                CurrentDate = new DateTime(2020, 10, 01, 11, 00, 00)
            });
            modelBuilder.Entity<SchedulingEntity>().HasData(new SchedulingEntity { SchedulingId = 14, CarWashEntityId = 5 });
            modelBuilder.Entity<SchedulingServices>().HasData(new SchedulingServices { SchedulingId = 14, CarWashServiceId = 4 });

            modelBuilder.Entity<Scheduling>().HasData(new Scheduling
            {
                Id = 15,
                Appointment = new DateTime(2023, 10, 04, 13, 00, 00),
                Status = "accepted",
                Price = 100.00m,
                UserReservation = _protector.Protect("john"),
                CurrentDate = new DateTime(2022, 10, 01, 10, 00, 00)
            });
            modelBuilder.Entity<SchedulingEntity>().HasData(new SchedulingEntity { SchedulingId = 15, CarWashEntityId = 5 });
            modelBuilder.Entity<SchedulingServices>().HasData(new SchedulingServices { SchedulingId = 15, CarWashServiceId = 4 });

            modelBuilder.Entity<Scheduling>().HasData(new Scheduling
            {
                Id = 16,
                Appointment = new DateTime(2023, 10, 06, 13, 00, 00),
                Status = "accepted",
                Price = 100.00m,
                UserReservation = _protector.Protect("wick"),
                CurrentDate = new DateTime(2022, 10, 01, 13, 00, 00)
            });
            modelBuilder.Entity<SchedulingEntity>().HasData(new SchedulingEntity { SchedulingId = 16, CarWashEntityId = 5 });
            modelBuilder.Entity<SchedulingServices>().HasData(new SchedulingServices { SchedulingId = 16, CarWashServiceId = 4 });

            modelBuilder.Entity<Scheduling>().HasData(new Scheduling
            {
                Id = 17,
                Appointment = new DateTime(2023, 10, 07, 13, 00, 00),
                Status = "accepted",
                Price = 100.00m,
                UserReservation = _protector.Protect("trent"),
                CurrentDate = new DateTime(2022, 10, 01, 12, 00, 00)
            });
            modelBuilder.Entity<SchedulingEntity>().HasData(new SchedulingEntity { SchedulingId = 17, CarWashEntityId = 1 });
            modelBuilder.Entity<SchedulingServices>().HasData(new SchedulingServices { SchedulingId = 17, CarWashServiceId = 2 });

            modelBuilder.Entity<Scheduling>().HasData(new Scheduling
            {
                Id = 18,
                Appointment = new DateTime(2023, 10, 09, 13, 00, 00),
                Status = "accepted",
                Price = 100.00m,
                UserReservation = _protector.Protect("billy"),
                CurrentDate = new DateTime(2022, 10, 02, 12, 00, 00)
            });
            modelBuilder.Entity<SchedulingEntity>().HasData(new SchedulingEntity { SchedulingId = 18, CarWashEntityId = 1 });
            modelBuilder.Entity<SchedulingServices>().HasData(new SchedulingServices { SchedulingId = 18, CarWashServiceId = 3 });

            modelBuilder.Entity<Scheduling>().HasData(new Scheduling
            {
                Id = 19,
                Appointment = new DateTime(2023, 10, 10, 13, 00, 00),
                Status = "accepted",
                Price = 100.00m,
                UserReservation = _protector.Protect("cartman"),
                CurrentDate = new DateTime(2022, 10, 02, 11, 00, 00)
            });
            modelBuilder.Entity<SchedulingEntity>().HasData(new SchedulingEntity { SchedulingId = 19, CarWashEntityId = 1 });
            modelBuilder.Entity<SchedulingServices>().HasData(new SchedulingServices { SchedulingId = 19, CarWashServiceId = 2 });

            modelBuilder.Entity<Scheduling>().HasData(new Scheduling
            {
                Id = 20,
                Appointment = new DateTime(2023, 10, 11, 13, 00, 00),
                Status = "accepted",
                Price = 100.00m,
                UserReservation = _protector.Protect("kenny"),
                CurrentDate = new DateTime(2022, 10, 02, 13, 00, 00)
            });
            modelBuilder.Entity<SchedulingEntity>().HasData(new SchedulingEntity { SchedulingId = 20, CarWashEntityId = 1 });
            modelBuilder.Entity<SchedulingServices>().HasData(new SchedulingServices { SchedulingId = 20, CarWashServiceId = 1 });
        }
    }
}
