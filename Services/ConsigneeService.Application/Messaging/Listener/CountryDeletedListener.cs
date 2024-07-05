using Microsoft.Extensions.Options;
using OrderService.Messaging.Model;
using RabbitMq.Shared.Messaging;
namespace OrderService.Messaging.Listener
{
    /// <summary>
    /// Listener that executes when a Country is deleted.
    /// </summary>
    public class CountryDeletedListener : MessageListenerBase<CountryDeletedModel>
    {
        protected override string Subject => "CountryDeleted";

        public CountryDeletedListener(IOptions<RabbitMqConfiguration> options) : base(options)
        {
         
        }
        
        protected override void HandleMessage(CountryDeletedModel model)
        {
  
        }
    }
}