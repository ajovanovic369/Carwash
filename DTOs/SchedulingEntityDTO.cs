namespace CarWash.DTOs
{
    public class SchedulingEntityDTO : SchedulingDTO
    {
        public List<CarWashDTO> Carwashes { get; set; }
        public List<CarWashServiceDTO> Services { get; set; }
    }
}
