using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    public class LoginResponseDto
    {
        [Required]
        public  string Token { get; set; }
        //public required string Token { get; set; }
       // public required UserDto User { get; set; }
    }
}
