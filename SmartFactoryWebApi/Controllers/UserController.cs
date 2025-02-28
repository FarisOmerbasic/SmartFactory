using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartFactoryWebApi.Dtos;
using SmartFactoryWebApi.InMemoryRepositories;
using SmartFactoryWebApi.Models;
using SmartFactoryWebApi.Services;

namespace SmartFactoryWebApi.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IRenderPDFReport renderPDF) : ControllerBase
    {

        [HttpPost("register")]
        public ActionResult<string> Register([FromBody] UserDto request)
        {
            var user = UserRepository.GetUserByEmail(request.Email);

            if (user!=null) return BadRequest("User already exists with that email");

            var userResponse = new User
            {
                Id = UserRepository.Users.Count()+1,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Role = request.Role,
                Password = RandomString(10)
            };

            UserRepository.AddUser(userResponse);

            var pdf = renderPDF.RenderUserRegisterReport(userResponse);

            return File(pdf, "application/pdf", $"employee_{userResponse.FirstName}_{userResponse.LastName}.pdf");
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

        [HttpDelete("removeUser/{id}")]
        public ActionResult RemoveUser(int id)
        {
            var user=UserRepository.GetUserById(id);
            if (user == null) return BadRequest("User not found");

            UserRepository.RemoveUser(user);

            return Ok("User removed");
        }




        private string RandomString(int size)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*-_?";
            string result = string.Empty;
            int charactersLength = characters.Length;

            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                int randomIndex = random.Next(charactersLength);
                result += characters[randomIndex];
            }

            return result;
        }
    }

}
