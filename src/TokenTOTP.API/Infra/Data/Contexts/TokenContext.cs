using Microsoft.EntityFrameworkCore;

namespace TokenTOTP.API.Infra.Data.Contexts
{
    public class TokenContext : DbContext
    {
        //public DbSet<Ticket> Tickets { get; set; }
        //public DbSet<Note> Notes { get; set; }
        //public DbSet<Status> Status { get; set; }

        public TokenContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new TicketModelConfiguration());
            //modelBuilder.ApplyConfiguration(new NodeModelConfiguration());
            //modelBuilder.ApplyConfiguration(new StatusModelConfiguration());
        }
    }
}