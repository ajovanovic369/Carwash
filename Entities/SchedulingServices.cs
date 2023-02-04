namespace CarWash.Entities
{
    public class SchedulingServices
    {
        public int SchedulingId { get; set; }
        public int CarWashServiceId { get; set; }
        public Scheduling Scheduling { get; set; }
        public CarWashService CarWashService { get; set; }
    }
}
