using Microsoft.EntityFrameworkCore;
using Payment.Service.Domain.Entities;
using model = Payment.Service.Domain.Entities;

namespace Payment.Service.Infrastructure.Persistense.Context
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                   .HasKey(x => x.Id);

            builder.Entity<User>()
                   .HasIndex(u => u.UserName)
                   .IsUnique();

            builder.Entity<User>()
                .HasMany(c => c.Payments)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<model.Payment> Payments { get; set; }
    }
}