namespace CommonService.API.Controllers.V1._0;
public class PaymentController : BaseController
{


    //[HttpGet("GetPaymentMethods")]
    //public async Task<IActionResult> GetPaymentMethods()
    //{
    //    var result = await _mediator.Send(new GetPaymentMethods {  });
    //   return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    //}

    //[HttpPost("ApprovePayment/{OrderCode}")]
    //public async Task<IActionResult> ApprovePayment(string OrderCode,string ConfirmationCode,long paymentOption)
    //{
    //    var result = await _mediator.Send(new ConfirmTransaction {
    //    ConfirmationCode=ConfirmationCode,
    //    OrderCode=OrderCode,
    //    paymentOption=paymentOption
    //    });
    //    return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    //}

    //[HttpPost("AuthorizePayment/{OrderCode}")]
    //public async Task<IActionResult> AuthorizePayment(string OrderCode, long paymentOption)
    //{
    //    var result = await _mediator.Send(new AuthorizePayement
    //    {        
    //        OrderCode = OrderCode,
    //        paymentOption = paymentOption
    //    });
    //    return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result);
    //}

}
