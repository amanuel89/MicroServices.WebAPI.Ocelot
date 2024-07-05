//using CommonService.Application.Models;
//using CommonService.Application.Services.ServiceIntegration;
//using CommonService.Domain.Models;
//using CommonService.Infrastructure.Configurations;

//namespace CommonService.Application.Commands;
//public class AuthorizePayement : IRequest<OperationResult<PaymentConfirmationResponseDto>>
//{
//    public string OrderCode { get; set; }
//    public long paymentOption { get; set; }
//}
//public class AuthorizePayementHandler : IRequestHandler<AuthorizePayement, OperationResult<PaymentConfirmationResponseDto>>
//{
//    private readonly IPaymentService _paymentService;
//    private readonly IRepositoryBase<Order> _order;
//    public AuthorizePayementHandler(IPaymentService paymentService, IRepositoryBase<Order> order)
//    {
//        _paymentService = paymentService;
//        _order = order;
//    }
//    public async Task<OperationResult<PaymentConfirmationResponseDto>> Handle(AuthorizePayement request, CancellationToken cancellationToken)
//    {
//        var result = new OperationResult<PaymentConfirmationResponseDto>();

//        try
//        {
//            var order = _order.Where(x => x.OrderCode == request.OrderCode).Include(x=>x.BillingAddress).FirstOrDefault();
//            if (order == null)
//            {
//                result.AddError(ErrorCode.RecordAlreadyExists, "Order not found.");
//                return result;
//            }

//            if (request.paymentOption == 1)
//            {
//                var payload = new PaymentRequestModel
//                {
//                    PhoneNumber = order.BillingAddress.PhoneNumber,
//                    Amount = order.TotalPrice,
//                    Branch = "OUD000000049",
//                    TIN = "0001643872",
//                    TransactionId = order.OrderCode,
//                    PaymentMethod = request.paymentOption

//                };
//                var accessToken = await _paymentService.Authenticate<TelebirrAccessTokenResponse>();
//                var res = await _paymentService.AuthorizePayment<PaymentAuthorizationResponseDto>(payload, accessToken.AccessToken);
              
//                if (res.IsSuccessful)
//                {
//                    order.Update(PaymentStatus.Pending, order.Progress);
//                    _order.Update(order);
//                }
//                else
//                {
//                    order.Update(PaymentStatus.Failed, order.Progress);
//                    _order.Update(order);
//                    if (res.IsSuccessful == false)
//                    {
//                        result.AddError(ErrorCode.UnknownError, "Something went wrong verfiying your payment.");
//                        return result;
//                    }

//                }
//            }

//            return result;
//        }
//        catch (Exception ex)
//        {
//            result.IsError = true;
//            result.Message = "An error occurred while processing the request.";
//            return result;
//        }
//    }
//}
