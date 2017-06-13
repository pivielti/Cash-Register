using System;
using System.Threading.Tasks;
using CashRegister.Web.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace CashRegister.Web.Services
{
    public interface IAuthenticationService
    {
        Task<Tuple<bool, SignInResult>> IsIdentityValid(LoginRequest model);

        string CreateToken(string userName);

        string UpdateToken(string token);
    }
}
