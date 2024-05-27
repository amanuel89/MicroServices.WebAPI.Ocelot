using ConsigneeService.Application.Services.ServiceIntegration;
using Microsoft.AspNetCore.Http;
using MediatR;
using Common.Application.Models.Common;

namespace ConsigneeService.Application.Queries
{
    public class GetPaymentMethods : IRequest<OperationResult<IEnumerable<TeleBirrPaymentOptionsDto>>>
    {

    }

    public class GetPaymentMethodsHandler : IRequestHandler<GetPaymentMethods, OperationResult<IEnumerable<TeleBirrPaymentOptionsDto>>>
    {
        private readonly IPaymentService _paymentService;

        public GetPaymentMethodsHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<OperationResult<IEnumerable<TeleBirrPaymentOptionsDto>>> Handle(GetPaymentMethods request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IEnumerable<TeleBirrPaymentOptionsDto>>();
            try
            {
                var accessToken = await _paymentService.Authenticate<TelebirrAccessTokenResponse>();
                var options = await _paymentService.GetPaymentOptions(accessToken.AccessToken);
                result.Payload = options;
                return result;
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = "An error occurred while processing the request.";
                return result;
            }
        }
    }
}
