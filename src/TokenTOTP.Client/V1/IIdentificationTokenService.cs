using Responses;
using System.Threading;
using System.Threading.Tasks;
using TokenTOTP.Shared.ViewModel;

namespace TokenTOTP.Client.V1
{
    public interface IIdentificationTokenService
    {
        Task<Result<TokenResponse, IError>> CreateToken(string correlationId, string tokenType, string seed, int? timeToLive, CancellationToken cancellationToken);

        Task<Result> ValidateToken(string correlationId, string token, string hash, CancellationToken cancellationToken);
    }
}