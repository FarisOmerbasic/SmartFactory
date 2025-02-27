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

        [HttpPost("register")]
        public ActionResult<string> Register([FromBody] User request)
        {
            var user = UserRepository.GetUserByEmail(request.Email);

            if (user!=null) return BadRequest("User already exists with that email");

            UserRepository.AddUser(request);

            return Ok("User created");
        }





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

        [HttpGet("getAllUsers")]
        public ActionResult<List<UserDto>> GetAllUsers()
        {
            var users=UserRepository.GetAllUsers();

            var resoponse = users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Role = u.Role,
            }).ToList();


            return Ok(resoponse);
        }
    }

}
