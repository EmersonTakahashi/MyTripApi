using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyTripApi.Data;
using MyTripApi.Models;
using MyTripApi.Models.Dto.Trip;
using MyTripApi.Repository;
using MyTripApi.Repository.IRepository;

namespace MyTripApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripApiController : ControllerBase
    {
        private readonly ILogger<TripApiController> _logger;
        private readonly ITripRepository _tripRepository;
        private readonly IMapper _mapper;

        public TripApiController(ILogger<TripApiController> logger, ITripRepository tripRepository, IMapper mapper)
        {
            _logger = logger;
            _tripRepository = tripRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TripDTO>>> GetTrips()
        {
            IEnumerable<Trip> tripList = await _tripRepository.GetAllAsync();
            return Ok(_mapper.Map<List<TripDTO>>(tripList));
        }
        [HttpGet("{id:Guid}", Name = "GetTrip")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TripDTO>> GetTrip(Guid id)
        {

            if (id == Guid.Empty)
            {
                _logger.LogError($"Get Trip Error with id: {id}");
                return BadRequest();
            }

            var trip = await _tripRepository.GetAsync(x => x.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TripDTO>(trip));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TripDTO>> CreateTrip([FromBody] TripCreateDTO tripCreateDTO)
        {
            if (tripCreateDTO == null)
            {
                return BadRequest(tripCreateDTO);
            }

            Trip trip = _mapper.Map<Trip>(tripCreateDTO);

            await _tripRepository.CreateAsync(trip);

            return CreatedAtRoute("GetTrip", new { id = trip.Id }, trip);
        }

        [HttpDelete("{id:Guid}", Name = "DeleteTrip")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTrip(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var trip = await _tripRepository.GetAsync(x => x.Id == id);
            if (trip == null)
            {
                return NotFound();
            }
            await _tripRepository.RemoveAsync(trip);
            return NoContent();
        }

        [HttpPut("{id:Guid}", Name = "UpdateTrip")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTrip(Guid id, [FromBody] TripUpdateDTO tripUpdateDTO)
        {
            if (tripUpdateDTO == null || tripUpdateDTO.Id != id)
            {
                return BadRequest();
            }

            Trip trip = _mapper.Map<Trip>(tripUpdateDTO);
            await _tripRepository.UpdateAsync(trip);

            return NoContent();
        }

        [HttpPatch("{id:Guid}", Name = "UpdatePartialTrip")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePartialTrip(Guid id, JsonPatchDocument<TripUpdateDTO> patchTripDTO)
        {
            if (patchTripDTO == null || id == Guid.Empty)
            {
                return BadRequest();
            }

            var trip = await _tripRepository.GetAsync(x => x.Id == id, tracked: false);
            if (trip == null)
            {
                return NotFound();
            }
            TripUpdateDTO tripUpdateDTO = _mapper.Map<TripUpdateDTO>(trip);

            patchTripDTO.ApplyTo(tripUpdateDTO, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Trip tripModel = _mapper.Map<Trip>(tripUpdateDTO);

            await _tripRepository.UpdateAsync(tripModel);
            
            return NoContent();
        }
    }
}
