using CarWash.Validations;
using System.ComponentModel.DataAnnotations;

namespace CarWash.DTOs
{
    public class UserInfo
    {
        [Required]
        [UserNameValidator]
        public string UserName { get; set; }

        [EmailAddress]
        [EmailAddressValidator]
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
