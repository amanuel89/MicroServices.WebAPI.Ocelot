using CommonService.Domain.Common;
using System.Collections.Generic;

namespace CommonService.Domain.Models
{
    public class Bank : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string AccountName { get; private set; } = string.Empty;
        public string AccountNumber { get; private set; } = string.Empty;
        public string BankLogo { get; private set; } = string.Empty;
        public virtual List<Transaction> Transaction { get; set; }

        // Private constructor to enforce object creation through factory method   
        public static Bank Create(string name, string accountName, string accountNumber, string bankLogo)
        {
            var bank= new Bank
            {
                Name = name,
                AccountName = accountName,
                AccountNumber = accountNumber,
                BankLogo = bankLogo
            };

            var validator = new BankValidator();
            var response = validator.Validate(bank);
            if (response.IsValid) return bank;
            var exception = new NotValidException("Bank data is not valid");
            response.Errors.ForEach(vr => exception.ValidationErrors.Add(vr.ErrorMessage));
            throw exception;
        }

        public void Update(string name, string accountName, string accountNumber, string bankLogo)
        {
            this.Name = name;
            this.AccountName = accountName;
            this.AccountNumber = accountNumber;
            this.BankLogo = bankLogo;
        }

    }
}
