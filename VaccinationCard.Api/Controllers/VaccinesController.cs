using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Application.Vaccines.Create;

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
    }
}
