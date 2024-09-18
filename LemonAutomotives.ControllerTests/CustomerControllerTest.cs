using AutoFixture;
using FluentAssertions;
using LemonAutomotives.Core.DTO;
using LemonAutomotives.Core.ServiceContracts;
using LemonAutomotives.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LemonAutomotives.ControllerTests
{
    public class CustomerControllerTest
    {
        private readonly ICustomerService _customerService;
        private readonly Mock<ICustomerService> _customerServiceMock;
        private readonly Fixture _fixture;

        public CustomerControllerTest()
        {
            _fixture = new Fixture();
            _customerServiceMock = new Mock<ICustomerService>();
            _customerService = _customerServiceMock.Object;
        }

        #region Index
        [Fact]
        public async Task Index_ShouldReturnIndexViewWithCustomerList()
        {
            //Arrange
            List<CustomerResponseDto> customerResponseList = _fixture.Create<List<CustomerResponseDto>>();
            CustomerController customerController = new CustomerController(_customerService);

            _customerServiceMock.Setup(s => s.GetFilteredCustomersAsync
            (It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(customerResponseList);

            _customerServiceMock.Setup(s => s.GetFilteredCustomersAsync
            (It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(customerResponseList);

            //Act
            IActionResult result = await customerController.Index(_fixture.Create<string>(), _fixture.Create<string>());

            //Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.ViewData.Model.Should().BeAssignableTo<IEnumerable<CustomerResponseDto>>();
            viewResult.ViewData.Model.Should().Be(customerResponseList);
        }
        #endregion
    }
}
