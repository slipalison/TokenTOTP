using Microsoft.EntityFrameworkCore;
using TokenTOTP.API.Domain.Model;
using TokenTOTP.API.Infra.Data.Configurations;

namespace TokenTOTP.API.Infra.Data.Contexts
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