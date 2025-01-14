using ExchangeRate.Application.Models.Identity;
using ExchangeRate.Domain.Primitive.Result;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<ResultT<AuthResponse>> Login(AuthRequest request);
        Task<ResultT<RegistrationResponse>> Register(RegistrationRequest request);
        Task<ResultT<object>> Logout(HttpRequest request);
        Task<bool> IsTokenBlacklisted(string token);
    }
}
