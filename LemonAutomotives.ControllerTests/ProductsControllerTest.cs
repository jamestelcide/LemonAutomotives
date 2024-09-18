using AutoFixture;
using FluentAssertions;
using LemonAutomotives.Core.DTO;
using LemonAutomotives.Core.ServiceContracts;
using LemonAutomotives.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
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
    }
}
