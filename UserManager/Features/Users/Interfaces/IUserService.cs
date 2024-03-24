using UserManager.Contracts.Responses;
using UserManager.Features.Users.Requests;

namespace UserManager.Features.Users.Interfaces
{
    public interface IUserService
    {
        Task<ResponseApi> CreateUser(CreateUserRequest request);
    }
}
