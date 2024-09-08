using LemonAutomotives.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
        Task<Products> GetAllProducts();

        /// <summary>
        /// Updates a product object based on the given ProductID
        /// </summary>
        /// <param name="product">Product object to update</param>
        /// <returns>Returns the updated product object</returns>
        Task<Products> UpdateProducts(Products product);
    }
}
