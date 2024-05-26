namespace Payment.Service.Domain.Entities
{
    public class Payment
    {
        public Payment()
        {
            this.Amount = 1.1m;
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }

        public Guid Id { get; set; }

        public string ServiceName { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        //Связи
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}