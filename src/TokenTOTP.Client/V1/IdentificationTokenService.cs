using Flurl.Http;
using Responses;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TokenTOTP.Shared.ViewModel;

namespace TokenTOTP.Client.V1
{
    public class IdentificationTokenService : AbstractService, IIdentificationTokenService
    {
        public IdentificationTokenService(IHttpClientFactory httpClientFactory) : base(httpClientFactory, nameof(IdentificationTokenService))
        {
        }

        public async Task<Result<TokenResponse, IError>> CreateToken(string correlationId, string tokenType, string seed, int? timeToLive, CancellationToken cancellationToken)
        {
            try
            {
                return await FlurlRequest("/api/v1/identificationtoken/create", correlationId)
                    .AllowAnyHttpStatus()
                    .PostJsonAsync(new CreateTokenCommand(tokenType, timeToLive, seed), cancellationToken)
                    .ReceiveResult<TokenResponse, IError>();
            }
            catch (FlurlHttpException ex)
            {
                return Result.Fail<TokenResponse, IError>(await ex.GetResponseJsonAsync<IError>());
            }
        }

        public async Task<Result> ValidateToken(string correlationId, string token, string hash, CancellationToken cancellationToken)
        {
            try
            {
                return await FlurlRequest("/api/v1/identificationtoken/validate", correlationId)
                    .AllowAnyHttpStatus()
                    .PostJsonAsync(new ValidateTokenRequest()
                    {
                        Hash = hash,
                        Token = token
                    }, cancellationToken: cancellationToken)
                    .ReceiveResult();
            }
            catch (FlurlHttpException ex)
            {
                return Result.Fail(await ex.GetResponseJsonAsync<Error>());
            }
        }
    }
}