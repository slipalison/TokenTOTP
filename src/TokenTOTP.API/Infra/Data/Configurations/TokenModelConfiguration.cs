using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokenTOTP.API.Infra.Data.Configurations
{
    //public class TicketModelConfiguration : IEntityTypeConfiguration<Ticket>
    //{
    //    public void Configure(EntityTypeBuilder<Ticket> builder)
    //    {
    //        builder.ToTable("Ticket");

    //        builder.HasKey(p => p.Id).HasName("PK_ServiceDesk_TicketId");

    //        builder.Property(p => p.Id).HasColumnName("TicketId");

    //        builder.Property(p => p.IdentificationDocument).IsRequired().HasColumnType("varchar(11)");
    //        builder.Property(p => p.CreateDate).IsRequired().HasColumnType("datetime(4)");

    //        builder.Property(p => p.CloseDate).IsRequired(false).HasColumnType("datetime(4)");

    //        builder.Property(p => p.AttendantEmail).HasColumnType("varchar(255)");
    //        builder.Property(p => p.AttendantFullName).HasColumnType("varchar(255)");
    //        builder.Property(p => p.AttendantID).HasColumnType("char(36)");

    //        builder.Property(p => p.StatusId).IsRequired().HasDefaultValue(1);

    //        builder
    //            .HasOne(a => a.Status)
    //            .WithMany(x => x.Tickets)
    //            .HasForeignKey(x => x.StatusId)
    //            .OnDelete(DeleteBehavior.Restrict)
    //            .HasConstraintName("FK_Ticket_Status_StatusId");
    //    }
    //}
}