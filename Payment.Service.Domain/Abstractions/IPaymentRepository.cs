using entity = Payment.Service.Domain.Entities;

namespace Payment.Service.Domain.Abstractions
{
    public interface IPaymentRepository : IBaseRepository<entity.Payment>
    {
        ICollection<entity.Payment> GetPayments(string username);

        void Pay(string username);
    }
}
