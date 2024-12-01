using MediatR;
using Microsoft.AspNetCore.Mvc;
using Core.Models.Request;

using Application.Query;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeServiceController(ISender sender) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> GetExchangeRate(CurrencyConvertRequest convertRequest)
        {
            var result = await sender.Send(new GetExchangeRateQuery(convertRequest));
            //var convertedResult = await sender.Send(new Convert(result));
            return Ok(result);
        }
    }
}
