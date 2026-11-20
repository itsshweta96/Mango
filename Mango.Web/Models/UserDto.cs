namespace Mango.Web.Models
{
    public class UserDto
    {
        public required string ID { get; set; } //As it is guid
        public required string Email { get; set; }
        public required string Name { get; set; }
        public string? PhoneNumber { get; set; }
        //public required string PhoneNumber { get; set; }
    }
}
