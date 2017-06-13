using System;
using System.IdentityModel.Tokens.Jwt;
using CashRegister.Web.Models.Auth;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CashRegister.Web.Services.Helpers;
using CashRegister.Web.Models.Settings;
using Microsoft.AspNetCore.Identity;
using CashRegister.Web.Models.DbContext;
using System.Threading.Tasks;

namespace CashRegister.Web.Services.Impl
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationSettings _options;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationService(
            IOptions<AuthenticationSettings> options,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _options = options.Value;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Tuple<bool, SignInResult>> IsIdentityValid(LoginRequest model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            return new Tuple<bool, SignInResult>(result.Succeeded, result);
        }

        public string CreateToken(string userName)
        {
            var securityToken = GetNewToken(userName);
            var encodedToken = JwtHelper.EncodeToken(securityToken);
            return encodedToken;
        }

        public string UpdateToken(string token)
        {
            var decodedToken = JwtHelper.DecodeToken(token);
            var securityToken = GetNewToken(decodedToken.Subject);
            var encodedToken = JwtHelper.EncodeToken(securityToken);
            return encodedToken;
        }

        private JwtSecurityToken GetNewToken(string userName)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                // Subject - https://tools.ietf.org/html/rfc7519#section-4.1.2
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                // JWT ID - https://tools.ietf.org/html/rfc7519#section-4.1.7
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // Issued At - https://tools.ietf.org/html/rfc7519#section-4.1.6
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUniversalTime().ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretKey));

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(_options.Expiration)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

            return jwt;
        }
    }
}
