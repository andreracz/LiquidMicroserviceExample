using Liquid.Core.Configuration;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using Liquid.Messaging.Azure;
using Liquid.Messaging.Azure.Attributes;
using Liquid.Messaging.Configuration;
using Microsoft.Extensions.Logging;
using OltivaFlix.Domain.Messages.Publishers;
using System.Collections.Generic;

namespace OltivaFlix.Infra.MessageBroker.Services
{
    [ServiceBusProducer("OltivaFlix", "movietopic")]
    public class EnqueueMessage : ServiceBusProducer<MovieAudience>
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
