using UserManager.Contracts.Responses;
using UserManager.Features.Authentication.Requests;

namespace UserManager.Features.Authentication.Interfaces
{
    public interface ILoginService
    {
        Task<ResponseApi> Login(LoginRequest request);
    }
}
