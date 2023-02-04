using System.Runtime.Serialization;

namespace CarWash.DTOs
{
    public class CarWashServiceDTO
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [IgnoreDataMember]
        public string? Owner { get; set; }
    }
}
