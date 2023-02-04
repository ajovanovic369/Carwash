namespace CarWash.Entities
{
    public class Earnings
    {
        public int Id { get; set; }
        public int CarWashId { get; set; }
        public int ServiceId { get; set; }
        public int SchedulingId { get; set; }
        public DateTime Appointment { get; set; }
        public decimal Price { get; set; }
        public string Owner { get; set; }

    }
}
