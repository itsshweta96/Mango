namespace Mango.Services.AuthAPI.Models
{
    public class UserDto
    {
        public required string Id { get; set; } //As it is guid
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
