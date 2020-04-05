using Microsoft.EntityFrameworkCore;
using TokenTOTP.Domain.Model;
using TokenTOTP.Infra.Data.Configurations;

namespace TokenTOTP.Infra.Data.Contexts
{
    public class TokenContext : DbContext
    {
        public DbSet<Totp> Totp { get; set; }

        public TokenContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TotpConfiguration());
        }
    }
}