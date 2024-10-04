using LemonAutomotives.Core.DTO;
using LemonAutomotives.Core.Exceptions;
using LemonAutomotives.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace LemonAutomotives.UI.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        public ProductsController(IProductsService productsService) 
        {
            _productsService = productsService;
        }

        [Route("[action]")]
        public async Task<IActionResult> Index(string searchBy, string? searchString)
        {
            List<ProductResponseDto> productList = await _productsService.GetFilteredProductsAsync(searchBy, searchString);

            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                { nameof(ProductResponseDto.ProductID), "ProductID"},
                { nameof(ProductResponseDto.ProductName), "Product Name"},
                { nameof(ProductResponseDto.ProductManufacturer), "Manufacturer"},
                { nameof(ProductResponseDto.ProductModel), "Model"},
                { nameof(ProductResponseDto.ProductYear), "Year"},
                { nameof(ProductResponseDto.ProductPurchasePrice), "Purchase Price"},
                { nameof(ProductResponseDto.ProductQty), "Quantity"},
                { nameof(ProductResponseDto.ProductCommission), "Commission"}
            };
            return View(productList);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Create(ProductAddRequestDto productRequest)
        {
            try
            {
                await _productsService.AddProductAsync(productRequest);
                return RedirectToAction("Index");
            }
            catch (DuplicateProductException ex)
            {
                // Handle the exception, for example, show an error message to the user
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Route("[action]/{productID}")]
        public async Task<IActionResult> Edit(Guid productID)
        {
            ProductResponseDto? productResponse = await _productsService.GetProductByIDAsync(productID);
            if (productResponse == null) { return RedirectToAction("Index");  }
            ProductUpdateRequestDto productUpdateRequest = productResponse.ToProductUpdateRequest();
            
            return View(productUpdateRequest);
        }

        [HttpPost]
        [Route("[action]/{productID}")]
        public async Task<IActionResult> Edit(ProductUpdateRequestDto productUpdateRequest)
        {
            ProductResponseDto? productResponse = await
                _productsService.GetProductByIDAsync(productUpdateRequest.ProductID);

            if(productResponse == null) { return RedirectToAction("Index"); }

            try
            {
                await _productsService.UpdateProductAsync(productUpdateRequest);
                return RedirectToAction("Index");
            }
            catch (DuplicateProductException ex)
            {
                // Handle the exception, for example, show an error message to the user
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Route("[action]/{productID}")]
        public async Task<IActionResult> Delete(Guid? productID)
        {
            if (!productID.HasValue)
            {
                return RedirectToAction("Index");
            }

            ProductResponseDto? productResponse = await _productsService.GetProductByIDAsync(productID.Value);
            if (productResponse == null) { return RedirectToAction("Index"); }

            return View(productResponse);
        }

        [HttpPost]
        [Route("[action]/{productID}")]
        public async Task<IActionResult> Delete(ProductUpdateRequestDto productUpdateResult)
        {
            ProductResponseDto? productResponse = await
                _productsService.GetProductByIDAsync(productUpdateResult.ProductID);

            if (productResponse == null) { return RedirectToAction("Index"); }

            await _productsService.DeleteProductAsync(productUpdateResult.ProductID);
            return RedirectToAction("Index");
        }
    }
}