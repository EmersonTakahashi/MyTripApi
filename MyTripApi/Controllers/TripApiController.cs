using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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

        public TripApiController(ILogger<TripApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<TripDTO>> GetTrips()
        {
            return Ok(TripStore.tripList);
        }
        [HttpGet("{id:Guid}",Name ="GetTrip")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TripDTO> GetTrip(Guid id) {

            if(id == Guid.Empty)
            {
                _logger.LogError($"Get Trip Error with id: {id}");
                return BadRequest();
            }

            var trip = TripStore.tripList.FirstOrDefault(x => x.Id == id);
            if(trip == null)
            {
                return NotFound();
            }

            return Ok(trip);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TripDTO> CreateTrip([FromBody]TripDTO tripDTO)
        {
            if(TripStore.tripList.FirstOrDefault(x => x.Name.ToLower() == tripDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomValidation", "Trip already exists");
                return BadRequest(ModelState);
            }
            if(tripDTO == null)
            {
                return BadRequest(tripDTO);
            }

            tripDTO.Id = Guid.NewGuid();    

            TripStore.tripList.Add(tripDTO);

            return CreatedAtRoute("GetTrip", new { id = tripDTO.Id}, tripDTO); 
        }

        [HttpDelete("{id:Guid}", Name = "DeleteTrip")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTrip(Guid id)
        {
            if(id == Guid.Empty)
            {
                return BadRequest();
            }

            var trip = TripStore.tripList.FirstOrDefault(x => x.Id == id);
            if (trip == null)
            {
                return NotFound();
            }
            TripStore.tripList.Remove(trip);
            return NoContent();
        }

        [HttpPut("{id:Guid}", Name = "UpdateTrip")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateTrip(Guid id, [FromBody]TripDTO tripDTO)
        {
            if(tripDTO == null || tripDTO.Id != id)
            {
                return BadRequest();   
            }

            var trip = TripStore.tripList.FirstOrDefault(x => x.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            trip.Name = tripDTO.Name;
            trip.UpdatedAt = DateTime.UtcNow;

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

            var trip = TripStore.tripList.FirstOrDefault(x => x.Id == id);
            if (trip == null)
            {
                return NotFound();
            }
            patchTripDTO.ApplyTo(trip, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
