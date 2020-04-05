using MediatR;
using Responses;

namespace TokenTOTP.API.Domain.Model.View
{
    public class ValidateTokenRequest : Shared.ViewModel.ValidateTokenRequest, IRequest<Result>
    {
    }
}