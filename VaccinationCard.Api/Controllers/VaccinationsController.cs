using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Application.Vaccinations.Create;
using VaccinationCard.Application.Vaccinations.Delete;

namespace VaccinationCard.Api.Controllers
{
    [ApiController]
    [Route("api/vaccinations")]
    public class VaccinationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VaccinationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVaccinationCommand command, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(Create), new { id }, id);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteVaccinationCommand(id));
            return NoContent();
        }
    }
}
