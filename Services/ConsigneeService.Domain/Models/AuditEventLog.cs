
namespace ConsigneeService.Domain.Models;
public class AuditEventLog
{
    public long Id { get; set; }
    public string UserName { get; private set; }
    public string IpAddress { get; private set; }
    public string URL { get; private set; }
    public string Payload { get; private set; }
    public string StatusCode { get; private set; }
    public DateTime DateTime { get; private set; }

    public static AuditEventLog Add( string userName, string ipAddress, string url, string payload, string statusCode)
    {
        var auditlog = new AuditEventLog
        {
            UserName = userName,
            IpAddress = ipAddress,
            URL = url,
            Payload = payload,
            StatusCode = statusCode,
            DateTime = DateTime.UtcNow
        };
        return auditlog;
    }

}
