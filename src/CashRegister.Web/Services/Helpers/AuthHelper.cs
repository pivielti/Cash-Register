using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace CashRegister.Web.Services.Helpers
{
    public static class AuthHelper
    {
        public static byte[] GetHash(string password, byte[] salt)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            var hashedPassword = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);
            return hashedPassword;
        }

        public static byte[] GetSalt()
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public static string HashPassword(string password, out string createdSalt)
        {
            var salt = GetSalt();
            createdSalt = Convert.ToBase64String(salt);
            var hash = GetHash(password, salt);
            return Convert.ToBase64String(hash);
        }

        public static string HashPassword(string password, string base64Salt)
        {
            var salt = Convert.FromBase64String(base64Salt);
            var hash = GetHash(password, salt);
            return Convert.ToBase64String(hash);
        }

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
