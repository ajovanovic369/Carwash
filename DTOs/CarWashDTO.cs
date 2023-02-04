using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CarWash.DTOs
{
    public class CarWashDTO
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
        [IgnoreDataMember]
        public string Owner { get; set; }

    }
}
