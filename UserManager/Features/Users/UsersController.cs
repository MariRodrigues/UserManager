using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserManager.Features.Users.Interfaces;
using UserManager.Features.Users.Requests;

namespace UserManager.Features.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra novo usuário",
                          OperationId = "Post")]
        [ProducesResponseType(200)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            var response = await _userService.CreateUser(request);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }
    }
}
