using ExchangeRate.Application.Models.Identity;
using ExchangeRate.Domain.Primitive.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegistrationResponse> Register(RegistrationRequest request);
        Task<bool> Logout(string token);
        Task<bool> IsTokenBlacklisted(string token);
    }
}
