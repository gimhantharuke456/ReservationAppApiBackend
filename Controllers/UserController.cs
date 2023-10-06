using Microsoft.AspNetCore.Mvc;
using ReservationAppApi.Models;
using ReservationAppApi.Services;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationAppApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var existingUser = await _userService.GetAsync(id);
            if (existingUser is null)
            {
                return NotFound();
            }
            return Ok(existingUser);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allUsers = await _userService.GetAsync();
            if (allUsers.Any())
            {
                return Ok(allUsers);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            await _userService.CreateUser(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, User user)
        {
            var existingUser = await _userService.GetAsync(id);
            if (existingUser is null)
            {
                return BadRequest();
            }
            user.Id = existingUser.Id;
            await _userService.UpdateUser(user);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingUser = await _userService.GetAsync(id);
            if (existingUser is null)
            {
                return BadRequest();
            }
            await _userService.DeleteUser(id);
            return NoContent();
        }
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            try
            {
                var user = await _userService.SignInAsync(request.Nic, request.Password);

                if (user != null)
                {
                    return Ok(new { Id = user.Id, NIC = user.NIC, role = user.Role });
                }

                return BadRequest("Invalid credentials");
            }
            catch (Exception ex)
            {

                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }
    }
    public class SignInRequest
    {
        public required string Nic { get; set; }
        public required string Password { get; set; }
    }
}
