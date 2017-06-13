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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;

        public AuthController(
            IAuthenticationService authService,
            IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
            _logger = loggerFactory.CreateLogger<AuthController>();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var loginResult = await _authService.IsIdentityValid(model);
            if (!loginResult.Item1)
            {
                ModelState.AddModelError("", "Invalid username or password!");
                return BadRequest(ModelState);
            }

            var token = _authService.CreateToken(model.UserName);

            // this allows @websanova/vue-auth to catch and register the token
            _httpContextAccessor.HttpContext.Response.Headers.Add("Authorization", token);

            return NoContent();
        }

        [HttpGet]
        [Route("refresh")]
        public IActionResult Refresh()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            token = _authService.UpdateToken(token);
            _httpContextAccessor.HttpContext.Response.Headers.Add("Authorization", token);
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