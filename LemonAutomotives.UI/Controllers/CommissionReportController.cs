using Microsoft.AspNetCore.Mvc;

namespace LemonAutomotives.UI.Controllers
{
    [Route("[controller]")]
    public class CommissionReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
