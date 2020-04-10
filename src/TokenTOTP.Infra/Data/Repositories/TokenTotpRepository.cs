using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokenTOTP.Domain.Model;
using TokenTOTP.Domain.Repositories;
using TokenTOTP.Infra.Data.Contexts;

namespace TokenTOTP.Infra.Data.Repositories
{
    public class TokenTotpRepository : ITokenTotpRepository
    {
        private readonly TokenContext _context;

        public TokenTotpRepository(TokenContext context) => _context = context;

        public Task<Totp> GetTokenAsync(string token, string hash, bool remove = true, CancellationToken cancellationToken = default)
        {
            return _context.Totp.FirstOrDefaultAsync(x => x.Token == token && x.HashTopt == hash);
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
