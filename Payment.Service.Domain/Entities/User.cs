namespace Payment.Service.Domain.Entities
{
    public class User
    {
        private const decimal _initialBalance = 8;

        public User(string Name, string UserName, string Password) 
        {
            this.Name = Name;
            this.UserName = UserName;
            this.Password = Password;
            this.Balance = _initialBalance;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        public string Solt { get; set; }

        public decimal Balance { get; set; }

        public short BadRequestCount { get; set; }

        public List<Payment>? Payments { get; set; }
    }
}