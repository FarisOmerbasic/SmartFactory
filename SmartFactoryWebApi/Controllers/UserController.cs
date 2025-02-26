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
        public ActionResult<User> Login([FromBody] UserLoginDto request)
        {
            var user = UserRepository.GetUserByEmail(request.Email);

            if (user == null) return BadRequest("Invalid credentials");

            if (user.Password != request.Password) return BadRequest("Invalid credentials");

            return Ok(user);
        }
    }

}
