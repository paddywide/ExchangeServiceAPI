using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Money.Commands.GetConvertedMoney;
using ExchangeRate.Domain.Primitive.Result;
using Core.Models.Response;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeServiceController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public ExchangeServiceController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        //below works for using Exception Middleware
        //[HttpPost("")]
        //public async Task<IActionResult> GetExchangeRate(GetExchangeRateCommand convertRequest)
        //{
        //    var result = await _mediator.Send(convertRequest);
        //    return Ok(result);
        //}

        [HttpPost("")]
        [Authorize(Roles = "Visitor")]
        public async Task<IActionResult> GetExchangeRate(GetExchangeRateCommand convertRequest)
        {
            var result = await _mediator.Send(convertRequest);
            return result.Match(
                        onSuccess: Ok,
                        onFailure: Problem
                    );
        }
        [HttpGet("")]
        public async Task<IActionResult> GetExchangeRateJson()
        {
            return Ok(new CurrencyConvertResponse());
        }


        [HttpGet("GetVersion")]
        public async Task<IActionResult> GetVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            // Get last modified time in UTC
            var utcBuildDate = System.IO.File.GetLastWriteTimeUtc(assembly.Location);

            // Get time zone from appsettings.json (Default: UTC)
            var timeZoneId = _configuration["VersionInfo:TimeZone"] ?? "UTC";
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            // Convert UTC to desired time zone
            var localBuildDate = TimeZoneInfo.ConvertTimeFromUtc(utcBuildDate, timeZone);

            return Ok(new
            {
                Version = version,
                BuildDate = localBuildDate.ToString("yyyy-MM-dd HH:mm:ss zzz")
            });
        }
    }
}
