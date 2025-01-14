using Application.Features.Money.Commands.GetConvertedMoney;
using ExchangeRate.Application.Contracts.Identity;
using ExchangeRate.Application.Models.Identity;
using ExchangeRate.Domain.Primitive.Result;
using Microsoft.AspNetCore.Mvc;
using ExchangeRate.Domain.Primitive.Result;
using Microsoft.AspNetCore.Authorization;
using Api.Controllers;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authenticationService;

        public AuthController(IAuthService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthRequest request)
        {
            //return Ok(await _authenticationService.Login(request));
            var result = await _authenticationService.Login(request);

            return result.Match(
                        onSuccess: Ok,
                        onFailure: Problem
                    );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            //return Ok(await _authenticationService.Register(request));
            var result = await _authenticationService.Register(request);
            return result.Match(
                        onSuccess: Ok,
                        onFailure: Problem
                    );

        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var result = await _authenticationService.Logout(Request);
            return result.Match(
                        onSuccess: Ok,
                        onFailure: Problem
                    );
        }
    }
}
