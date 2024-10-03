using LemonAutomotives.Core.Domain.Entities;
using LemonAutomotives.Core.Domain.RepositoryContracts;
using LemonAutomotives.Core.DTO;
using LemonAutomotives.Core.Exceptions;
using LemonAutomotives.Core.ServiceContracts;

namespace LemonAutomotives.Core.Services
{
    public class ProductService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;
        public ProductService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<ProductResponseDto> AddProductAsync(ProductAddRequestDto? productAddRequestDto)
        {
            if (productAddRequestDto == null)
            {
                throw new ArgumentNullException(nameof(productAddRequestDto));
            }

            // Construct the full product name
            string fullProductName = $"{productAddRequestDto.ProductYear} {productAddRequestDto.ProductManufacturer} {productAddRequestDto.ProductModel}";

            // Check if product with the same name already exists
            var existingProducts = await _productsRepository.GetAllProductsAsync();
            if (existingProducts.Any(p => p.ProductName == fullProductName))
            {
                throw new DuplicateProductException($"A product with the name '{fullProductName}' already exists.");
            }

            Products product = productAddRequestDto.ToProducts();

            product.ProductID = Guid.NewGuid();

            product.ProductName = $"{product.ProductYear} {product.ProductManufacturer} {product.ProductModel}";

            await _productsRepository.AddProductAsync(product);
            return product.ToProductResponse();
        }

        public async Task<List<ProductResponseDto>> GetAllProductsAsync()
        {
            List<Products> products = await _productsRepository.GetAllProductsAsync();
            return products.Select(products => products.ToProductResponse()).ToList();
        }

        public async Task<ProductResponseDto?> GetProductByIDAsync(Guid? productID)
        {
            if (productID == null) { return null; }
            Products? productFromResponseList = await _productsRepository.GetProductsByIDAsync(productID);

            if (productFromResponseList == null) { return null; }
            return productFromResponseList.ToProductResponse();
        }

        public async Task<List<ProductResponseDto>> GetFilteredProductsAsync(string searchBy, string? searchString)
        {
            List<Products> products;

            products = searchBy switch
            {
                nameof(ProductResponseDto.ProductName) =>
                await _productsRepository.GetFilteredProducts(p => 
                p.ProductName != null &&
                p.ProductName.Contains(searchString ?? string.Empty)),

                nameof(ProductResponseDto.ProductManufacturer) =>
                await _productsRepository.GetFilteredProducts(p =>
                p.ProductManufacturer != null &&
                p.ProductManufacturer.Contains(searchString ?? string.Empty)),

                nameof(ProductResponseDto.ProductModel) =>
                await _productsRepository.GetFilteredProducts(p =>
                p.ProductModel != null &&
                p.ProductModel.Contains(searchString ?? string.Empty)),

                nameof(ProductResponseDto.ProductYear) =>
                await _productsRepository.GetFilteredProducts(p =>
                p.ProductModel != null &&
                p.ProductModel.Contains(searchString ?? string.Empty)),

                nameof(ProductResponseDto.ProductPurchasePrice) =>
                await _productsRepository.GetFilteredProducts(p =>
                p.ProductPurchasePrice != null &&
                p.ProductPurchasePrice == Convert.ToDouble(searchString)),

                nameof(ProductResponseDto.ProductQty) =>
                await _productsRepository.GetFilteredProducts(p =>
                p.ProductQty == Convert.ToInt32(searchString)),

                nameof(ProductResponseDto.ProductCommission) =>
                await _productsRepository.GetFilteredProducts(p =>
                p.ProductCommission == Convert.ToDouble(searchString)),

                _ => await _productsRepository.GetAllProductsAsync()
            };
            return products.Select(p => p.ToProductResponse()).ToList();
        }

        public async Task<ProductResponseDto> UpdateProductAsync(ProductUpdateRequestDto? productUpdateRequest)
        {
            if(productUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(productUpdateRequest));
            }

            Products? matchingProduct = await _productsRepository.GetProductsByIDAsync(productUpdateRequest.ProductID);
            if (matchingProduct == null)
            {
                throw new ArgumentNullException(nameof(productUpdateRequest.ProductID), "ProductID not found.");
            }

            matchingProduct.ProductName = productUpdateRequest.ProductName;
            matchingProduct.ProductManufacturer = productUpdateRequest.ProductManufacturer;
            matchingProduct.ProductModel = productUpdateRequest.ProductModel;
            matchingProduct.ProductYear = productUpdateRequest.ProductYear;
            matchingProduct.ProductPurchasePrice = productUpdateRequest.ProductPurchasePrice;
            matchingProduct.ProductQty = productUpdateRequest.ProductQty;
            matchingProduct.ProductCommission = productUpdateRequest.ProductCommission;

            matchingProduct.ProductName = $"{productUpdateRequest.ProductYear} {productUpdateRequest.ProductManufacturer} {productUpdateRequest.ProductModel}";

            await _productsRepository.UpdateProductsAsync(matchingProduct);
            return matchingProduct.ToProductResponse();
        }

        public async Task<bool> DeleteProductAsync(Guid? productID)
        {
            if (productID == null)
            {
                throw new ArgumentNullException(nameof(productID));
            }

            Products? product = await _productsRepository.GetProductsByIDAsync(productID.Value);

            if (product == null) { return false; }

            await _productsRepository.DeleteProductByIDAsync(productID.Value);

            return true;
        }
    }
}
