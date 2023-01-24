using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTripApi.Models;
using MyTripApi.Models.Dto.User;
using System.CodeDom.Compiler;

namespace MyTripApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        protected APIResponse _response;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
            _response = new();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            try
            {
                var user = Authenticate(userLoginDTO);

                if (user != null)
                {
                    var token = Generate(user);

                    _response.StatusCode = System.Net.HttpStatusCode.OK;
                    _response.Result = token;
                    return Ok(_response);
                }

                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErroMessages = new List<string>() { ex.Message?.ToString() ?? ex.InnerException?.Message?.ToString() ?? ex.ToString() };
            }
            return _response;
        }

        private UserDTO Authenticate(UserLoginDTO userLoginDTO)
        {
            throw new NotImplementedException();
        }  
        private string Generate(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }
    }
}
