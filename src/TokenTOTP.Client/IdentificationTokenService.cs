using Flurl.Http;
using Responses;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TokenTOTP.Shared.ViewModel;

namespace TokenTOTP.Client
{
    public class IdentificationTokenService : IIdentificationTokenService
    {
        private readonly IFlurlClient _client;

        public IdentificationTokenService(HttpClient httpClient) => _client = new FlurlClient(httpClient);

        public async Task<Result<TokenResponse, IError>> CreateToken(string version, string correlationId, string tokenType, string seed, int? timeToLive, CancellationToken cancellationToken)
        {
            try
            {
                return await $"/api/V{version}/IdentificationToken/Create"
                    .WithClient(_client)
                    .WithHeader("X-Correlation-ID", correlationId)
                    .PostJsonAsync(new CreateTokenCommand(tokenType, timeToLive, seed), cancellationToken)
                    .ReceiveResult<TokenResponse, IError>();
            }
            catch (FlurlHttpException ex)
            {
                return Result.Fail<TokenResponse, IError>(await ex.GetResponseJsonAsync<AggregatedError>());
            }
        }

        public async Task<Result> ValidateToken(string version, string correlationId, string token, string hash, CancellationToken cancellationToken)
        {
            try
            {
                return await $"/api/V{version}/IdentificationToken/Validate"
                    .WithClient(_client)
                    .WithHeader("Accept", "application/json")
                    .WithHeader("X-Correlation-ID", correlationId)
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