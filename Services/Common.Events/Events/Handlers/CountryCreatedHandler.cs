using System.Threading.Tasks;
using Services.Country.Data;
using Services.Country.Messages;
using Shared.Kafka.Consumer;

namespace Common.Events.Events.Handlers
{
    public class CountryCreatedHandler : IKafkaHandler<string, User>
    {
        private readonly CountryDBContext _dbContext;

        public CountryCreatedHandler(CountryDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task HandleAsync(string key, User value)
        {
            _dbContext.Countrys.Add(new Country.Data.Country
            {
                Id = value.Id,
                Email = value.Email,
                FirstName = value.FirstName,
                LastName = value.LastName,
            });

            await _dbContext.SaveChangesAsync();
        }
    }
}