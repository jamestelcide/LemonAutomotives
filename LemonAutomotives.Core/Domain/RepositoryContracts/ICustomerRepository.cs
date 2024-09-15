using LemonAutomotives.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        Task<List<Customer>> GetAllCustomersAsync();
        /// <summary>
        /// Returns all Customer objects based on the given expression
        /// </summary>
        /// <param name="predicate">LINQ expression to check</param>
        /// <returns>All matching Salespersons with a given condition</returns>
        Task<List<Customer>> GetFilteredCustomers(Expression<Func<Customer, bool>> predicate);
    }
}
