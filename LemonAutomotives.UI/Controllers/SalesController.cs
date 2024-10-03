using LemonAutomotives.Core.DTO;
using LemonAutomotives.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace LemonAutomotives.UI.Controllers
{
    [Route("[controller]")]
    public class SalesController : Controller
    {
        private readonly ISalesService _salesService;
        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [Route("[action]")]
        public async Task<IActionResult> Index(string searchBy, string? searchString)
        {
            List<SalesResponseDto> salesList = await
                _salesService.GetFilteredSalesAsync(searchBy, searchString);

            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                { nameof(SalesResponseDto.ProductID), "Product" },
                { nameof(SalesResponseDto.SalespersonID), "Salesperson" },
                { nameof(SalesResponseDto.CustomerID), "Customer" }
            };

            return View(salesList);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Create(SalesAddRequestDto salesRequest)
        {
            await _salesService.CreateSaleAsync(salesRequest);
            return RedirectToAction("Index", "Sales");
        }
    }
}
