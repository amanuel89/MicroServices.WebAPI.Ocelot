using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Logging;
namespace RideBackend.Application.Services.Helper;
public interface IEmailSender
{
    Task<bool> SendEmailAsync(string message, string subject, string fileName, List<string> toAddress, List<string> ccAddress, byte[] attachement);
}
public class EmailSenderService : IEmailSender
{
    private readonly Settings _emailSettings;
    private readonly string _emailSender = string.Empty;
    private readonly string _emailServer = string.Empty;
    private readonly string _emailPassword = string.Empty;
    private readonly int _emailPort;
    ILogger<EmailSenderService> _logger;
    public EmailSenderService(IOptions<Settings> emailSettings, ILogger<EmailSenderService> logger)
    {
#pragma warning disable CS8601
        _logger = logger;
        _emailSettings = emailSettings.Value;
        _emailSender = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("EMAIL_SENDER")) ? Environment.GetEnvironmentVariable("EMAIL_SENDER") : _emailSettings.EmailSettings.Sender;
        _emailServer = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("EMAIL_SERVER")) ? Environment.GetEnvironmentVariable("EMAIL_SERVER") : _emailSettings.EmailSettings.MailServer;
        _emailPort = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("EMAIL_PORT")) ? Convert.ToInt32(Environment.GetEnvironmentVariable("EMAIL_PORT")) : _emailSettings.EmailSettings.MailPort;
        _emailPassword = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("EMAIL_PASSWORD")) ? Environment.GetEnvironmentVariable("EMAIL_PASSWORD") : _emailSettings.EmailSettings.Password;
#pragma warning restore CS8601
    }
    public async Task<bool> SendEmailAsync(string message, string subject, string fileName, List<string> toAddress, List<string> ccAddress, byte[] attachement)
    {
        try
        {
            if (toAddress == null && ccAddress == null)
                return false;
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(_emailSender, "Getnet Mart")
            };

            if (toAddress != null)
            {
                foreach (var to in toAddress)
                    mail.To.Add(new MailAddress(to));
            }

            if (ccAddress != null)
            {
                foreach (var cc in ccAddress)
                {
                    if (String.IsNullOrEmpty(cc))
                        mail.Bcc.Add(new MailAddress(cc));
                }
            }
            if (attachement != null)
            {
                Attachment attch = new Attachment(new MemoryStream(attachement), fileName + ".pdf", "application/pdf");
                mail.Attachments.Add(attch);
            }
            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            using (SmtpClient smtp = new SmtpClient(_emailServer, _emailPort))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_emailSender, _emailPassword);
                smtp.EnableSsl = true;
                smtp.Timeout = 20000;
                await smtp.SendMailAsync(mail);
                return true;
            }
        }
        catch (Exception ex)
        {
            _logger.LogCritical("Email Sender Exception :" + ex.Message);
            return false;
        }
    }

}

