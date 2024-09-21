using LemonAutomotives.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace LemonAutomotives.UI.Controllers
{
    [Route("[controller]")]
    public class QSCRController : Controller
    {
        private readonly IQSCRService _QSCRService;

        public QSCRController(IQSCRService QSCRService) 
        {
            _QSCRService = QSCRService;
        }

        [Route("[action]")]
        public async Task<IActionResult> Index(DateTime startDate, DateTime endDate, Guid? salespersonId)
        {
            var report = await _QSCRService.GetQuarterlyCommissionReportAsync(startDate, endDate, salespersonId);
            return View(report);
        }
    }
}
