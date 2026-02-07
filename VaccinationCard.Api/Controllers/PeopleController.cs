using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Application.People.Create;
using VaccinationCard.Application.People.Delete;
using VaccinationCard.Application.People.Queries;
using VaccinationCard.Application.Vaccinations.Queries;

namespace VaccinationCard.Api.Controllers
{
    [ApiController]
    [Route("api/people")]
    public class PeopleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PeopleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePersonCommand command, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(Create), new { id }, id);
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllPersonsQuery(), cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id}/vaccination-card")]
        public async Task<IActionResult> GetVaccinationCard(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetVaccinationCardQuery(id), cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeletePersonCommand(id));
            return NoContent();
        }
    }
}
