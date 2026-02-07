using FluentAssertions;
using VaccinationCard.Application.Commom.Exceptions;
using VaccinationCard.Application.People.Create;
using VaccinationCard.Domain.Enums;
using VaccinationCard.Tests.Common;

namespace VaccinationCard.Tests.People.Command;

public class CreatePersonCommandHandlerTests
{
    [Fact]
    public async Task Should_create_person_successfully()
    {
        var context = DbContextFactory.Create();
        var handler = new CreatePersonCommandHandler(context);

        var command = new CreatePersonCommand(
            "Pedro",
            "12345678901",
            Gender.Male,
            DateTime.UtcNow.AddYears(-30),
            "pedro@email.com"
        );

        var id = await handler.Handle(command, CancellationToken.None);

        id.Should().NotBeEmpty();
        context.Persons.Should().HaveCount(1);
    }

    [Fact]
    public async Task Should_throw_exception_when_document_already_exists()
    {
        var context = DbContextFactory.Create();
        var handler = new CreatePersonCommandHandler(context);

        await handler.Handle(new CreatePersonCommand(
            "Pessoa 1", "12345678901", Gender.Male,
            DateTime.UtcNow.AddYears(-20), "a@email.com"
        ), CancellationToken.None);

        Func<Task> act = async () =>
            await handler.Handle(new CreatePersonCommand(
                "Pessoa 2", "12345678901", Gender.Female,
                DateTime.UtcNow.AddYears(-25), "b@email.com"
            ), CancellationToken.None);

        await act.Should().ThrowAsync<ConflictException>();
    }

}
