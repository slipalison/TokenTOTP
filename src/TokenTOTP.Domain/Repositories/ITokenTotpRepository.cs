using System.Threading;
using System.Threading.Tasks;
using TokenTOTP.Domain.Model;

namespace TokenTOTP.Domain.Repositories
{
    public interface ITokenTotpRepository
    {
        Task InsertTokenAsync(string hashTopt, string tokenTopt, string consumeToken, long timeToLive, CancellationToken cancellationToken = default);

        Task<Totp> GetTokenAsync(string token, string hash, bool remove = true, CancellationToken cancellationToken = default);
    }
}