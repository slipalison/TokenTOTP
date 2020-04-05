using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TokenTOTP.API.Domain.Model;
using TokenTOTP.API.Infra.Data.Contexts;

namespace TokenTOTP.API.Infra.Data.Repositories
{
    public class TokenTotpRepository : ITokenTotpRepository
    {
        private TokenContext _context;

        public TokenTotpRepository(TokenContext context) => _context = context;

        public async Task<Totp> GetTokenAsync(string token, string hash, bool remove = true, CancellationToken cancellationToken = default)
        {
            return _context.Totp.FirstOrDefault(x => x.Token == token && x.HashTopt == hash);
        }

        public async Task InsertTokenAsync(string hashTopt, string tokenTopt, string consumeToken, long timeToLive, CancellationToken cancellationToken = default)
        {
            var totp = new Totp
            {
                HashTopt = hashTopt,
                Token = tokenTopt,
                ConsumeToken = consumeToken,
                TimeToLive = timeToLive,
                Created = DateTime.UtcNow,

                Deleted = false
            };

            await _context.Totp.AddAsync(totp, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}