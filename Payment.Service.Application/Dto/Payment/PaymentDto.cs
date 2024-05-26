namespace Payment.Service.Application.Dto.Payment
{
    public class PaymentDto
    {
        public Guid Id { get; set; }

        public string ServiceName { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}