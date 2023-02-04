using CarWash.Helpers;
using CarWash.Validations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarWash.DTOs
{
    public class CarWashCreationDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        [Required]
        public int OpeningHours { get; set; }
        [Required]
        public int ClosingHours { get; set; }
        public string? Owner { get; set; }

        [FileSizeValidator(MaxFileSizeInMbs: 4)]
        [ContentTypeValidator(ContentTypeGroup.Image)]
        public IFormFile? Picture { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> CarWashServiceId { get; set; }
    }
}
