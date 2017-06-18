using System.Threading.Tasks;
using CashRegister.Web.Models.Auth;

namespace CashRegister.Web.Services
{
    public interface IAuthenticationService
    {
        bool IsIdentityValid(LoginModel model);

        string CreateToken(string userName);

        string UpdateToken(string token);
    }
}
