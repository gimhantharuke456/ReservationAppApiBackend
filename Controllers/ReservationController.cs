using Microsoft.AspNetCore.Mvc;
using ReservationAppApi.Models;
using ReservationAppApi.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationAppApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var existingReservation = await _reservationService.GetAsync(id);
            if (existingReservation is null)
            {
                return NotFound();
            }
            return Ok(existingReservation);
        }
        [HttpGet("/train/{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            var existingReservation = await _reservationService.GetAsyncTrain(id);
            if (existingReservation is null)
            {
                return NotFound();
            }
            return Ok(existingReservation);
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allReservations = await _reservationService.GetAsync();
            if (allReservations.Any())
            {
                return Ok(allReservations);
            }
            return NotFound();
        }


        [HttpGet("/user/{id:length(24)}")]
        public async Task<IActionResult> GetReservationByUserId(string id)
        {
            var allReservations = await _reservationService.GetByUserIdAsync(id);
            if (allReservations.Any())
            {
                return Ok(allReservations);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Reservation reservation)
        {
            await _reservationService.CreateReservation(reservation);
            return CreatedAtAction(nameof(Get), new { id = reservation.Id }, reservation);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Reservation reservation)
        {
            var existingReservation = await _reservationService.GetAsync(id);
            if (existingReservation is null)
            {
                return BadRequest();
            }
            reservation.Id = existingReservation.Id;
            await _reservationService.UpdateReservation(reservation);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingReservation = await _reservationService.GetAsync(id);
            if (existingReservation is null)
            {
                return BadRequest();
            }
            await _reservationService.DeleteReservation(id);
            return NoContent();
        }
    }
}
