using CarWash.Entities;
using CarWash.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.DTOs
{
    public class SchedulingCreationDTO
    {
        public int Id { get; set; }
        public DateTime Appointment { get; set; }
        public DateTime CurrentDate { get; set; }
        public string? Status { get; set; }
        public decimal Price { get; set; }
        public string? Owner { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> CarWashEntityId { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> CarWashServiceId { get; set; }

    }
}
