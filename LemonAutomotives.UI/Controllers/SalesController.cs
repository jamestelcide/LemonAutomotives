using Microsoft.AspNetCore.Mvc;

namespace LemonAutomotives.UI.Controllers
{
    [Route("[controller]")]
    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
