using MediatR;
using Microsoft.AspNetCore.Mvc;

using Application.Features.Money.Commands.GetConvertedMoney;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeServiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ExchangeServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        public async Task<IActionResult> GetExchangeRate(GetExchangeRateCommand convertRequest)
        {
            var result = await _mediator.Send(convertRequest);
            //var convertedResult = await sender.Send(new Convert(result));
            return Ok(result);
        }
    }
}
