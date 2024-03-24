using Microsoft.AspNetCore.Identity;
using System.Web;
using UserManager.Contracts.Responses;
using UserManager.Features.Users.Interfaces;
using UserManager.Features.Users.Requests;
using UserManager.Model;
using UserManager.Shared.Interface;

namespace UserManager.Features.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly IEmailService _emailService;

        public UserService(UserManager<CustomUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<ResponseApi> CreateUser(CreateUserRequest request)
        {
            // Verifica se o e-mail já existe
            if (await _userManager.FindByEmailAsync(request.Email) is not null)
                return new ResponseApi(false, "E-mail existente.");

            CustomUser newUser = new(request.Name, request.Email);

            try
            {
                var identityUser = await _userManager.CreateAsync(newUser, request.Password);

                if (!identityUser.Succeeded)
                    return new ResponseApi(false, "Não foi possível cadastrar o usuário: " + identityUser.Errors.First().Description);

                // Adiciona a role ao usuário
                var resultRole = await _userManager.AddToRoleAsync(newUser, "User");

                if (!resultRole.Succeeded)
                    return new ResponseApi(false, "Não foi possível adicionar a role ao usuário.");
            }
            catch (Exception ex)
            {
                return new ResponseApi(true, "Erro ao cadastrar usuário: " + ex.Message);
            }

            // cria token de confirmação de conta
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            string codeHtmlVersion = HttpUtility.UrlEncode(token);

            var response = await _emailService.SendAccountConfirmationEmail(newUser.Id, newUser.Name, codeHtmlVersion, newUser.Email);

            if (!response.Success)
            {
                return new ResponseApi(true, "Erro ao enviar e-mail: " + response.Message);
            }

            return new ResponseApi(true, "Usuário cadastrado com sucesso.", newUser.Id);
        }
    }
}
