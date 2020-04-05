using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TokenTOTP.API.Infra.Data.Contexts
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TokenContextDesingContext : IDesignTimeDbContextFactory<TokenContext>
    {
        public TokenContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TokenContext>();
            optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=Token;Uid=root;Pwd=Fk*(%cS7G+DB?:bK;");

            return new TokenContext(optionsBuilder.Options);
        }
    }
}