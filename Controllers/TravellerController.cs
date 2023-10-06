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
    public class TravelerController : ControllerBase
    {
        private const string V = "Not Active";
        private readonly TravelerService _travelerService;

        public TravelerController(TravelerService travelerService)
        {
            _travelerService = travelerService;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var existingTraveler = await _travelerService.GetAsync(id);
            if (existingTraveler is null)
            {
                return NotFound();
            }
            return Ok(existingTraveler);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allTravelers = await _travelerService.GetAsync();
            if (allTravelers.Any())
            {
                return Ok(allTravelers);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Traveler traveler)
        {
            await _travelerService.CreateTraveler(traveler);
            return CreatedAtAction(nameof(Get), new { id = traveler.Id }, traveler);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Traveler traveler)
        {
            var existingTraveler = await _travelerService.GetAsync(id);
            if (existingTraveler is null)
            {
                return BadRequest();
            }
            traveler.Id = existingTraveler.Id;
            await _travelerService.UpdateTraveler(traveler);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingTraveler = await _travelerService.GetAsync(id);
            if (existingTraveler is null)
            {
                return BadRequest();
            }
            await _travelerService.DeleteTraveler(id);
            return NoContent();
        }
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            try
            {
                var user = await _travelerService.SignInAsync(request.Nic, request.Password);

                if (user != null)
                {
                    Debug.WriteLine(user.AccountStatus);
                    if (user.AccountStatus == V)
                    {
                        return BadRequest("Account deactivated");
                    }
                    return Ok(new { Id = user.Id, NIC = user.NIC });
                }

                return BadRequest("Invalid credentials");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{nic}")]
        public async Task<ActionResult<Traveler>> GetByNIC(string nic)
        {
            var traveler = await _travelerService.GetByNICAsync(nic);
            if (traveler == null)
                return NotFound(); // Return 404 if not found

            return Ok(traveler); // Return the traveler data
        }

    }

}
