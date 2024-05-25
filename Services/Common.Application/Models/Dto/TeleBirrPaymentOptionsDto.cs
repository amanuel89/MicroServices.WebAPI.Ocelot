namespace RideBackend.Application.Models
{
    public class TeleBirrPaymentOptionsDto
    {
        public long Id { get; set; }
        public string name { get; set; }
        public string provider { get; set; }
        public string synchronous { get; set; }
        public string singleStep { get; set; }
    }

    public class PaymentApprovalRequestModel
    {
        public string PhoneNumber { get; set; }
        public string TransactionId { get; set; }
        public string TIN { get; set; }
        public string Branch { get; set; }
        public decimal Amount { get; set; }
        public long PaymentMethod { get; set; }
        public string PIN { get; set; }
    }

    public class PaymentConfirmationResponseDto
    {
        public string TransactionReference { get; set; }
        public bool IsSuccessful { get; set; }
        public List<string> ErrorMessages { get; set; }
        public object AdditionalParameters { get; set; }
    }


    public class PaymentRequestModel
    {
        public string PhoneNumber { get; set; }
        public string TransactionId { get; set; }
        public string TIN { get; set; }
        public string Branch { get; set; }
        public decimal Amount { get; set; }
        public long PaymentMethod { get; set; }
    }

    public class PaymentAuthorizationResponseDto
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessages { get; set; }
        public object AdditionalParameters { get; set; }
    }

}

