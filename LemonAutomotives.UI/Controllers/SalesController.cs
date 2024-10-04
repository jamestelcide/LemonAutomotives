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
                { nameof(SalesResponseDto.SalespersonID), "SalespersonID" },
                { nameof(SalesResponseDto.ProductID), "ProductID" },
                { nameof(SalesResponseDto.CustomerID), "CustomerID" },
                { nameof(SalesResponseDto.SalesDate), "Sales Date" },
                { nameof(SalesResponseDto.PriceSold), "Price Sold" },
                { nameof(SalesResponseDto.Commission), "Commission" },
                { nameof(SalesResponseDto.CommissionEarnings), "CommissionEarnings" }
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
