using System.ComponentModel.DataAnnotations;

namespace SmartFactoryWebApi.Dtos
{
    public class UserLoginDto
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
