using LemonAutomotives.Core.Domain.Entities;
using System.Linq.Expressions;

namespace LemonAutomotives.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data acess logic for managing Salesperson entity
    /// </summary>
    public interface ISalespersonRepository
    {
        /// <summary>
        /// Adds a new Salesperson object to the data store
        /// </summary>
        /// <param name="salesperson">Salesperson object to add</param>
        /// <returns>Returns the Salesperson object after adding it to the data store</returns>
        Task<Salesperson> AddSalespersonAsync(Salesperson salesperson);

        /// <summary>
        /// Returns all Salespersons in the data store
        /// </summary>
        /// <returns>All Salespersons in the table</returns>
        Task<List<Salesperson>> GetAllSalespersonsAsync();

        /// <summary>
        /// Returns a Salesperson object based on the given SalespersonID if no SalespersonID is present it will return null
        /// </summary>
        /// <param name="SalespersonID">SalespersonID to search</param>
        /// <returns>Matching Salesperson or null</returns>
        Task<Salesperson?> GetSalespersonByIDAsync(string salespersonID);

        /// <summary>
        /// Returns all Salesperson objects based on the given expression
        /// </summary>
        /// <param name="predicate">LINQ expression to check</param>
        /// <returns>All matching Salespersons with a given condition</returns>
        Task<List<Salesperson>> GetFilteredSalespersons(Expression<Func<Salesperson, bool>> predicate);

        /// <summary>
        /// Updates a Salesperson object based on the given id
        /// </summary>
        /// <param name="person">Salesperson object to update</param>
        /// <returns>Returns updated person object</returns>
        Task<Salesperson> UpdateSalespersonAsync(Salesperson salesperson);

        /// <summary>
        /// Deletes a Salesperson object based on the SalespersonID
        /// </summary>
        /// <param name="salespersonID">Salesperson id (string) to search</param>
        /// <returns>Returns true if delete is successful; otherwise returns false</returns>
        Task<bool> DeleteSalespersonByIDAsync(string salespersonID);
    }
}
