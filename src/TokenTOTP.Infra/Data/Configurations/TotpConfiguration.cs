using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokenTOTP.Domain.Model;

namespace TokenTOTP.Infra.Data.Configurations
{
    public class TotpConfiguration : IEntityTypeConfiguration<Totp>
    {
        public void Configure(EntityTypeBuilder<Totp> builder)
        {
            builder.ToTable("Totp");

            builder.HasKey(p => p.Id);
        }
    }
}