using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyTripApi.Data;
using MyTripApi.Models;
using MyTripApi.Models.Dto.Trip;
using MyTripApi.Models.Entities;
using MyTripApi.Repository;
using MyTripApi.Repository.IRepository;
using System.Net;

namespace MyTripApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripApiController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ILogger<TripApiController> _logger;
        private readonly ITripRepository _tripRepository;
        private readonly IMapper _mapper;

        public TripApiController(ILogger<TripApiController> logger, ITripRepository tripRepository, IMapper mapper)
        {
            _logger = logger;
            _tripRepository = tripRepository;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [Route("trips")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetTrips()
        {
            try
            {
                IEnumerable<Trip> tripList = await _tripRepository.GetAllAsync(x => x.Active == true);
                _response.Result = _mapper.Map<List<TripDTO>>(tripList);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErroMessages = new List<string>() { ex.Message?.ToString() ?? ex.InnerException?.Message?.ToString() ?? ex.ToString() };
            }
            return _response;
        }
        [HttpGet]
        [Route("trips/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetTrip(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    _response.IsSuccess = false;
                    _logger.LogError($"Get Trip Error with id: {id}");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var trip = await _tripRepository.GetAsync(x => x.Active == true && x.Id == id);
                if (trip == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<TripDTO>(trip);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErroMessages = new List<string>() { ex.Message?.ToString() ?? ex.InnerException?.Message?.ToString() ?? ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [Route("trips")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateTrip([FromBody] TripCreateDTO tripCreateDTO)
        {
            try
            {
                if (tripCreateDTO == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                Trip trip = _mapper.Map<Trip>(tripCreateDTO);

                await _tripRepository.CreateAsync(trip);

                _response.Result = _mapper.Map<TripDTO>(trip);
                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErroMessages = new List<string>() { ex.Message?.ToString() ?? ex.InnerException?.Message?.ToString() ?? ex.ToString() };
            }
            return _response;
        }

        [HttpDelete]
        [Route("trips/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteTrip(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var trip = await _tripRepository.GetAsync(x => x.Active == true && x.Id == id);
                if (trip == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                trip.Active = false;
                await _tripRepository.UpdateAsync(trip);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErroMessages = new List<string>() { ex.Message?.ToString() ?? ex.InnerException?.Message?.ToString() ?? ex.ToString() };
            }
            return _response;
        }

        [HttpPut]
        [Route("trips/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateTrip(Guid id, [FromBody] TripUpdateDTO tripUpdateDTO)
        {
            try
            {
                if (tripUpdateDTO == null || tripUpdateDTO.Id != id)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (await _tripRepository.GetAsync(x => x.Active == true && x.Id == id) == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }


                Trip trip = _mapper.Map<Trip>(tripUpdateDTO);
                await _tripRepository.UpdateAsync(trip);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErroMessages = new List<string>() { ex.Message?.ToString() ?? ex.InnerException?.Message?.ToString() ?? ex.ToString() };
            }
            return _response;
        }

    }
}
