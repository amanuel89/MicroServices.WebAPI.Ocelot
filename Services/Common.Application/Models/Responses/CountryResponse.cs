using System.ComponentModel.DataAnnotations;

namespace CommonService.Application.Models
{

    public class CountryListResponse
    {
        public long Id { get; set; }
        public string Name { get;  set; } = string.Empty;
        public string PoliticalName { get;  set; } = string.Empty;
        public long? Continent { get;  set; }=long.MinValue;
        public string TelephoneCode { get;  set; } = string.Empty;
        public string TimeZone { get;  set; } = string.Empty;
        public string Nationality { get;  set; } = string.Empty;
        public string CountryCode { get;  set; } = string.Empty;
        public bool IsActive { get;  set; } = true;
        public string TrxnUnit { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
    }

    public class CountrySingleResponse
    {
        public long Id { get; set; }
        public string Name { get;  set; } = string.Empty;
        public string PoliticalName { get;  set; } = string.Empty;
        public long? Continent { get;  set; } = long.MinValue;
        public string TelephoneCode { get;  set; } = string.Empty;
        public string TimeZone { get;  set; } = string.Empty;
        public string Nationality { get;  set; } = string.Empty;
        public string CountryCode { get;  set; } = string.Empty;
        public bool IsActive { get;  set; } = true;
        public string TrxnUnit { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
        public DateTime CreatedDate { get; private set; } = DateTime.Now;
        public string CreatedBy { get; private set; } = string.Empty;
        public DateTime LastUpdateOn { get; private set; } = DateTime.Now;
        public string LastUpdateBy { get; private set; } = string.Empty;
    }
}
