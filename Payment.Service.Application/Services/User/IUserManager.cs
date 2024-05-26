using Payment.Service.Application.Dto.User;

namespace Payment.Service.Application.Services.User
{
    public interface IUserManager
    {
        void Create(UserInfoDto userInfo);
    }
}