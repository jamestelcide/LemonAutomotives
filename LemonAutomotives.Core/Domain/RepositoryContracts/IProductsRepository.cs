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
    /// Represents the data access logic for managing Products entity
    /// </summary>
    public interface IProductsRepository
    {
        /// <summary>
        /// Returns all products in the data store
        /// </summary>
        /// <returns>List of product objects from table</returns>
        Task<List<Products>> GetAllProductsAsync();

        /// <summary>
        /// Updates a product object based on the given ProductID
        /// </summary>
        /// <param name="product">Product object to update</param>
        /// <returns>Returns the updated product object</returns>
        Task<Products> UpdateProductsAsync(Products product);

        /// <summary>
        /// Returns a Product object based on the given ProductID if no ProductID is present it will return null
        /// </summary>
        /// <param name="productID">ProductID to search</param>
        /// <returns>Matching Product or null</returns>
        Task<Products?> GetProductsByIDAsync(Guid? productID);

        /// <summary>
        /// Returns all Product objects based on the given expression
        /// </summary>
        /// <param name="predicate">LINQ expression to check</param>
        /// <returns>All matching Products with a given condition</returns>
        Task<List<Products>> GetFilteredProducts(Expression<Func<Products, bool>> predicate);
    }
}
