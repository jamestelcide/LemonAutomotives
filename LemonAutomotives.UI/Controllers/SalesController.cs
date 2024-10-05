using LemonAutomotives.Core.DTO;
using LemonAutomotives.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LemonAutomotives.UI.Controllers
{
    [Route("[controller]")]
    public class SalesController : Controller
    {
        private readonly ISalesService _salesService;
        private readonly ISalespersonService _salespersonService;
        private readonly IProductsService _productsService;
        private readonly ICustomerService _customerService;
        public SalesController(ISalesService salesService, ISalespersonService salespersonService, 
            IProductsService productsService, ICustomerService customerService)
        {
            _salesService = salesService;
            _salespersonService = salespersonService;
            _productsService = productsService;
            _customerService = customerService;
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
        public async Task<IActionResult> Create()
        {
            var salespersonIDs = await _salespersonService.GetAllSalespersonsAsync();
            var productsIDs = await _productsService.GetAllProductsAsync();
            var customersIDs = await _customerService.GetAllCustomersAsync();
            
            ViewBag.SalespersonIDs = new SelectList(salespersonIDs, "SalespersonID", "SalespersonID");
            ViewBag.ProductsIDs = new SelectList(productsIDs, "ProductID", "ProductID");
            ViewBag.CustomerIDs = new SelectList(customersIDs, "CustomerID", "CustomerID");

            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Create(SalesAddRequestDto salesRequest)
        {
            if (!ModelState.IsValid)
            {
                // If model state is invalid, populate ViewBag again to redisplay the form with errors
                var salespersonIDs = await _salespersonService.GetAllSalespersonsAsync();
                var productsIDs = await _productsService.GetAllProductsAsync();
                var customersIDs = await _customerService.GetAllCustomersAsync();

                ViewBag.SalespersonIDs = new SelectList(salespersonIDs, "SalespersonID", "SalespersonID");
                ViewBag.ProductsIDs = new SelectList(productsIDs, "ProductID", "ProductID");
                ViewBag.CustomerIDs = new SelectList(customersIDs, "CustomerID", "CustomerID");

                return View(salesRequest);
            }

            
            await _salesService.CreateSaleAsync(salesRequest);
            return RedirectToAction("Index", "Sales");
        }

    }
}
