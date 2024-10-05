using LemonAutomotives.Core.Domain.Entities;
using System.Linq.Expressions;


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
        Task<List<Sales>> GetAllSalesAsync();

        /// <summary>
        /// Returns all Sales within a date range in the data store
        /// </summary>
        /// <param name="startDate">First day of range</param>
        /// <param name="endDate">Last day of range</param>
        /// <returns>List of Sales objects from table within specified date range</returns>
        Task<List<Sales>> GetSalesByDateRange(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Adds a new Sales object in the data store
        /// </summary>
        /// <param name="sale">Sales object to add</param>
        /// <returns>Returns the Sales object after adding it to the data store</returns>
        Task<Sales> CreateSale(Sales sale);

        /// <summary>
        /// Returns a Sales object based on the given SaleID if no SaleID is present it will return null
        /// </summary>
        /// <param name="saleID">SaleID to search</param>
        /// <returns>Matching Sales or null</returns>
        Task<Sales?> GetSalesByIDAsync(Guid saleID);

        /// <summary>
        /// Returns all Sales objects based on the given expression
        /// </summary>
        /// <param name="predicate">LINQ expression to check</param>
        /// <returns>All matching Sales with a given condition</returns>
        Task<List<Sales>> GetFilteredSalesAsync(Expression<Func<Sales, bool>> predicate);
    }
}
