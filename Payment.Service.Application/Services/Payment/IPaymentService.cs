using Payment.Service.Application.Dto.Payment;

namespace Payment.Service.Application.Services.Payment
{
    public interface IPaymentService
    {
        void Pay(string username);

        ICollection<PaymentDto> GetPayments(string username);
    }
}
