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
    public class SalespersonServiceTest
    {
        private readonly ISalespersonService _salespersonService;

        private readonly Mock<ISalespersonRepository> _salespersonRepositoryMock;
        private readonly ISalespersonRepository _salespersonRepository;

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        public SalespersonServiceTest(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _salespersonRepositoryMock = new Mock<ISalespersonRepository>();
            _salespersonRepository = _salespersonRepositoryMock.Object;

            _salespersonService = new SalespersonService(_salespersonRepository);

            _testOutputHelper = testOutputHelper;
        }

        #region AddSalesperson
        //when we supply null value as SalespersonAddRequest, it should throw ArgumentNullException
        [Fact]
        public async Task AddSalespersonAsync_NullPerson_ToBeArgumentNullException()
        {
            //Arrange
            SalespersonAddRequestDto? salespersonAddRequestDto = null;
            
            //Act
            Func<Task> action = async () =>
            {
                await _salespersonService.AddSalespersonAsync(salespersonAddRequestDto);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        //When we supply null value as SalespersonFirstName, it should throw ArgumentException
        [Fact]
        public async Task AddSalespersonAsync_SalespersonFirstNameIsNull_ToBeArgumentNullException()
        {
            //Arrange
            SalespersonAddRequestDto? salespersonAddRequest = _fixture.Build<SalespersonAddRequestDto>()
                .With(s => s.SalespersonFirstName, null as string)
                .Create();

            Salesperson salesperson = salespersonAddRequest.ToSalesperson();

            //When SalespersonRepository.AddSalespersonAsync is called  it returns the same Salesperson object
            _salespersonRepositoryMock
                .Setup(sMock => sMock.AddSalespersonAsync(It.IsAny<Salesperson>()))
                .ReturnsAsync(salesperson);

            //Act
            Func<Task> action = async () =>
            {
                await _salespersonService.AddSalespersonAsync(salespersonAddRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task AddSalesperonsAsync_AllSalespersonDetails_ToBeSuccessful()
        {
            //Arrange
            SalespersonAddRequestDto? salespersonAddRequest = _fixture.Build<SalespersonAddRequestDto>().Create();
            Salesperson salesperson = salespersonAddRequest.ToSalesperson();
            SalespersonResponseDto salespersonResponseExpected = salesperson.ToSalespersonResponse();

            _salespersonRepositoryMock.Setup(s => s.AddSalespersonAsync(It.IsAny<Salesperson>())).ReturnsAsync(salesperson);

            //Act
            SalespersonResponseDto salespersonResponseFromAdd = await _salespersonService.AddSalespersonAsync(salespersonAddRequest);
            salespersonResponseExpected.SalespersonID = salespersonResponseFromAdd.SalespersonID;

            //Assert
            salespersonResponseFromAdd.SalespersonID.Should().NotBe(Guid.Empty);
            salespersonResponseFromAdd.Should().Be(salespersonResponseExpected);
        }
        #endregion

        #region GetSalespersonByIDAsync
        [Fact]
        public async Task GetSalespersonByIDAsync_NullSalespersonID_ToBeNull()
        {
            Guid? salespersonID = null;

            SalespersonResponseDto? salespersonResponseFromGet = await _salespersonService.GetSalespersonByIDAsync(salespersonID);

            salespersonResponseFromGet.Should().BeNull();
        }

        [Fact]
        public async Task GetSalespersonByIDAsync_SalespersonID_ToBeSuccessful()
        {
            Salesperson salesperson = _fixture.Build<Salesperson>().Without(s => s.Sales).Create();
            SalespersonResponseDto salespersonResponseExpected = salesperson.ToSalespersonResponse();
            _salespersonRepositoryMock.Setup(s => s.GetSalespersonByIDAsync(It.IsAny<Guid>()))
                .ReturnsAsync(salesperson);

            SalespersonResponseDto? salespersonResponseFromGet = await _salespersonService.GetSalespersonByIDAsync(salesperson.SalespersonID);

            salespersonResponseFromGet.Should().Be(salespersonResponseExpected);
        }
        #endregion

        #region GetAllSalespersonsAsync
        [Fact]
        public async Task GetAllSalespersonsAsync_ToBeEmptyList()
        {
            var salesperson = new List<Salesperson>();
            _salespersonRepositoryMock.Setup(s => s.GetAllSalespersonsAsync())
                .ReturnsAsync(salesperson);

            List<SalespersonResponseDto> salespersonFromGet = await _salespersonService.GetAllSalespersonsAsync();

            salespersonFromGet.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllSalespersonsAsync_WithFewSalespersons_ToBeSuccessful()
        {
            List<Salesperson> salespersons = new List<Salesperson>()
            {
                _fixture.Build<Salesperson>().Without(s => s.Sales).Create(),
                _fixture.Build<Salesperson>().Without(s => s.Sales).Create(),
                _fixture.Build<Salesperson>().Without(s => s.Sales).Create(),
                _fixture.Build<Salesperson>().Without(s => s.Sales).Create()
            };

            List<SalespersonResponseDto> salespersonResponseListExpected = 
                salespersons.Select(s => s.ToSalespersonResponse()).ToList();

            _testOutputHelper.WriteLine("Expected:");
            foreach (SalespersonResponseDto salespersonResponseFromAdd in salespersonResponseListExpected)
            {
                _testOutputHelper.WriteLine(salespersonResponseFromAdd.ToString());
            }

            _salespersonRepositoryMock.Setup(s => s.GetAllSalespersonsAsync()).ReturnsAsync(salespersons);

            List<SalespersonResponseDto> salespersonsListFromGet = await _salespersonService.GetAllSalespersonsAsync();

            _testOutputHelper.WriteLine("Actual:");
            foreach (SalespersonResponseDto salespersonResponseFromGet in salespersonsListFromGet)
            {
                _testOutputHelper.WriteLine(salespersonResponseFromGet.ToString());
            }

            salespersonsListFromGet.Should().BeEquivalentTo(salespersonResponseListExpected);
        }
        #endregion

        #region GetFilteredSalespersonsAsync
        [Fact]
        public async Task GetFilteredSalespersonsAsync_EmptySearchText_ToBeSuccessful()
        {
            //Arrange
            List<Salesperson> salespersons = new List<Salesperson>()
            {
                _fixture.Build<Salesperson>().Without(s => s.Sales).Create(),
                _fixture.Build<Salesperson>().Without(s => s.Sales).Create(),
                _fixture.Build<Salesperson>().Without(s => s.Sales).Create(),
                _fixture.Build<Salesperson>().Without(s => s.Sales).Create()
            };

            List<SalespersonResponseDto> salespersonResponseListExpected = 
                salespersons.Select(s => s.ToSalespersonResponse()).ToList();

            _testOutputHelper.WriteLine("Expected:");
            foreach (SalespersonResponseDto salespersonResponseFromAdd in salespersonResponseListExpected)
            {
                _testOutputHelper.WriteLine(salespersonResponseFromAdd.ToString());
            }

            _salespersonRepositoryMock.Setup(s => s.GetFilteredSalespersons
            (It.IsAny<Expression<Func<Salesperson, bool>>>())).ReturnsAsync(salespersons);

            //Act
            List<SalespersonResponseDto> salespersonListFromSearch = await
                _salespersonService.GetFilteredSalespersonsAsync(nameof(Salesperson.SalespersonFirstName), "");

            _testOutputHelper.WriteLine("Actual:");
            foreach (SalespersonResponseDto salespersonResponseFromGet in salespersonListFromSearch)
            {
                _testOutputHelper.WriteLine(salespersonResponseFromGet.ToString());
            }

            //Assert
            salespersonListFromSearch.Should().BeEquivalentTo(salespersonResponseListExpected);
        }

        [Fact]
        public async Task GetFilteredSalespersonsAsync_SearchBySalespersonName_ToBeSuccessful()
        {
            //Arrange
            List<Salesperson> salespersons = new List<Salesperson>()
            {
                _fixture.Build<Salesperson>().With(s => s.SalespersonFirstName, "Sandra").Without(s => s.Sales).Create(),
                _fixture.Build<Salesperson>().Without(s => s.Sales).Create(),
                _fixture.Build<Salesperson>().Without(s => s.Sales).Create(),
                _fixture.Build<Salesperson>().Without(s => s.Sales).Create()
            };

            List<SalespersonResponseDto> salespersonResponseListExpected =
                salespersons.Select(s => s.ToSalespersonResponse()).ToList();

            _testOutputHelper.WriteLine("Expected:");
            foreach(SalespersonResponseDto salespersonResponseFromAdd in salespersonResponseListExpected)
            {
                _testOutputHelper.WriteLine(salespersonResponseFromAdd.ToString());
            }

            _salespersonRepositoryMock.Setup(s => s.GetFilteredSalespersons
            (It.IsAny<Expression<Func<Salesperson, bool>>>())).ReturnsAsync(salespersons);

            //Act
            List<SalespersonResponseDto> salespersonsListFromSearch = await 
                _salespersonService.GetFilteredSalespersonsAsync(nameof(Salesperson.SalespersonFirstName), "sa");

            _testOutputHelper.WriteLine("Actual:");
            foreach (SalespersonResponseDto salespersonResponseFromGet in salespersonsListFromSearch)
            {
                _testOutputHelper.WriteLine(salespersonResponseFromGet.ToString());
            }

            //Assert
            salespersonsListFromSearch.Should().BeEquivalentTo(salespersonResponseListExpected);
        }
        #endregion

        #region UpdateSalespersonAsync
        [Fact]
        public async Task UpdateSalespersonAsync_NullSalesperson_ToBeArgumentNullException()
        {
            //Arange
            SalespersonUpdateRequestDto? salespersonUpdateRequest = null;

            //Act
            Func<Task> action = async () =>
            {
                await _salespersonService.UpdateSalespersonAsync(salespersonUpdateRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateSalespersonAsync_InvalidPersonID_ToBeArgumentException()
        {
            //Arrange
            SalespersonUpdateRequestDto? salespersonUpdateRequest = _fixture.Build<SalespersonUpdateRequestDto>().Create();

            //Act
            Func<Task> action = async () =>
            {
                await _salespersonService.UpdateSalespersonAsync(salespersonUpdateRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task UpdateSalespersonAsync_AllSalespersonDetails_ToBeSuccessful()
        {
            //Arrange
            Salesperson salesperson = _fixture.Build<Salesperson>().Without(s => s.Sales).Create();
            SalespersonResponseDto salespersonResponseExpected= salesperson.ToSalespersonResponse();
            SalespersonUpdateRequestDto salespersonUpdateRequest = salespersonResponseExpected.ToSalespersonUpdateRequest();

            _salespersonRepositoryMock.Setup(s => s.UpdateSalespersonAsync(It.IsAny<Salesperson>())).ReturnsAsync(salesperson);
            _salespersonRepositoryMock.Setup(s => s.GetSalespersonByIDAsync(It.IsAny<Guid>())).ReturnsAsync(salesperson);

            //Act
            SalespersonResponseDto salespersonResponseFromUpdate = await _salespersonService.UpdateSalespersonAsync(salespersonUpdateRequest);

            //Assert
            salespersonResponseFromUpdate.Should().Be(salespersonResponseExpected);
        }
        #endregion

        #region DeleteSalespersonAsync
        [Fact]
        public async Task DeleteSalespersonAsync_ValidPersonID_ToBeSuccessful()
        {
            //Arrange
            Salesperson salesperson = _fixture.Build<Salesperson>().Without(s => s.Sales).Create();

            _salespersonRepositoryMock.Setup(s => s.DeleteSalespersonByIDAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _salespersonRepositoryMock.Setup(s => s.GetSalespersonByIDAsync(It.IsAny<Guid>())).ReturnsAsync(salesperson);

            //Act
            bool isDeleted = await _salespersonService.DeleteSalespersonAsync(salesperson.SalespersonID);

            //Assert
            isDeleted.Should().BeTrue();
        }
        [Fact]
        public async Task DeleteSalespersonAsync_InvalidSalespersonID()
        {
            //Act
            bool isDeleted = await _salespersonService.DeleteSalespersonAsync(Guid.NewGuid());

            //Assert
            isDeleted.Should().BeFalse();
        }
        #endregion
    }
}