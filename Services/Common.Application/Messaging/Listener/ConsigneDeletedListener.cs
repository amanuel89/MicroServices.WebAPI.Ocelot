using Microsoft.Extensions.Options;
using RabbitMq.Shared.Messaging;

namespace Common.Application.Messaging.Listener
{
    /// <summary>
    /// Listener that executes when a Country is deleted.
    /// </summary>
    public class ConsigneDeletedListener : MessageListenerBase<ConsigneDeletedModel>
    {
        protected override string Subject => "ConsigneDeleted";

        public ConsigneDeletedListener(IOptions<RabbitMqConfiguration> options) : base(options)
        {
         
        }
        
        protected override void HandleMessage(ConsigneDeletedModel model)
        {
  
        }
    }
}