using RideBackend.Domain.Common;
using System.Collections.Generic;

namespace RideBackend.Domain.Models
{
    public class VerificationCodes : BaseEntity
    {
        public long VerifierId {  get; set; }
        public string Phone { get; private set; } = string.Empty;
        public string VerificationCode { get; private set; } = string.Empty;

      
        public static VerificationCodes Create(string phone, string code,long verifierId)
        {
            return new VerificationCodes
            {
                Phone = phone,
                VerificationCode = code,
                VerifierId = verifierId
            };
        }
    }
}
