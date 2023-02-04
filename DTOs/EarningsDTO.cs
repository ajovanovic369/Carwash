using System.Runtime.Serialization;

namespace CarWash.DTOs
{
    public class EarningsDTO
    {
        [IgnoreDataMember]
        public int Id { get; set; }
        public int CarWashId { get; set; }
        public int ServiceId { get; set; }
        public int SchedulingId { get; set; }
        public DateTime Appointment { get; set; }
        public decimal Price { get; set; }
        [IgnoreDataMember]
        public string Owner { get; set; }

    }
}
