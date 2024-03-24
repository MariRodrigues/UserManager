using UserManager.Contracts.Responses;

namespace UserManager.Shared.Interface
{
    public interface IEmailService
    {
        Task<ResponseApi> SendAccountConfirmationEmail(int userid, string nome, string token, string email);
        Task<ResponseApi> ConfirmEmail(int userid, string token);
    }
}
