using Microsoft.AspNetCore.Identity;
using UserManager.Contracts.Responses;
using UserManager.Model;
using UserManager.Features.Authentication.Interfaces;
using UserManager.Features.Authentication.Model;
using UserManager.Features.Authentication.Requests;

namespace UserManager.Features.Authentication
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly ITokenService _tokenService;

        public LoginService(SignInManager<CustomUser> signInManager, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<ResponseApi> Login(LoginRequest request)
        {
            var usuario = _signInManager.UserManager.FindByEmailAsync(request.Email);

            if (usuario.Result == null) return new ResponseApi(false, "E-mail incorreto!");

            var resultadoIdentity = _signInManager
                .PasswordSignInAsync(usuario.Result.UserName, request.Password, false, false);

            if (!resultadoIdentity.Result.Succeeded)
            {
                return new ResponseApi(false, "Login falhou");
            }

            var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(user =>
                    user.NormalizedUserName == usuario.Result.UserName.ToUpper());

            if (identityUser.Status != true)
                return new ResponseApi(false, "Usuário inativo");

            Token token = _tokenService.CreateToken(identityUser,
                _signInManager.UserManager.GetRolesAsync(identityUser).Result.FirstOrDefault());

            return new ResponseApi(true, token.Value);
        }
    }
}
