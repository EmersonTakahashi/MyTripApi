using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyTripApi.Data;
using MyTripApi.Models;
using MyTripApi.Models.Dto;

namespace MyTripApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripApiController : ControllerBase
    {
        private readonly ILogger<TripApiController> _logger;
        private readonly MyTripDbContext _dbContext;

        public TripApiController(ILogger<TripApiController> logger, MyTripDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TripDTO>> GetTrips()
        {
            return Ok(_dbContext.Trip.ToList());
        }
        [HttpGet("{id:Guid}", Name = "GetTrip")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TripDTO> GetTrip(Guid id)
        {

            if (id == Guid.Empty)
            {
                _logger.LogError($"Get Trip Error with id: {id}");
                return BadRequest();
            }

            var trip = _dbContext.Trip.FirstOrDefault(x => x.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            return Ok(trip);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TripDTO>> CreateTrip([FromBody] TripDTO tripDTO)
        {
            if (_dbContext.Trip.FirstOrDefault(x => x.Name.ToLower() == tripDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomValidation", "Trip already exists");
                return BadRequest(ModelState);
            }
            if (tripDTO == null)
            {
                return BadRequest(tripDTO);
            }

            tripDTO.Id = Guid.NewGuid();

            Trip trip = new()
            {
                Id = tripDTO.Id,
                Description = tripDTO.Description,
                StartAt = tripDTO.StartAt,
                EndAt = tripDTO.EndAt,
                Name = tripDTO.Name,
                Active = tripDTO.Active
            };

            await _dbContext.Trip.AddAsync(trip);
            await _dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetTrip", new { id = tripDTO.Id }, tripDTO);
        }

        [HttpDelete("{id:Guid}", Name = "DeleteTrip")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTrip(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var trip = _dbContext.Trip.FirstOrDefault(x => x.Id == id);
            if (trip == null)
            {
                return NotFound();
            }
            _dbContext.Trip.Remove(trip);
            _dbContext.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:Guid}", Name = "UpdateTrip")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateTrip(Guid id, [FromBody] TripDTO tripDTO)
        {
            if (tripDTO == null || tripDTO.Id != id)
            {
                return BadRequest();
            }

            Trip trip = new()
            {
                Name = tripDTO.Name,
                UpdatedAt = DateTime.UtcNow,
                Description = tripDTO.Description,
                Active = tripDTO.Active,
                StartAt = tripDTO.StartAt,
                EndAt = tripDTO.EndAt,
            };
            _dbContext.Update(trip);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:Guid}", Name = "UpdatePartialTrip")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePartialTrip(Guid id, JsonPatchDocument<TripDTO> patchTripDTO)
        {
            if (patchTripDTO == null || id == Guid.Empty)
            {
                return BadRequest();
            }

            var trip = _dbContext.Trip.FirstOrDefault(x => x.Id == id);
            if (trip == null)
            {
                return NotFound();
            }
            TripDTO tripDTO = new()
            {
                Name = trip.Name,                
                Description = trip.Description,
                Active = trip.Active,
                StartAt = trip.StartAt,
                EndAt = trip.EndAt,
            };

            patchTripDTO.ApplyTo(tripDTO, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Trip tripModel = new()
            {
                Name = tripDTO.Name,
                UpdatedAt = DateTime.UtcNow,
                Description = tripDTO.Description,
                Active = tripDTO.Active,
                StartAt = tripDTO.StartAt,
                EndAt = tripDTO.EndAt,
            };
            _dbContext.Update(tripModel);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
