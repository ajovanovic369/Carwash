using System.Runtime.Serialization;

namespace CarWash.DTOs
{
    public class CarWashEditDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int OpeningHours { get; set; }
        public int ClosingHours { get; set; }
        public string? Owner { get; set; }
    }
}
