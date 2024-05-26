using Payment.Service.Domain.Entities;

namespace Payment.Service.Domain.Abstractions
{
    public interface IUserRepository : IBaseRepository<User>
    {
        bool CheckPassword(string username, string password);
        User GetByName(string username);
    }
}