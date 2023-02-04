using CarWash.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;

namespace CarWash.DTOs
{
    public class CarWashServiceCreationDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        [IgnoreDataMember]
        public string? Owner { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> CarWashEntityId { get; set; }
    }
}
