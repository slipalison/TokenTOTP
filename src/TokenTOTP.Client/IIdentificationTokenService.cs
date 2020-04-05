using Responses;
using System.Threading;
using System.Threading.Tasks;
using TokenTOTP.Shared.ViewModel;

namespace TokenTOTP.Client
{
    public interface IIdentificationTokenService
    {
        Task<Result<TokenResponse, IError>> CreateToken(string version, string correlationId, string tokenType, string seed, int? timeToLive, CancellationToken cancellationToken);

        Task<Result> ValidateToken(string version, string correlationId, string token, string hash, CancellationToken cancellationToken);
    }
}