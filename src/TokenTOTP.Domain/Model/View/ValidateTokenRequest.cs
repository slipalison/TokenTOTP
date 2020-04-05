using MediatR;
using Responses;

namespace TokenTOTP.Domain.Model.View
{
    public class ValidateTokenRequest : Shared.ViewModel.ValidateTokenRequest, IRequest<Result>
    {
    }
}