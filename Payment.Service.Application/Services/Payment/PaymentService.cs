using AutoMapper;
using Payment.Service.Application.Dto.Payment;
using Payment.Service.Domain.Abstractions;

namespace Payment.Service.Application.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper) 
        { 
            _paymentRepository= paymentRepository;
            _mapper= mapper;
        }

        public ICollection<PaymentDto> GetPayments(string username)
        {
           var payments = _paymentRepository.GetPayments(username);
           var result = _mapper.Map<List<PaymentDto>>(payments);
           return result;
        }

        public void Pay(string username)
        {
            _paymentRepository.Pay(username);
        }
    }
}