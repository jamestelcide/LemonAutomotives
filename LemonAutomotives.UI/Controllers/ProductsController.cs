using LemonAutomotives.Core.DTO;
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
                { nameof(ProductResponseDto.ProductName), "Product Name"},
                { nameof(ProductResponseDto.ProductManufacturer), "Product Name"},
                { nameof(ProductResponseDto.ProductModel), "Product Name"},
                { nameof(ProductResponseDto.ProductPurchasePrice), "Product Name"},
                { nameof(ProductResponseDto.ProductSalePrice), "Product Name"},
                { nameof(ProductResponseDto.ProductQty), "Product Name"},
                { nameof(ProductResponseDto.ProductCommission), "Product Name"}
            };
            return View(productList);
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
            await _productsService.UpdateProductAsync(productUpdateRequest);

            return RedirectToAction("Index");
        }
    }
}
