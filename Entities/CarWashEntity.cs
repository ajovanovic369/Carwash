using System.ComponentModel.DataAnnotations;

namespace CarWash.Entities
{
    public class CarWashEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int OpeningHours { get; set; }
        [Required]
        public int ClosingHours { get; set; }
        public string? Picture { get; set; }
        public bool CarWashOpen { get; set; }
        public string Owner { get; set; }

        public List<CarWashEntityServices> CarWashEntityServices { get; set; }
        public List<SchedulingEntity> SchedulingEntity { get; set; }

    }
}
