using CommonService.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonService.Domain.Models
{
    [Table("Common.Country")]
    public class Country : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; private set; } = string.Empty;
        [MaxLength(100)]
        public string PoliticalName { get; private set; } = string.Empty;
        public long? Continent { get; private set; }
        [MaxLength(10)]
        public string TelephoneCode { get; private set; } = string.Empty; 
        [MaxLength(10)]
        public string TimeZone { get; private set; } = string.Empty; 
        [MaxLength(50)]
        public string Nationality { get; private set; } = string.Empty; 
        [MaxLength(10)]
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