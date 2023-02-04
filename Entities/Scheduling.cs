namespace CarWash.Entities
{
    public class Scheduling
    {
        public int Id { get; set; }
        public DateTime Appointment { get; set; }
        public DateTime CurrentDate { get; set; }
        public string? Status { get; set; }
        public decimal Price { get; set; }
        public string? UserReservation { get; set; }

        public List<SchedulingEntity> SchedulingEntity { get; set; }
        public List<SchedulingServices> SchedulingServices { get; set; }

    }
}
