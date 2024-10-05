using AutoFixture;
using FluentAssertions;
using LemonAutomotives.Core.Domain.Entities;
using LemonAutomotives.Core.Domain.RepositoryContracts;
using LemonAutomotives.Core.DTO;
using LemonAutomotives.Core.ServiceContracts;
using LemonAutomotives.Core.Services;
using Moq;
using System.Linq.Expressions;
using Xunit.Abstractions;

namespace LemonAutomotives.ServiceTests
{
    public class SalesServiceTest
    {
        private readonly ISalesService _salesService;
        private readonly Mock<ISalesRepository> _salesRepositoryMock;
        private readonly ISalesRepository _salesRepository;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        public SalesServiceTest(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _salesRepositoryMock = new Mock<ISalesRepository>();
            _salesRepository = _salesRepositoryMock.Object;
            _salesService = new SalesService(_salesRepository);
            _testOutputHelper = testOutputHelper;
        }

        #region CreateSaleAsync

        [Fact]
        public async Task CreateSaleAsync_ShouldThrowArgumentNullException_WhenSalesAddRequestIsNull()
        {
            // Arrange
            SalesAddRequestDto? request = null;

            // Act
            Func<Task> act = async () => await _salesService.CreateSaleAsync(request);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>().WithMessage("*salesAddRequest*");
        }

        [Fact]
        public async Task CreateSaleAsync_ShouldThrowArgumentNullException_WhenProductIDIsNull()
        {
            // Arrange
            var salesAddRequest = _fixture.Build<SalesAddRequestDto>()
                .With(x => x.ProductID, (string?)null)
                .Create();

            // Act
            Func<Task> act = async () => await _salesService.CreateSaleAsync(salesAddRequest);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>().WithMessage("*salesAddRequest*");
        }

        #endregion

        #region GetAllSalesAsync
        [Fact]
        public async Task GetAllSalesAsync_WithFewSales_ToBeSuccessful()
        {
            List<Sales> sales = new List<Sales>()
            {
                _fixture.Build<Sales>().Without(s => s.Salesperson).Without(s => s.Products).Without(s => s.Customer).Create(),
                _fixture.Build<Sales>().Without(s => s.Salesperson).Without(s => s.Products).Without(s => s.Customer).Create(),
                _fixture.Build<Sales>().Without(s => s.Salesperson).Without(s => s.Products).Without(s => s.Customer).Create(),
                _fixture.Build<Sales>().Without(s => s.Salesperson).Without(s => s.Products).Without(s => s.Customer).Create()
            };

            List<SalesResponseDto> salesResponseListExpected =
                sales.Select(s => s.ToSalesResponse()).ToList();

            _testOutputHelper.WriteLine("Expected:");
            foreach (SalesResponseDto salesResponseFromAdd in salesResponseListExpected)
            {
                _testOutputHelper.WriteLine(salesResponseFromAdd.ToString());
            }

            _salesRepositoryMock.Setup(s => s.GetAllSalesAsync()).ReturnsAsync(sales);

            List<SalesResponseDto> salesListFromGet = await _salesService.GetAllSalesAsync();

            _testOutputHelper.WriteLine("Actual:");
            foreach (SalesResponseDto salesResponseFromGet in salesListFromGet)
            {
                _testOutputHelper.WriteLine(salesResponseFromGet.ToString());
            }

            salesListFromGet.Should().BeEquivalentTo(salesResponseListExpected);
        }
        #endregion

        #region GetFilteredSalesAsync
        [Fact]
        public async Task GetFilteredSalesAsync_SearchBySalespersonID_ToBeSuccessful()
        {
            //Arrange
            List<Sales> sales = new List<Sales>()
            {
                _fixture.Build<Sales>().With(s => s.SalespersonID, "LHUNTER84140").Without(s => s.Salesperson).Without(s => s.Products).Without(s => s.Customer).Create(),
                _fixture.Build<Sales>().With(s => s.SalespersonID, "JSTEWART33126").Without(s => s.Salesperson).Without(s => s.Products).Without(s => s.Customer).Create(),
                _fixture.Build<Sales>().With(s => s.SalespersonID, "DLUCZAK88957").Without(s => s.Salesperson).Without(s => s.Products).Without(s => s.Customer).Create()
            };

            List<SalesResponseDto> salesResponseListExpected =
                sales.Select(s => s.ToSalesResponse()).ToList();

            _testOutputHelper.WriteLine("Expected:");
            foreach (SalesResponseDto salesResponseFromAdd in salesResponseListExpected)
            {
                _testOutputHelper.WriteLine(salesResponseFromAdd.ToString());
            }

            _salesRepositoryMock.Setup(s => s.GetFilteredSalesAsync
            (It.IsAny<Expression<Func<Sales, bool>>>())).ReturnsAsync(sales);

            //Act
            List<SalesResponseDto> salesListFromSearch = await
                _salesService.GetFilteredSalesAsync(nameof(Sales.SalespersonID), "LHUN");

            _testOutputHelper.WriteLine("Actual:");
            foreach (SalesResponseDto salesResponseFromGet in salesListFromSearch)
            {
                _testOutputHelper.WriteLine(salesResponseFromGet.ToString());
            }

            //Assert
            salesListFromSearch.Should().BeEquivalentTo(salesResponseListExpected);
        }
        #endregion

            #region GetSaleByIDAsync
            [Fact]
        public async Task GetSaleByIDAsync_ShouldReturnNull_WhenSaleIDIsInvalid()
        {
            // Arrange
            var saleID = Guid.NewGuid();
            _salesRepositoryMock.Setup(r => r.GetSalesByIDAsync(saleID)).ReturnsAsync((Sales?)null);

            // Act
            var result = await _salesService.GetSaleByIDAsync(saleID);

            // Assert
            result.Should().BeNull();
        }
        #endregion
    }
}
