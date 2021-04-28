using Liquid.Core.Configuration;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using Liquid.Messaging.Azure;
using Liquid.Messaging.Configuration;
using Microsoft.Extensions.Logging;
using OltivaFlix.Domain.Messages.Publishers;
using OltivaFlix.Domain.Service;
using System.Collections.Generic;
using System.Threading.Tasks;
using Liquid.Messaging.Azure.Attributes;

namespace OltivaFlix.Infra.MessageBroker.Services
{
    [ServiceBusProducer("OltivaFlix", "movietopic")]
    public class EnqueueMessage : ServiceBusProducer<MovieAudience>, IMovieMessage
    {
        public EnqueueMessage(ILightContextFactory contextFactory, 
            ILightTelemetryFactory telemetryFactory, 
            ILoggerFactory loggerFactory,
            ILightConfiguration<List<MessagingSettings>> messagingSettings) 
            : base(contextFactory, 
                  telemetryFactory,
                  loggerFactory,
                  messagingSettings)
        {
        }  
    }
}
