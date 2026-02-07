using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Application.Vaccines.Create;
using VaccinationCard.Application.Vaccines.Delete;
using VaccinationCard.Application.Vaccines.Queries;

namespace VaccinationCard.Api.Controllers
{
    [ApiController]
    [Route("api/vaccines")]
    public class VaccinesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VaccinesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVaccineCommand command, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(Create), new { id }, id);
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllVaccinesQuery(), cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteVaccineCommand(id));
            return NoContent();
        }
    }
}
