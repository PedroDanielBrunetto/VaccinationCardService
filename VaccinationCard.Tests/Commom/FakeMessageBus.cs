using VaccinationCard.Application.Abstractions.Messaging;

namespace VaccinationCard.Tests.Common;

public class FakeMessageBus : IMessageBus
{
    public List<object> PublishedMessages { get; } = new();

    public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
    {
        PublishedMessages.Add(message!);
        return Task.CompletedTask;
    }
}
