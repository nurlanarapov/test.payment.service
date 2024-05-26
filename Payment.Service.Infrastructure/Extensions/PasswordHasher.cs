namespace Payment.Service.Infrastructure.Extensions
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password, string solt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, solt);
        }

        public static string GenerateSolt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt();
        }
    }
}