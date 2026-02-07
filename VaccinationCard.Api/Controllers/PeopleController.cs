using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Application.People.Create;

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
    }
}
