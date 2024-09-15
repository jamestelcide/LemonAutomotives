using LemonAutomotives.Core.Domain.Entities;
using LemonAutomotives.Core.Domain.RepositoryContracts;
using LemonAutomotives.Core.DTO;
using LemonAutomotives.Core.ServiceContracts;

namespace LemonAutomotives.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<CustomerResponseDto>> GetAllCustomersAsync()
        {
            List<Customer> customers = await _customerRepository.GetAllCustomersAsync();
            return customers.Select(c => c.ToCustomerResponse()).ToList();
        }

        public async Task<List<CustomerResponseDto>> GetFilteredCustomersAsync(string searchBy, string? searchString)
        {
            List<Customer> customers;

            customers = searchBy switch
            {
                nameof(CustomerResponseDto.CustomerFirstName) =>
                await _customerRepository.GetFilteredCustomers(c => 
                c.CustomerFirstName != null &&
                c.CustomerFirstName.Contains(searchString ?? string.Empty)),

                nameof(CustomerResponseDto.CustomerLastName) =>
                await _customerRepository.GetFilteredCustomers(c =>
                c.CustomerLastName != null &&
                c.CustomerLastName.Contains(searchString ?? string.Empty)),

                nameof(CustomerResponseDto.CustomerAddress) =>
                await _customerRepository.GetFilteredCustomers(c =>
                c.CustomerAddress != null &&
                c.CustomerAddress.Contains(searchString ?? string.Empty)),

                nameof(CustomerResponseDto.CustomerPhone) =>
                await _customerRepository.GetFilteredCustomers(c =>
                c.CustomerPhone != null &&
                c.CustomerPhone.Contains(searchString ?? string.Empty)),

                nameof(CustomerResponseDto.CustomerStartDate) =>
                await _customerRepository.GetFilteredCustomers(c =>
                c.CustomerStartDate.HasValue &&
                c.CustomerStartDate.Value.ToString("dd MM yyyy").Contains(searchString ?? string.Empty)),

                _ => await _customerRepository.GetAllCustomersAsync()
            };

            return customers.Select(c => c.ToCustomerResponse()).ToList();
        }
    }
}
