using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    public class LoginRequestDto
    {
        [Required]
        public  string Email { get; set; }
        [Required]
        public  string Password { get; set; }
        //public required string Email { get; set; } at LoginRequestDto loginRequestDto = new(); get error
        //public required string Password { get; set; }
    }
}
