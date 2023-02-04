namespace CarWash.Entities
{
    public class CarWashService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Owner { get; set; }

        public List<CarWashEntityServices> CarWashEntityServices { get; set; }
    }
}
