using Fleet.PrincipalUpdate.Models.Dto;
using Fleet.PrincipalUpdate.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.PrincipalUpdate.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {

        private readonly IAuthService _authService;
        protected ResponseDto _response;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _response = new ();
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if(!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSucess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);

            }
            return Ok(_response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }
    }
}
