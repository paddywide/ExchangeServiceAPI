using Application.Exception;
using Azure.Core;
using ExchangeRate.Application.Contracts.Identity;
using ExchangeRate.Application.Models.Identity;
using ExchangeRate.Domain.Errors;
using ExchangeRate.Domain.Primitive.Result;
using Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        // In-memory blacklist for demonstration
        private readonly HashSet<string> _blacklistedTokens = new();

        public AuthService(UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
        }

        public async Task<ResultT<AuthResponse>> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                //throw new Exception($"User with {request.Email} not found.");
                return ConfigurationErrors.NotFound(request.Email);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded == false)
            {
                //throw new Exception($"Credentials for '{request.Email} aren't valid.");
                return ConfigurationErrors.CredentialInvalid(request.Email);
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            var response = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName
            };

            return response;
        }


        public async Task<ResultT<RegistrationResponse>> Register(RegistrationRequest request)
        {
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Vistor");
                return new RegistrationResponse() { UserId = user.Id };
            }
            else
            {
                StringBuilder str = new StringBuilder();
                foreach (var err in result.Errors)
                {
                    str.AppendFormat("•{0}\n", err.Description);
                }

                //throw new BadRequestException($"{str}");
                return ConfigurationErrors.RegisterNotSuccessful(str.ToString());
            }
        }
        public async Task<ResultT<object>> Logout(HttpRequest request)
        {
            var email = request.Body.ToString();
            var token = request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return ConfigurationErrors.EmptyToken(email);
            }

            try
            {
                await AddTokenToBlacklist(token);
                return (new { Message = "Successfully logged out." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ConfigurationErrors.LogoutFail(email, ex.Message);
            }
        }

        private async Task AddTokenToBlacklist(string token)
        {
            _blacklistedTokens.Add(token);
            await Task.CompletedTask;
        }
        public Task<bool> IsTokenBlacklisted(string token)
        {
            return Task.FromResult(_blacklistedTokens.Contains(token));
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();
            long iat = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                //new Claim(JwtRegisteredClaimNames.Iat, iat.ToString()),
            new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
               issuer: _jwtSettings.Issuer,
               audience: _jwtSettings.Audience,
               claims: claims,
               expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
               signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

    }
}
