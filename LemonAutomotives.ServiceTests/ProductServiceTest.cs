using AutoFixture;
using FluentAssertions;
using LemonAutomotives.Core.Domain.Entities;
using LemonAutomotives.Core.Domain.RepositoryContracts;
using LemonAutomotives.Core.DTO;
using LemonAutomotives.Core.ServiceContracts;
using LemonAutomotives.Core.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace LemonAutomotives.ServiceTests
{
    public class ProductServiceTest
    {
        private readonly IProductsService _productService;

        private readonly Mock<IProductsRepository> _productsRepositoryMock;
        private readonly IProductsRepository _productsRepository;

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        public ProductServiceTest(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _productsRepositoryMock = new Mock<IProductsRepository>();
            _productsRepository = _productsRepositoryMock.Object;
            _productService = new ProductService(_productsRepository);

            _testOutputHelper = testOutputHelper;
        }

        #region GetAllProductsAsync
        [Fact]
        public async Task GetAllProductsAsync_ToBeEmptyList()
        {
            var products = new List<Products>();
            _productsRepositoryMock.Setup(p => p.GetAllProductsAsync())
                .ReturnsAsync(products);

            List<ProductResponseDto> productFromGet = await _productService.GetAllProductsAsync();

            productFromGet.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllProductsAsync_WithFewProducts_ToBeSuccessful()
        {
            List<Products> products = new List<Products>()
            {
                _fixture.Build<Products>().Without(p => p.Discount).Without(p => p.Sales).Create(),
                _fixture.Build<Products>().Without(p => p.Discount).Without(p => p.Sales).Create(),
                _fixture.Build<Products>().Without(p => p.Discount).Without(p => p.Sales).Create(),
                _fixture.Build<Products>().Without(p => p.Discount).Without(p => p.Sales).Create()
            };

            List<ProductResponseDto> productResponseListExpected =
                products.Select(p => p.ToProductResponse()).ToList();

            _testOutputHelper.WriteLine("Expected:");
            foreach (ProductResponseDto productResponseFromAdd in productResponseListExpected)
            {
                _testOutputHelper.WriteLine(productResponseFromAdd.ToString());
            }

            _productsRepositoryMock.Setup(p => p.GetAllProductsAsync()).ReturnsAsync(products);

            List<ProductResponseDto> productListFromGet = await _productService.GetAllProductsAsync();

            _testOutputHelper.WriteLine("Actual:");
            foreach (ProductResponseDto productResponseFromGet in productListFromGet)
            {
                _testOutputHelper.WriteLine(productResponseFromGet.ToString());
            }

            productListFromGet.Should().BeEquivalentTo(productResponseListExpected);
        }
        #endregion

        #region GetFilteredProductsAsync
        [Fact]
        public async Task GetFilteredProductsAsync_EmptySearchText_ToBeSuccessful()
        {
            //Arrange
            List<Products> prdoucts = new List<Products>()
            {
                _fixture.Build<Products>().Without(p => p.Discount).Without(p => p.Sales).Create(),
                _fixture.Build<Products>().Without(p => p.Discount).Without(p => p.Sales).Create(),
                _fixture.Build<Products>().Without(p => p.Discount).Without(p => p.Sales).Create(),
                _fixture.Build<Products>().Without(p => p.Discount).Without(p => p.Sales).Create()
            };

            List<ProductResponseDto> productResponseListExpected =
                prdoucts.Select(p => p.ToProductResponse()).ToList();

            _testOutputHelper.WriteLine("Expected:");
            foreach (ProductResponseDto prdouctResponseFromAdd in productResponseListExpected)
            {
                _testOutputHelper.WriteLine(prdouctResponseFromAdd.ToString());
            }

            _productsRepositoryMock.Setup(p => p.GetFilteredProducts
            (It.IsAny<Expression<Func<Products, bool>>>())).ReturnsAsync(prdoucts);

            //Act
            List<ProductResponseDto> productListFromSearch = await
                _productService.GetFilteredProductsAsync(nameof(Products.ProductName), "");

            _testOutputHelper.WriteLine("Actual:");
            foreach (ProductResponseDto productResponseFromGet in productListFromSearch)
            {
                _testOutputHelper.WriteLine(productResponseFromGet.ToString());
            }

            //Assert
            productListFromSearch.Should().BeEquivalentTo(productResponseListExpected);
        }

        [Fact]
        public async Task GetFilteredProductsAsync_SearchByProductName_ToBeSuccessful()
        {
            //Arrange
            List<Products> products = new List<Products>()
            {
                _fixture.Build<Products>().With(p => p.ProductName, "Jeep Grand Cherokee").Without(p => p.Discount).Without(p => p.Sales).Create(),
                _fixture.Build<Products>().Without(p => p.Discount).Without(p => p.Sales).Create(),
                _fixture.Build<Products>().Without(p => p.Discount).Without(p => p.Sales).Create(),
                _fixture.Build<Products>().Without(p => p.Discount).Without(p => p.Sales).Create()
            };

            List<ProductResponseDto> productResponseListExpected =
                products.Select(p => p.ToProductResponse()).ToList();

            _testOutputHelper.WriteLine("Expected:");
            foreach (ProductResponseDto productResponseFromAdd in productResponseListExpected)
            {
                _testOutputHelper.WriteLine(productResponseFromAdd.ToString());
            }

            _productsRepositoryMock.Setup(s => s.GetFilteredProducts
            (It.IsAny<Expression<Func<Products, bool>>>())).ReturnsAsync(products);

            //Act
            List<ProductResponseDto> productsListFromSearch = await
                _productService.GetFilteredProductsAsync(nameof(Products.ProductName), "jee");

            _testOutputHelper.WriteLine("Actual:");
            foreach (ProductResponseDto productResponseFromGet in productsListFromSearch)
            {
                _testOutputHelper.WriteLine(productResponseFromGet.ToString());
            }

            //Assert
            productsListFromSearch.Should().BeEquivalentTo(productResponseListExpected);
        }
        #endregion

        #region GetProductByIDAsync
        [Fact]
        public async Task GetProductByIDAsync_NullProductID_ToBeNull()
        {
            Guid? productID = null;

            ProductResponseDto? productResponseFromGet = await _productService.GetProductByIDAsync(productID);

            productResponseFromGet.Should().BeNull();
        }

        [Fact]
        public async Task GetProductByIDAsync_ProductID_ToBeSuccessful()
        {
            Products product = _fixture.Build<Products>().Without(p => p.Discount).Without(p => p.Sales).Create();
            ProductResponseDto productResponseExpected = product.ToProductResponse();
            _productsRepositoryMock.Setup(p => p.GetProductsByIDAsync(It.IsAny<Guid>()))
                .ReturnsAsync(product);

            ProductResponseDto? productResponseFromGet = await _productService.GetProductByIDAsync(product.ProductID);

            productResponseFromGet.Should().Be(productResponseExpected);
        }
        #endregion

        #region UpdateProductAsync
        [Fact]
        public async Task UpdateProductAsync_NullProduct_ToBeArgumentNullException()
        {
            //Arange
            ProductUpdateRequestDto? productUpdateRequest = null;

            //Act
            Func<Task> action = async () =>
            {
                await _productService.UpdateProductAsync(productUpdateRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateProductAsync_InvalidProductID_ToBeArgumentException()
        {
            //Arrange
            ProductUpdateRequestDto? productUpdateRequest = _fixture.Build<ProductUpdateRequestDto>().Create();

            //Act
            Func<Task> action = async () =>
            {
                await _productService.UpdateProductAsync(productUpdateRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task UpdateProductAsync_AllProductDetails_ToBeSuccessful()
        {
            //Arrange
            Products product = _fixture.Build<Products>().Without(p => p.Discount).Without(p => p.Sales).Create();
            ProductResponseDto productResponseExpected = product.ToProductResponse();
            ProductUpdateRequestDto productUpdateRequest = productResponseExpected.ToProductUpdateRequest();

            _productsRepositoryMock.Setup(p => p.UpdateProductsAsync(It.IsAny<Products>())).ReturnsAsync(product);
            _productsRepositoryMock.Setup(p => p.GetProductsByIDAsync(It.IsAny<Guid>())).ReturnsAsync(product);

            //Act
            ProductResponseDto productResponseFromUpdate = await _productService.UpdateProductAsync(productUpdateRequest);

            //Assert
            productResponseFromUpdate.Should().Be(productResponseExpected);
        }
        #endregion
    }
}
