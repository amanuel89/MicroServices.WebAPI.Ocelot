//using ConsigneeService.Application.Models;
//using ConsigneeService.Application.Services.ServiceIntegration;
//using ConsigneeService.Domain.Models;
//using ConsigneeService.Infrastructure.Configurations;

//namespace ConsigneeService.Application.Commands;
//public class AuthorizePayment : IRequest<OperationResult<PaymentAuthorizationResponseDto>>
//{
//    public string OrderCode { get; set; }
//    public string ConfirmationCode { get; set; }
//    public long paymentOption { get; set; }
//}
//public class AuthorizePaymentHandler : IRequestHandler<AuthorizePayment, OperationResult<PaymentAuthorizationResponseDto>>
//{
//    private readonly IPaymentService _paymentService;
//    private readonly IRepositoryBase<Order> _order;
//    public AuthorizePaymentHandler(IPaymentService paymentService, IRepositoryBase<Order> order)
//    {
//        _paymentService = paymentService;
//        _order = order;
//    }
//    public async Task<OperationResult<PaymentAuthorizationResponseDto>> Handle(AuthorizePayment request, CancellationToken cancellationToken)
//    {
//        var result = new OperationResult<PaymentAuthorizationResponseDto>();

//        try
//        {
//            var order = _order.Where(x => x.OrderCode == request.OrderCode).Include(x=>x.BillingAddress).FirstOrDefault();
//            if (order == null)
//            {
//                result.AddError(ErrorCode.RecordAlreadyExists, "Order not found.");
//                return result;
//            }
//            var payload = new PaymentRequestModel() {
            
//                TIN= "0001643872",
//                Branch= "OUD000000049",
//                TransactionId=order.OrderCode,
//                PaymentMethod=request.paymentOption,
//                PhoneNumber=order.BillingAddress.PhoneNumber,
//                Amount=order.TotalPrice
//            };
//            var accessToken = await _paymentService.Authenticate<TelebirrAccessTokenResponse>();
//            var res = await _paymentService.AuthorizePayment<PaymentAuthorizationResponseDto>(payload, accessToken.AccessToken);
//            if (res.IsSuccessful)
//            {
//                order.Update(PaymentStatus.Completed,order.Progress);
//                _order.Update(order);
//            }
//            else
//            {
//                order.Update(PaymentStatus.Failed, order.Progress);
//                _order.Update(order);
//                //result.AddError(ErrorCode.ValidationError, res.ErrorMessages[0]);
//                return result;
//            }
//            result.Payload = res;
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
