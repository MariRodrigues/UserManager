using UserManager.Features.Authentication.Model;
using UserManager.Model;

namespace UserManager.Features.Authentication.Interfaces
{
    public interface ITokenService
    {
        Token CreateToken(CustomUser usuario, string role);
    }
}
