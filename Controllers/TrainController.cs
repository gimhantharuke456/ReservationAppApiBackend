using Microsoft.AspNetCore.Mvc;
using ReservationAppApi.Models;
using ReservationAppApi.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationAppApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TrainController : ControllerBase
    {
        private readonly TrainService _trainService;

        public TrainController(TrainService trainService)
        {
            _trainService = trainService;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var existingTrain = await _trainService.GetAsync(id);
            if (existingTrain is null)
            {
                return NotFound();
            }
            return Ok(existingTrain);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allTrains = await _trainService.GetAsync();
            if (allTrains.Any())
            {
                return Ok(allTrains);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Train train)
        {
            await _trainService.CreateTrain(train);
            return CreatedAtAction(nameof(Get), new { id = train.Id }, train);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Train train)
        {
            var existingTrain = await _trainService.GetAsync(id);
            if (existingTrain is null)
            {
                return BadRequest();
            }
            train.Id = existingTrain.Id;
            await _trainService.UpdateTrain(train);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingTrain = await _trainService.GetAsync(id);
            if (existingTrain is null)
            {
                return BadRequest();
            }
            await _trainService.DeleteTrain(id);
            return NoContent();
        }
    }
}
