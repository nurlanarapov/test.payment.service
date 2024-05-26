using Payment.Service.Application.Dto.Auth;

namespace Payment.Service.Application.Services.Auth
{
    public interface IAuthService
    {
        string Login(LoginDto loginDto);
        void Logout(string token);
    }
}