using Application.Features.Money.Commands.GetConvertedMoney;
using ExchangeRate.Application.Contracts.Identity;
using ExchangeRate.Application.Models.Identity;
using ExchangeRate.Domain.Primitive.Result;
using Microsoft.AspNetCore.Mvc;
using ExchangeRate.Domain.Primitive.Result;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authenticationService;

        public AuthController(IAuthService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            return Ok(await _authenticationService.Login(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            return Ok(await _authenticationService.Register(request));
            //var result = await _authenticationService.Register(request);
            //return result.Match(
            //    onSuccess: Ok,
            //    onFailure: Problem
            //);

        }
    }
}
