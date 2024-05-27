using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using PhoneNumber = Twilio.Types.PhoneNumber;

public class TwilioSmsVerification
{
    private readonly string _twilioAccountSid;
    private readonly string _twilioAuthToken;
    private readonly string _twilioPhoneNumber;

    public TwilioSmsVerification()
    {
        IConfiguration configuration = LoadConfiguration();
        _twilioAccountSid = configuration["Twilio:AccountSid"] ?? "";
        _twilioAuthToken = configuration["Twilio:AuthToken"] ?? "";
        _twilioPhoneNumber = configuration["Twilio:PhoneNumber"] ?? "";

        TwilioClient.Init(_twilioAccountSid, _twilioAuthToken);
    }

    private IConfiguration LoadConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
    }

    public async Task<bool> SendVerificationCodeAsync(string phoneNumber, string verificationCode)
    {
        try
        {
            var message = MessageResource.Create(
                body: $"Your Getnet ride phone verification code is {verificationCode}",
                from: new PhoneNumber(_twilioPhoneNumber),
                to: new PhoneNumber(phoneNumber)
            );

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending verification code: {ex.Message}");
            return false;
        }
    }
}
