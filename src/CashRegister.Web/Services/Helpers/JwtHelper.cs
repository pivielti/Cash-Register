using System.IdentityModel.Tokens.Jwt;

namespace CashRegister.Web.Services.Helpers
{
    public static class JwtHelper
    {
        public static string EncodeToken(JwtSecurityToken token)
        {
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static JwtSecurityToken DecodeToken(string token)
        {
            if (token.StartsWith("Bearer "))
            {
                token = token.Substring(7);
            }
            return new JwtSecurityTokenHandler().ReadJwtToken(token);
        }
    }
}
