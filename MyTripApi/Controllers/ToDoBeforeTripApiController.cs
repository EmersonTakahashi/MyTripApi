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
    public class ToDoBeforeTripApiController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ILogger<ToDoBeforeTripApiController> _logger;
        private readonly IToDoBeforeTripRepository _toDoBeforeTripRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IMapper _mapper;

        public ToDoBeforeTripApiController(ILogger<ToDoBeforeTripApiController> logger, IToDoBeforeTripRepository toDoBeforeTripRepository, IMapper mapper, ITripRepository tripRepository)
        {
            _logger = logger;
            _toDoBeforeTripRepository = toDoBeforeTripRepository;
            _mapper = mapper;
            this._response = new();
            _tripRepository = tripRepository;
        }

        [HttpGet]
        [Route("to-dos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllToDoBeforeTrip()
        {
            try
            {
                IEnumerable<ToDoBeforeTrip> tripList = await _toDoBeforeTripRepository.GetAllAsync();
                _response.Result = _mapper.Map<List<ToDoBeforeTripDTO>>(tripList);
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
        [Route("to-dos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetToDoBeforeTrip(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    _response.IsSuccess = false;
                    _logger.LogError($"Get ToDoBeforeTrip Error with id: {id}");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var toDoBeforeTrip = await _toDoBeforeTripRepository.GetAsync(x => x.Id == id);
                if (toDoBeforeTrip == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<ToDoBeforeTripDTO>(toDoBeforeTrip);
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
        [Route("to-dos")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateToDoBeforeTrip([FromBody] ToDoBeforeTripCreateDTO toDoBeforeTripCreateDTO)
        {
            try
            {
                if (toDoBeforeTripCreateDTO == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if(await _tripRepository.GetAsync(x => x.Id == toDoBeforeTripCreateDTO.TripId) == null)
                {
                    ModelState.AddModelError("CustomError", "TripId is invalid!");
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                ToDoBeforeTrip toDoBeforeTrip = _mapper.Map<ToDoBeforeTrip>(toDoBeforeTripCreateDTO);

                await _toDoBeforeTripRepository.CreateAsync(toDoBeforeTrip);

                _response.Result = _mapper.Map<ToDoBeforeTripCreateDTO>(toDoBeforeTrip);
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
        [Route("to-dos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteToDoBeforeTrip(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var toDoBeforeTrip = await _toDoBeforeTripRepository.GetAsync(x => x.Id == id);
                if (toDoBeforeTrip == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                toDoBeforeTrip.Active = false;
                await _toDoBeforeTripRepository.UpdateAsync(toDoBeforeTrip);  

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
        [Route("to-dos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateToDoBeforeTrip(Guid id, [FromBody] ToDoBeforeTripUpdateDTO toDoBeforeTripUpdateDTO)
        {
            try
            {
                //check if we are sending the id to API
                if (toDoBeforeTripUpdateDTO == null || toDoBeforeTripUpdateDTO.Id != id)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (await _tripRepository.GetAsync(x => x.Id == toDoBeforeTripUpdateDTO.TripId) == null)
                {
                    ModelState.AddModelError("CustomError", "TripId is invalid!");
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                ToDoBeforeTrip toDoBeforeTrip = _mapper.Map<ToDoBeforeTrip>(toDoBeforeTripUpdateDTO);
                await _toDoBeforeTripRepository.UpdateAsync(toDoBeforeTrip);

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
