using System;
using System.IdentityModel.Tokens.Jwt;
using CashRegister.Web.Models.Auth;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CashRegister.Web.Services.Helpers;
using CashRegister.Web.Models.Settings;
using CashRegister.Web.DataAccess;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CashRegister.Web.Services.Impl
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationSettings _options;
        private readonly ApplicationDbContext _context;

        public AuthenticationService(IOptions<AuthenticationSettings> options, ApplicationDbContext context)
        {
            _options = options.Value;
            _context = context;
        }

        public bool IsIdentityValid(LoginModel model)
        {
            var user = _context.Users
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                .SingleOrDefault(x => x.Login == model.UserName);
            if (user == null)
                return false;
            var hash = AuthHelper.HashPassword(model.Password, user.HashSalt);
            return user.PasswordHash == hash;
        }

        public string CreateToken(string userName)
        {
            var securityToken = GetNewToken(userName);
            var encodedToken = AuthHelper.EncodeToken(securityToken);
            return encodedToken;
        }

        public string UpdateToken(string token)
        {
            var decodedToken = AuthHelper.DecodeToken(token);
            var securityToken = GetNewToken(decodedToken.Subject);
            var encodedToken = AuthHelper.EncodeToken(securityToken);
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
