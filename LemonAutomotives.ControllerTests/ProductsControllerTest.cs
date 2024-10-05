using AutoFixture;
using FluentAssertions;
using LemonAutomotives.Core.DTO;
using LemonAutomotives.Core.Exceptions;
using LemonAutomotives.Core.ServiceContracts;
using LemonAutomotives.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace LemonAutomotives.ControllerTests
{
    public class ProductsControllerTest
    {
        private readonly IProductsService _productsService;
        private readonly Mock<IProductsService> _productsServiceMock;
        private readonly Fixture _fixture;

        public ProductsControllerTest()
        {
            _fixture = new Fixture();
            _productsServiceMock = new Mock<IProductsService>();
            _productsService = _productsServiceMock.Object;
        }

        #region Index
        [Fact]
        public async Task Index_ShouldReturnIndexViewWithProductsList()
        {
            //Arrange
            List<ProductResponseDto> productResponseList = _fixture.Create<List<ProductResponseDto>>();
            ProductsController productController = new ProductsController(_productsService);

            _productsServiceMock.Setup(p => p.GetFilteredProductsAsync
            (It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(productResponseList);

            _productsServiceMock.Setup(s => s.GetFilteredProductsAsync
            (It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(productResponseList);

            //Act
            IActionResult result = await productController.Index(_fixture.Create<string>(), _fixture.Create<string>());

            //Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.ViewData.Model.Should().BeAssignableTo<IEnumerable<ProductResponseDto>>();
            viewResult.ViewData.Model.Should().Be(productResponseList);
        }
        #endregion

        #region Create
        [Fact]
        public async Task Create_ValidProductRequest_ShouldRedirectToIndex()
        {
            // Arrange
            var productRequest = _fixture.Create<ProductAddRequestDto>();

            _productsServiceMock.Setup(p => p.AddProductAsync(It.IsAny<ProductAddRequestDto>()))
                .ReturnsAsync(_fixture.Create<ProductResponseDto>());

            var productsController = new ProductsController(_productsService);

            // Act
            IActionResult result = await productsController.Create(productRequest);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            redirectResult.ActionName.Should().Be("Index");
        }

        [Fact]
        public async Task Create_DuplicateProduct_ShouldRedirectToError()
        {
            // Arrange
            var productRequest = _fixture.Create<ProductAddRequestDto>();

            _productsServiceMock.Setup(p => p.AddProductAsync(It.IsAny<ProductAddRequestDto>()))
                .ThrowsAsync(new DuplicateProductException("Duplicate product"));

            var productsController = new ProductsController(_productsService);

            // Mock TempData
            productsController.TempData = new Mock<ITempDataDictionary>().Object;

            // Act
            IActionResult result = await productsController.Create(productRequest);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            redirectResult.ActionName.Should().Be("Error");
            redirectResult.ControllerName.Should().Be("Home");
        }
        #endregion

        #region Edit
        [Fact]
        public async Task Edit_IfNoModelErrors_ToReturnRedirectToIndex()
        {
            //Arrange
            ProductUpdateRequestDto productUpdateRequest = _fixture.Create<ProductUpdateRequestDto>();
            ProductResponseDto productResponse = _fixture.Create<ProductResponseDto>();

            _productsServiceMock.Setup(p => p.UpdateProductAsync
            (It.IsAny<ProductUpdateRequestDto>())).ReturnsAsync(productResponse);

            ProductsController productsController = new ProductsController(_productsService);

            //Act
            IActionResult result = await productsController.Edit(productUpdateRequest);

            //Assert
            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);
            redirectResult.ActionName.Should().Be("Index");
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_ProductNotFound_ShouldRedirectToIndex()
        {
            // Arrange
            var productId = "";

            _productsServiceMock.Setup(p => p.GetProductByIDAsync(productId))
                .ReturnsAsync((ProductResponseDto?)null);

            var productsController = new ProductsController(_productsService);

            // Act
            IActionResult result = await productsController.Delete(productId);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            redirectResult.ActionName.Should().Be("Index");
        }

        [Fact]
        public async Task Delete_ValidProduct_ShouldRedirectToIndexAfterDelete()
        {
            // Arrange
            var productResponse = _fixture.Create<ProductResponseDto>();
            var productUpdateRequest = _fixture.Create<ProductUpdateRequestDto>();
            productUpdateRequest.ProductID = productResponse.ProductID;

            _productsServiceMock.Setup(p => p.GetProductByIDAsync(productUpdateRequest.ProductID))
                .ReturnsAsync(productResponse);

            _productsServiceMock.Setup(p => p.DeleteProductAsync(productUpdateRequest.ProductID));

            var productsController = new ProductsController(_productsService);

            // Act
            IActionResult result = await productsController.Delete(productUpdateRequest);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            redirectResult.ActionName.Should().Be("Index");
        }
        #endregion
    }
}
