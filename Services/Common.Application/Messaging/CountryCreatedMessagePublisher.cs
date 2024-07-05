using Microsoft.Extensions.Options;
using RabbitMq.Shared.Messaging;

namespace Common.Application.Messaging
{
    public class CountryCreatedMessagePublisher : MessagePublisherBase<Country>
    {
        protected override string Subject => "CountryCreated";

        public CountryCreatedMessagePublisher(IOptions<RabbitMqConfiguration> options) : base(options)
        {
            
        }
    }
}