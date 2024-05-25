using RideBackend.Domain.Common;

namespace RideBackend.Domain.Models
{
    public class Transaction : BaseEntity
    {
        public double Amount { get; private set; }
        public string Image { get; private set; } = string.Empty;
        public string Reference { get; private set; } = string.Empty;
        public string DepositedBy { get; private set; } = string.Empty;
        public long DriverId { get;  set; }
        public TransactionStatus Status { get; private set; }
        public long BankId { get; set; }
        public virtual Bank Bank { get;  set; }

        public static Transaction Create(
            double amount,
            string image,
            string reference,
            string depositedBy,
            TransactionStatus status
           )
        {
            return new Transaction
            {
                Amount = amount,
                Image = image,
                Reference = reference,
                DepositedBy = depositedBy,
                Status = status,
            };
        }
    }
}
