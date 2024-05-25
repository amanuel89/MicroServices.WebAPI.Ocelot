namespace RideBackend.Infrastructure.Configurations;
public static class BaseUrl
{
    public static string Identity() => Environment.GetEnvironmentVariable("Identity_URL") ?? "https://identityapi.getnetsoft.com/";
    public static string TeleBirr() => Environment.GetEnvironmentVariable("TeleBirr_URL") ?? "https://paymentgateway.getnetsoft.com/";
}

public class ServicesUrl
{
    public IdentityService IdentityService { get; set; } = new IdentityService();
    public PaymentService PaymentService { get; set; } = new PaymentService();
}
public class IdentityService
{
    public string ValidateAll { get; set; } = String.Empty;
    public string CreateClaims { get; set; }
    public string CreateUser { get; set; }
    
}

public class PaymentService
{
    public string Authentication { get; set; } = String.Empty;
    public string AuthorizePayment { get; set; }
    public string GetPaymentOptions { get; set; }
    public string Transaction { get; set; }

}