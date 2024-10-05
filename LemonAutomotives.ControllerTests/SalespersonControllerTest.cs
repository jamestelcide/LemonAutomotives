using AutoFixture;
using FluentAssertions;
using LemonAutomotives.Core.DTO;
using LemonAutomotives.Core.ServiceContracts;
using LemonAutomotives.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LemonAutomotives.ControllerTests
{
    public class SalespersonControllerTest
    {
        private readonly ISalespersonService _salespersonService;
        private readonly Mock<ISalespersonService> _salespersonServiceMock;
        private readonly Fixture _fixture;

        public SalespersonControllerTest()
        {
            _fixture = new Fixture();
            _salespersonServiceMock = new Mock<ISalespersonService>();
            _salespersonService = _salespersonServiceMock.Object;
        }

        #region Index
        [Fact]
        public async Task Index_ShouldReturnIndexViewWithSalespersonsList()
        {
            //Arrange
            List<SalespersonResponseDto> salespersonResponseList = _fixture.Create<List<SalespersonResponseDto>>();
            SalespersonController salespersonController = new SalespersonController(_salespersonService);
            
            _salespersonServiceMock.Setup(s => s.GetFilteredSalespersonsAsync
            (It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(salespersonResponseList);

            _salespersonServiceMock.Setup(s => s.GetFilteredSalespersonsAsync
            (It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(salespersonResponseList);

            //Act
            IActionResult result = await salespersonController.Index(_fixture.Create<string>(), _fixture.Create<string>());

            //Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.ViewData.Model.Should().BeAssignableTo<IEnumerable<SalespersonResponseDto>>();
            viewResult.ViewData.Model.Should().Be(salespersonResponseList);
        }
        #endregion

        #region Create
        [Fact]
        public async Task Create_IfNoModelErrors_ToReturnRedirectToIndex()
        {
            //Arrange
            SalespersonAddRequestDto salespersonAddRequest = _fixture.Create<SalespersonAddRequestDto>();
            SalespersonResponseDto salespersonResponse = _fixture.Create<SalespersonResponseDto>();
            
            _salespersonServiceMock.Setup(s => s.AddSalespersonAsync
            (It.IsAny<SalespersonAddRequestDto>())).ReturnsAsync(salespersonResponse);

            SalespersonController salespersonController = new SalespersonController(_salespersonService);

            //Act
            IActionResult result = await salespersonController.Create(salespersonAddRequest);

            //Assert
            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);
            redirectResult.ActionName.Should().Be("Index");
        }
        #endregion

        #region Edit
        [Fact]
        public async Task Edit_IfNoModelErrors_ToReturnRedirectToIndex()
        {
            //Arrange
            SalespersonUpdateRequestDto salespersonUpdateRequest = _fixture.Create<SalespersonUpdateRequestDto>();
            SalespersonResponseDto salespersonResponse = _fixture.Create<SalespersonResponseDto>();

            _salespersonServiceMock.Setup(s => s.UpdateSalespersonAsync
            (It.IsAny<SalespersonUpdateRequestDto>())).ReturnsAsync(salespersonResponse);

            SalespersonController salespersonController = new SalespersonController(_salespersonService);

            //Act
            IActionResult result = await salespersonController.Edit(salespersonUpdateRequest);

            //Assert
            RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);
            redirectResult.ActionName.Should().Be("Index");
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_ValidSalespersonID_ShouldRedirectToIndex()
        {
            // Arrange
            var salespersonID = "RMORRIS88351";
            var salespersonResponse = new SalespersonResponseDto
            {
                SalespersonID = salespersonID
            };

            var salespersonUpdateDto = new SalespersonUpdateRequestDto
            {
                SalespersonID = salespersonID
            };

            // Mock GetSalespersonByIDAsync to return a valid salesperson
            _salespersonServiceMock
                .Setup(s => s.GetSalespersonByIDAsync(salespersonID))
                .ReturnsAsync(salespersonResponse);

            // Mock DeleteSalespersonAsync to complete successfully
            _salespersonServiceMock
                .Setup(s => s.DeleteSalespersonAsync(salespersonID))
                .ReturnsAsync(true);

            var controller = new SalespersonController(_salespersonServiceMock.Object);

            // Act
            var result = await controller.Delete(salespersonUpdateDto);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>();
            var redirectResult = result as RedirectToActionResult;
            redirectResult?.ActionName.Should().Be("Index");
        }

        [Fact]
        public async Task Delete_InvalidSalespersonID_ShouldRedirectToIndex()
        {
            // Arrange
            var salespersonID = "WBUCKS33121";
            var salespersonUpdateDto = new SalespersonUpdateRequestDto
            {
                SalespersonID = salespersonID
            };

            // Mock GetSalespersonByIDAsync to return null (invalid salesperson)
            _salespersonServiceMock
                .Setup(s => s.GetSalespersonByIDAsync(salespersonID))
                .ReturnsAsync((SalespersonResponseDto?)null);

            var controller = new SalespersonController(_salespersonServiceMock.Object);

            // Act
            var result = await controller.Delete(salespersonUpdateDto);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>();
            var redirectResult = result as RedirectToActionResult;
            redirectResult?.ActionName.Should().Be("Index");
        }

        #endregion
    }
}