namespace CarWash.Entities
{
    public class CarWashEntityServices
    {
        public int CarWashEntityId { get; set; }
        public int CarWashServiceId { get; set; }
        public CarWashEntity CarWashEntity { get; set; }
        public CarWashService CarWashService { get; set; }
    }
}
