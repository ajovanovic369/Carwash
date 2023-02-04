namespace CarWash.Entities
{
    public class SchedulingEntity
    {
        public int SchedulingId { get; set; }
        public int CarWashEntityId { get; set; }
        public Scheduling Scheduling { get; set; }
        public CarWashEntity CarWashEntity { get; set; }
    }
}
