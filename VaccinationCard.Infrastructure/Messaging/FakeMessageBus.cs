using Microsoft.Extensions.Logging;
using VaccinationCard.Application.Abstractions.Messaging;

namespace VaccinationCard.Infrastructure.Messaging
{
    public class FakeMessageBus : IMessageBus
    {
        private readonly ILogger<FakeMessageBus> _logger;

        public FakeMessageBus(ILogger<FakeMessageBus> logger)
        {
            _logger = logger;
        }

        public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "✉️ Message published to broker: {Message}",
                message);

            return Task.CompletedTask;
        }
    }
}
