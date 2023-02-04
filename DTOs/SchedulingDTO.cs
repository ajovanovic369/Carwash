using System.Runtime.Serialization;

namespace CarWash.DTOs
{
    public class SchedulingDTO
    {
        
        public int Id { get; set; }
        public DateTime Appointment { get; set; }
        [IgnoreDataMember]
        public DateTime CurrentDate { get; set; }
        public string? Status { get; set; }
        public decimal Price { get; set; }
        [IgnoreDataMember]
        public string? UserReservation { get; set; }
    }
}
