using Microsoft.AspNetCore.Mvc;

namespace CashRegister.Web.Controllers
{
    public class HomeController : Controller
    {
        [Route("~/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}