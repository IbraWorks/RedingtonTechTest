using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedingtonTechTest.Api.Commands;
using RedingtonTechTest.Api.Requests;

namespace RedingtonTechTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProbabilitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProbabilitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<float>> GetProbability([FromBody] CalculateProbabilityRequest getModel)
        {
            var response = await _mediator.Send(new CalculateProbabilityCommand(getModel.FirstValue, getModel.SecondValue, getModel.CalculationType));
            return Ok(response.Result);
        }
    }
}
