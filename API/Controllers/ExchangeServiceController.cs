using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Money.Commands.GetConvertedMoney;
using ExchangeRate.Domain.Primitive.Result;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeServiceController : BaseController
    {
        private readonly IMediator _mediator;
        public ExchangeServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //below works for using Exception Middleware
        //[HttpPost("")]
        //public async Task<IActionResult> GetExchangeRate(GetExchangeRateCommand convertRequest)
        //{
        //    var result = await _mediator.Send(convertRequest);
        //    return Ok(result);
        //}  

        [HttpPost("")]
        public async Task<IActionResult> GetExchangeRate(GetExchangeRateCommand convertRequest)
        {
            var result = await _mediator.Send(convertRequest);
            return result.Match(
                        onSuccess: Ok,
                        onFailure: Problem
                    );
        }
    }
}
