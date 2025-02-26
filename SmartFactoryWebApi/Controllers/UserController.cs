using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartFactoryWebApi.Dtos;
using SmartFactoryWebApi.InMemoryRepositories;
using SmartFactoryWebApi.Models;

namespace SmartFactoryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("login")]
        public ActionResult<UserDto> Login([FromBody] UserLoginDto request)
        {
            var user = UserRepository.GetUserByEmail(request.Email);

            if (user == null) return BadRequest("Invalid credentials");

            if (user.Password != request.Password) return BadRequest("Invalid credentials");

            var resoponse = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
            };

            return Ok(resoponse);
        }
    }

}
