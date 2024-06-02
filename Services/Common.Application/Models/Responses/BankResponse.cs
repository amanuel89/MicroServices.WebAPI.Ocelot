namespace CommonService.Application.Models
{

    public class BankResponseDTO
    {
        public long Id { get; set; }
        public string Name { get;  set; } = string.Empty;
        public string AccountName { get;  set; } = string.Empty;
        public string AccountNumber { get;  set; } = string.Empty;
        public string BankLogo { get; set; } = string.Empty; 
        public RecordStatus RecordStatus { get; set; }
        //public virtual List<Transaction> Transaction { get; set; }
    }
}
