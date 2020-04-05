using Responses;
using System.Threading;
using System.Threading.Tasks;

namespace TokenTOTP.API.HostedServices.Handlers
{
    public interface IResultHandler<in T> where T : class
    {
        Task HandleErrorAsync(IError error, string correlationId, CancellationToken cancellationToken);

        Task HandleAsync(T message, string correlationId, CancellationToken cancellationToken);
    }
}