using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserManager.Shared.Interface;

namespace UserManager.Features.AccountManagement
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public AccountController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet("confirmacao")]
        [SwaggerOperation(Summary = "Confirma e-mail do usuário",
                          OperationId = "Post")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ConfirmaEmailUsuario(int userId, string token)
        {
            var response = await _emailService.ConfirmEmail(userId, token);

            return Ok(response);
        }
    }
}
