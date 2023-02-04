using System.Runtime.Serialization;

namespace CarWash.DTOs
{
    public class EarningsServiceCountDTO
    {
        [IgnoreDataMember]
        public int Id { get; set; }
        [IgnoreDataMember]
        public int CarWashId { get; set; }
        public int ServiceId { get; set; }
        [IgnoreDataMember]
        public int SchedulingId { get; set; }
        [IgnoreDataMember]
        public DateTime Appointment { get; set; }
        public decimal Price { get; set; }
        [IgnoreDataMember]
        public string Owner { get; set; }
    }
}
