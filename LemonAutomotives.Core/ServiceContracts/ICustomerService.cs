using LemonAutomotives.Core.Domain.Entities;
using LemonAutomotives.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonAutomotives.Core.ServiceContracts
{
    /// <summary>
    /// Represents buisness logic for managing Customer entity
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Returns all Customers from the list
        /// </summary>
        /// <returns>All Customers from the list as list of CustomerResponse</returns>
        Task<List<CustomerResponseDto>> GetAllCustomersAsync();
        /// <summary>
        /// Returns all Customer objects that matches with the given search field and search string
        /// </summary>
        /// <param name="searchBy">Search field to search</param>
        /// <param name="searchString">Search string to search</param>
        /// <returns>Returns all matching Customers based on the given search field and search string</returns>
        Task<List<CustomerResponseDto>> GetFilteredCustomersAsync(string searchBy, string? searchString);
    }
}
