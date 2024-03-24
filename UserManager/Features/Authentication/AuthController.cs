using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserManager.Features.Authentication.Interfaces;
using UserManager.Features.Authentication.Requests;

namespace UserManager.Features.Authentication
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Fazer login do usuário",
                          OperationId = "Post")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> LogaUsuario(LoginRequest request)
        {
            var response = await _loginService.Login(request);
            return Ok(response);
        }
    }
}
