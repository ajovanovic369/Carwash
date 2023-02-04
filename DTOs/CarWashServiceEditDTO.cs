using CarWash.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;

namespace CarWash.DTOs
{
    public class CarWashServiceEditDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        [IgnoreDataMember]
        public string? Owner { get; set; }
    }
}
