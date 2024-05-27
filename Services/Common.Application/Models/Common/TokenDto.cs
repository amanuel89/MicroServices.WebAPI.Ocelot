namespace Common.Application.Models.Common
{
    public class ValidateAllRequest
    {
        public string AccessToken { get; set; }
        public string IdToken { get; set; }
        public string ApiResource { get; set; }
        public string ClientResource { get; set; }
        public long ServiceId { get; set; }
    }
    public class UserTokenValidationResponse
    {
        public string ClientId { get; set; }
        public string UserId { get; set; }
    }
    public class ValidateTokenRequest
    {
        public string AccessToken { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class TelebirrAccessTokenResponse
    {
        public string AccessToken { get; set; }
    }

}
