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
    public class CustomerServiceTest
    {
        private readonly ICustomerService _customerService;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        public CustomerServiceTest(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _customerRepository = _customerRepositoryMock.Object;
            _customerService = new CustomerService(_customerRepository);
            _testOutputHelper = testOutputHelper;
        }

        #region GetAllCustomersAsync
        [Fact]
        public async Task GetAllCustomersAsync_ToBeEmptyList()
        {
            var customers = new List<Customer>();
            _customerRepositoryMock.Setup(s => s.GetAllCustomersAsync())
                .ReturnsAsync(customers);

            List<CustomerResponseDto> customerFromGet = await _customerService.GetAllCustomersAsync();

            customerFromGet.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllCustomersAsync_WithFewCustomers_ToBeSuccessful()
        {
            List<Customer> customers = new List<Customer>()
            {
                _fixture.Build<Customer>().Without(s => s.Sales).Create(),
                _fixture.Build<Customer>().Without(s => s.Sales).Create(),
                _fixture.Build<Customer>().Without(s => s.Sales).Create(),
                _fixture.Build<Customer>().Without(s => s.Sales).Create()
            };

            List<CustomerResponseDto> customerResponseListExpected =
                customers.Select(c => c.ToCustomerResponse()).ToList();

            _testOutputHelper.WriteLine("Expected:");
            foreach (CustomerResponseDto customerResponseFromAdd in customerResponseListExpected)
            {
                _testOutputHelper.WriteLine(customerResponseFromAdd.ToString());
            }

            _customerRepositoryMock.Setup(s => s.GetAllCustomersAsync()).ReturnsAsync(customers);

            List<CustomerResponseDto> customersListFromGet = await _customerService.GetAllCustomersAsync();

            _testOutputHelper.WriteLine("Actual:");
            foreach (CustomerResponseDto customerResponseFromGet in customersListFromGet)
            {
                _testOutputHelper.WriteLine(customerResponseFromGet.ToString());
            }

            customersListFromGet.Should().BeEquivalentTo(customerResponseListExpected);
        }
        #endregion

        #region GetFilteredCustomersAsync
        [Fact]
        public async Task GetFilteredCustomersAsync_EmptySearchText_ToBeSuccessful()
        {
            //Arrange
            List<Customer> customers = new List<Customer>()
            {
                _fixture.Build<Customer>().Without(s => s.Sales).Create(),
                _fixture.Build<Customer>().Without(s => s.Sales).Create(),
                _fixture.Build<Customer>().Without(s => s.Sales).Create(),
                _fixture.Build<Customer>().Without(s => s.Sales).Create()
            };

            List<CustomerResponseDto> customerResponseListExpected =
                customers.Select(c => c.ToCustomerResponse()).ToList();

            _testOutputHelper.WriteLine("Expected:");
            foreach (CustomerResponseDto customerResponseFromAdd in customerResponseListExpected)
            {
                _testOutputHelper.WriteLine(customerResponseFromAdd.ToString());
            }

            _customerRepositoryMock.Setup(c => c.GetFilteredCustomers
            (It.IsAny<Expression<Func<Customer, bool>>>())).ReturnsAsync(customers);

            //Act
            List<CustomerResponseDto> customerListFromSearch = await
                _customerService.GetFilteredCustomersAsync(nameof(Customer.CustomerFirstName), "");

            _testOutputHelper.WriteLine("Actual:");
            foreach (CustomerResponseDto customerResponseFromGet in customerListFromSearch)
            {
                _testOutputHelper.WriteLine(customerResponseFromGet.ToString());
            }

            //Assert
            customerListFromSearch.Should().BeEquivalentTo(customerResponseListExpected);
        }

        [Fact]
        public async Task GetFilteredCustomersAsync_SearchByCustomerName_ToBeSuccessful()
        {
            //Arrange
            List<Customer> customers = new List<Customer>()
            {
                _fixture.Build<Customer>().With(s => s.CustomerFirstName, "Mark").Without(s => s.Sales).Create(),
                _fixture.Build<Customer>().Without(s => s.Sales).Create(),
                _fixture.Build<Customer>().Without(s => s.Sales).Create(),
                _fixture.Build<Customer>().Without(s => s.Sales).Create()
            };

            List<CustomerResponseDto> customerResponseListExpected =
                customers.Select(c => c.ToCustomerResponse()).ToList();

            _testOutputHelper.WriteLine("Expected:");
            foreach (CustomerResponseDto customerResponseFromAdd in customerResponseListExpected)
            {
                _testOutputHelper.WriteLine(customerResponseFromAdd.ToString());
            }

            _customerRepositoryMock.Setup(s => s.GetFilteredCustomers
            (It.IsAny<Expression<Func<Customer, bool>>>())).ReturnsAsync(customers);

            //Act
            List<CustomerResponseDto> customerListFromSearch = await
                _customerService.GetFilteredCustomersAsync(nameof(Customer.CustomerFirstName), "ma");

            _testOutputHelper.WriteLine("Actual:");
            foreach (CustomerResponseDto customerResponseFromGet in customerListFromSearch)
            {
                _testOutputHelper.WriteLine(customerResponseFromGet.ToString());
            }

            //Assert
            customerListFromSearch.Should().BeEquivalentTo(customerResponseListExpected);
            #endregion
        }
    }
}
