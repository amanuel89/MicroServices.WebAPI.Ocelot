using Microsoft.Extensions.Options;
using RabbitMq.Shared.Messaging;

namespace Common.Application.Messaging
{
    public class CountryDeletedMessagePublisher : MessagePublisherBase<Country>
    {
        protected override string Subject => "CountryDeleted";

        public CountryDeletedMessagePublisher(IOptions<RabbitMqConfiguration> options) : base(options)
        {
        }
    }
}