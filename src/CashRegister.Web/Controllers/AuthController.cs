using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CashRegister.Web.Models.Auth;
using CashRegister.Web.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CashRegister.Web.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IAuthenticationService _authService;
        private readonly HttpContext _httpContext;
        private readonly ILogger _logger;

        public AuthController(
            IAuthenticationService authService,
            IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory)
        {
            _authService = authService;
            _httpContext = httpContextAccessor.HttpContext;
            _logger = loggerFactory.CreateLogger<AuthController>();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isValid = _authService.IsIdentityValid(model);
            if (!isValid)
            {
                ModelState.AddModelError("", "Invalid username or password!");
                return BadRequest(ModelState);
            }

            var token = _authService.CreateToken(model.UserName);

            // this allows @websanova/vue-auth to catch and register the token
            _httpContext.Response.Headers.Add("Authorization", token);

            return NoContent();
        }

        [HttpGet]
        [Route("refresh")]
        public IActionResult Refresh()
        {
            var token = _httpContext.Request.Headers["Authorization"];
            token = _authService.UpdateToken(token);
            _httpContext.Response.Headers.Add("Authorization", token);
            return NoContent();
        }

        [HttpGet]
        [Route("user")]
        public IActionResult UserInfos()
        {
            var userName = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(new UserDatas {
                UserName = userName,
                Roles = new[] { "admin" }
            });
        }
    }
}