using Microsoft.AspNetCore.Mvc;

namespace WebPortal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}