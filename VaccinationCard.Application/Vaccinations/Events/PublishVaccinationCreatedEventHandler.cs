using MediatR;
using VaccinationCard.Application.Abstractions.Messaging;
using VaccinationCard.Application.Vaccinations.IntegrationEvents;
using VaccinationCard.Domain.Events;

namespace VaccinationCard.Application.Vaccinations.Events
{
    public class PublishVaccinationCreatedEventHandler : INotificationHandler<VaccinationCreatedEvent>
    {
        private readonly IMessageBus _messageBus;

        public PublishVaccinationCreatedEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task Handle(VaccinationCreatedEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new VaccinationCreatedIntegrationEvent(
                notification.VaccinationId,
                notification.PersonId,
                notification.VaccineId,
                notification.Dose,
                notification.OccurredAt
            );

            await _messageBus.PublishAsync(integrationEvent, cancellationToken);
        }
    }
}
