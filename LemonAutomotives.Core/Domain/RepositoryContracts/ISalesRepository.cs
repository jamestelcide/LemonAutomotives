using LemonAutomotives.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonAutomotives.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing Sales entity
    /// </summary>
    public interface ISalesRepository
    {
        /// <summary>
        /// Returns all Sales in the data store
        /// </summary>
        /// <returns>List of Sales objects from table</returns>
        Task<Sales> GetAllSales();

        /// <summary>
        /// Returns all Sales within a date range in the data store
        /// </summary>
        /// <param name="startDate">First day of range</param>
        /// <param name="endDate">Last day of range</param>
        /// <returns>List of Sales objects from table within specified date range</returns>
        Task<Sales> GetSalesByDateRange(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Adds a new Sales object in the data store
        /// </summary>
        /// <param name="sale">Sales object to add</param>
        /// <returns>Returns the Sales object after adding it to the data store</returns>
        Task<Sales> CreateSale(Sales sale);
    }
}
