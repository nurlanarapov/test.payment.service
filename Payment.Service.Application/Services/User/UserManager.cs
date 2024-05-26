using Payment.Service.Application.Dto.User;
using Payment.Service.Domain.Abstractions;
using domain = Payment.Service.Domain.Entities;

namespace Payment.Service.Application.Services.User
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        public UserManager(IUserRepository userRepository) 
        { 
            _userRepository= userRepository;
        }

        public void Create(UserInfoDto userInfo)
        {
            domain.User user = _userRepository.GetByName(userInfo.UserName);

            if (user == null)
                user = new domain.User(userInfo.UserName, userInfo.Name, userInfo.Password);
            else
                throw new ArgumentException("Такой пользователь уже существует");

            _userRepository.Add(user);
        }
    }
}