using CommonService.Domain.Common;

namespace CommonService.Domain.Models
{
    public class Country : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string PoliticalName { get; private set; } = string.Empty;
        public long? Continent { get; private set; }
        public string TelephoneCode { get; private set; } = string.Empty;
        public string TimeZone { get; private set; } = string.Empty;
        public string Nationality { get; private set; } = string.Empty;
        public string CountryCode { get; private set; } = string.Empty;

        public static Country Create(
            string name,
            string politicalName,
            long? continent,
            string telephoneCode,
            string timeZone,
            string nationality,
            string countryCode,
            string remark)
        {
            return new Country
            {
                Name = name,
                PoliticalName = politicalName,
                Continent = continent,
                TelephoneCode = telephoneCode,
                TimeZone = timeZone,
                Nationality = nationality,
                CountryCode = countryCode
            };
        }
    }
}