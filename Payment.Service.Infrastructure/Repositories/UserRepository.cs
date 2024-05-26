using Payment.Service.Domain.Abstractions;
using Payment.Service.Domain.Entities;
using Payment.Service.Infrastructure.Extensions;
using Payment.Service.Infrastructure.Persistense.Context;

namespace Payment.Service.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly PaymentDbContext _context;
        private const short maxBadRequest = 3;

        public UserRepository(PaymentDbContext context) : base(context)
        {
            _context = context;
        }

        public bool CheckPassword(string username, string password)
        {
            
            var user = _context.Users
                               .FirstOrDefault(x => x.UserName.Equals(username) && x.BadRequestCount < maxBadRequest);

            if (user != null)
            {
                string hashPassword = PasswordHasher.HashPassword(password, user.Solt);
                var isEqual = user.Password.Equals(hashPassword);

                if (isEqual)
                    return true;

                user.BadRequestCount = user.BadRequestCount++;

                Update(user);
            }
            _context.SaveChanges();
            return false;
        }

        public override void Add(User entity)
        {
            
            entity.Solt = PasswordHasher.GenerateSolt();
            entity.Password = PasswordHasher.HashPassword(entity.Password, entity.Solt);
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public User GetByName(string username)
        {
            return _context.Users.FirstOrDefault(x => x.UserName.Equals(username));
        }
    }
}