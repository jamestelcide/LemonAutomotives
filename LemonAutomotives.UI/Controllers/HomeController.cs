using Microsoft.AspNetCore.Mvc;

namespace LemonAutomotives.UI.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
