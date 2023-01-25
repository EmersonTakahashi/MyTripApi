using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTripApi.Data;
using MyTripApi.Models;
using MyTripApi.Models.Dto.User;
using System.Net;

namespace MyTripApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        //private readonly SignInManager<ApiUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;

        public AccountController(UserManager<ApiUser> userManager, /*SignInManager<ApiUser> signInManager,*/ ILogger<AccountController> logger, IMapper mapper)
        {
            _userManager = userManager;
            //_signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
            _response = new();
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> Register([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;                
                return BadRequest(_response);
            }
            try
            {
                var user = _mapper.Map<ApiUser>(userDTO);
                var result = await _userManager.CreateAsync(user, userDTO.Password);

                if (!result.Succeeded)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;                    
                    return BadRequest(_response);
                }

                return Ok(_response);
            }
            catch (Exception ex)
            {
                //add log error
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErroMessages = new List<string>() { ex.Message?.ToString() ?? ex.InnerException?.Message?.ToString() ?? ex.ToString() };
                return _response;
            }
        }
        //[HttpPost]
        //[Route("login")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<APIResponse>> Login([FromBody] UserLoginDTO userLoginDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        _response.IsSuccess = false;
        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //        return BadRequest(_response);
        //    }
        //    try
        //    {
        //        //Check the lockoutOnFailure arg
        //        var result = await _signInManager.PasswordSignInAsync(userLoginDTO.EmailAddress, userLoginDTO.Password, false, false);

        //        if(!result.Succeeded)
        //        {
        //            _response.IsSuccess = false;
        //            _response.StatusCode = HttpStatusCode.Unauthorized;
        //            return Unauthorized(_response);
        //        }

        //        return Ok(_response);
        //    }
        //    catch (Exception ex)
        //    {
        //        //add log error
        //        _response.IsSuccess = false;
        //        _response.StatusCode = HttpStatusCode.InternalServerError;
        //        _response.ErroMessages = new List<string>() { ex.Message?.ToString() ?? ex.InnerException?.Message?.ToString() ?? ex.ToString() };
        //        return _response;
        //    }
        //}
    }
}
