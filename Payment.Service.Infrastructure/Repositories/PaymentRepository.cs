using Payment.Service.Domain.Abstractions;
using domain = Payment.Service.Domain.Entities;
using Payment.Service.Infrastructure.Persistense.Context;

namespace Payment.Service.Infrastructure.Repositories
{
    public class PaymentRepository : BaseRepository<domain.Payment>, IPaymentRepository
    {
        private readonly PaymentDbContext _context;

        public PaymentRepository(PaymentDbContext context) : base(context)
        {
            _context = context;
        }

        public ICollection<domain.Payment> GetPayments(string username)
        {
            return _context.Payments
                           .Where(x => x.User.Name == username)
                           .ToList();
        }

        public void Pay(string username)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var user = _context.Users.FirstOrDefault(x => x.Name == username);
                    domain.Payment payment = new domain.Payment();


                    if (user?.Balance > payment.Amount)
                    {
                        payment.ServiceName = "Agent Name";
                        payment.UserId = user.Id;
                        _context.Add(payment);

                        user.Balance = user.Balance - payment.Amount;
                        _context.Users.Update(user);

                        _context.SaveChanges();
                        transaction.Commit();
                    }
                    else
                    {
                        throw new ArgumentException("Не хватает денег на счету");
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}