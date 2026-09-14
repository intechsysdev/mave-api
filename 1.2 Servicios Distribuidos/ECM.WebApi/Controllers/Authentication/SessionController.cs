using System.Threading.Tasks;
using ECM.Aplicacion.DTO.Authentication;
using ECM.Aplicacion.Servicios.Interfaz.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECM.WebApi.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly IAuthenticationAppService _authenticationAppService;

        public SessionController(IAuthenticationAppService authenticationAppService)
        {

            _authenticationAppService = authenticationAppService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody]LoginDTO login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authenticationAppService.Login(login);

            if (!result.Valid)
            {
                return BadRequest("Failure credentials userName or Password");
            }

            return new OkObjectResult(result);
        }

        [HttpPost("[action]/{code}")]
        public async Task<IActionResult> Login([FromBody]LoginDTO login, string code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authenticationAppService.Login(login, code);

            if (!result.Valid)
            {
                return BadRequest("Failure credentials userName or Password");
            }

            return new OkObjectResult(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ValidationPassword([FromBody]LoginDTO login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authenticationAppService.ValidationPassword(login);

            if (!result.Valid)
            {
                return BadRequest("Failure credentials userName or Password");
            }

            return new OkObjectResult(new { valid = true });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]LoginDTO login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authenticationAppService.ValidationPassword(login);

            if (!result.Valid)
            {
                return BadRequest("Failure credentials user name or password");
            }

            return Ok();
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Logout([FromBody]string refreshToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            LoginDTO login = new LoginDTO()
            {
                UserCust = "",
                UserSuccli = "",
                Password = "",
                TokenRefresh = refreshToken
            };

            await _authenticationAppService.Logout(login);

            return Ok();
        }

        [HttpPut("[action]")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordDTO changePassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _authenticationAppService.ChangePassword(changePassword);

            return Ok();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> ChangePasswordUser([FromBody]LoginDTO login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _authenticationAppService.ChangePassword(login);

            return new OkObjectResult(new { valid = true });
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> AssingPassword([FromBody]AssingPasswordDTO assingPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _authenticationAppService.AssingPassword(assingPassword);

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Reset([FromBody]ResetPasswordDTO resetPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _authenticationAppService.ResetPassword(resetPassword);

            return Ok();
        }

        [HttpPost("[action]/{refreshToken}")]
        public async Task<IActionResult> Refresh(string refreshToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string jwt = Request.Headers["Authorization"];

            var result = await _authenticationAppService.Refresh(jwt, refreshToken);

            if (!result.Valid)
            {
                return BadRequest("Failure credentials userName or Password");
            }

            return new OkObjectResult(result.Jwt);
        }

        [HttpGet("[action]/{companyId}")]
        public async Task<IActionResult> UrlApi(string companyId)
        {
            var url = await _authenticationAppService.UrlApi(companyId);

            return new OkObjectResult(new { url });
        }
    }
}