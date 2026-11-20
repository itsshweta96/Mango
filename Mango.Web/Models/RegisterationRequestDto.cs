using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models 
{
    public class RegisterationRequestDto
    {
        [Required]
        public  string Email { get; set; }
        [Required]
        public  string Name { get; set; }
        [Required]
        public  string PhoneNumber { get; set; }
        [Required]
        public  string Password { get; set; }
        public string? Role { get; set; }

        //public required string Email { get; set; }
        //public required string Name { get; set; }
        //public required string PhoneNumber { get; set; }
        //public required string Password { get; set; }
        //public string? Role { get; set; }
    }
}
