using AutoFixture;
using FluentAssertions;
using LemonAutomotives.Core.DTO;
using LemonAutomotives.Core.ServiceContracts;
using LemonAutomotives.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LemonAutomotives.ControllerTests
{
    public class SalesControllerTest
    {
        private readonly ISalesService _salesService;
        private readonly ISalespersonService _salespersonService;
        private readonly IProductsService _productsService;
        private readonly ICustomerService _customerService;

        private readonly Mock<ISalesService> _salesServiceMock;
        private readonly Mock<ISalespersonService> _salespersonServiceMock;
        private readonly Mock<IProductsService> _productsServiceMock;
        private readonly Mock<ICustomerService> _customerServiceMock;
        private readonly Fixture _fixture;

        public SalesControllerTest()
        {
            _fixture = new Fixture();
            _salesServiceMock = new Mock<ISalesService>();
            _salespersonServiceMock = new Mock<ISalespersonService>();
            _productsServiceMock = new Mock<IProductsService>();
            _customerServiceMock = new Mock<ICustomerService>();
            _salesService = _salesServiceMock.Object;
            _salespersonService = _salespersonServiceMock.Object;
            _productsService = _productsServiceMock.Object;
            _customerService = _customerServiceMock.Object;
        }

        #region Index
        [Fact]
        public async Task Index_ShouldReturnIndexViewWithSalesList()
        {
            //Arrange
            List<SalesResponseDto> salesResponseList = _fixture.Create<List<SalesResponseDto>>();
            SalesController salesController = new SalesController(_salesService, _salespersonService, _productsService, _customerService);

            _salesServiceMock.Setup(s => s.GetFilteredSalesAsync
            (It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(salesResponseList);

            _salesServiceMock.Setup(s => s.GetFilteredSalesAsync
            (It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(salesResponseList);

            //Act
            IActionResult result = await salesController.Index(_fixture.Create<string>(), _fixture.Create<string>());

            //Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.ViewData.Model.Should().BeAssignableTo<IEnumerable<SalesResponseDto>>();
            viewResult.ViewData.Model.Should().Be(salesResponseList);
        }
        #endregion

        #region Create
        [Fact]
        public async Task Create_ValidSalesRequest_ShouldRedirectToIndex()
        {
            // Arrange
            var salesRequest = _fixture.Create<SalesAddRequestDto>();

            _salesServiceMock.Setup(p => p.CreateSaleAsync(It.IsAny<SalesAddRequestDto>()))
                .ReturnsAsync(_fixture.Create<SalesResponseDto>());

            var salesController = new SalesController(_salesService, _salespersonService, _productsService, _customerService);

            // Act
            IActionResult result = await salesController.Create(salesRequest);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            redirectResult.ActionName.Should().Be("Index");
        }
        #endregion
    }
}
