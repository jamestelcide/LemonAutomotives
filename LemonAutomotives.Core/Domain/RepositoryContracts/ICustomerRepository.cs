using LemonAutomotives.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonAutomotives.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Customer entity
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Returns all customers in the data store
        /// </summary>
        /// <returns>List of customer objects from table</returns>
        Task<Customer> GetAllCustomers();
    }
}
