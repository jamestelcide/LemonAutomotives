using LemonAutomotives.Core.Domain.Entities;
using System.Linq.Expressions;

namespace LemonAutomotives.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents the data access logic for managing Products entity
    /// </summary>
    public interface IProductsRepository
    {
        /// <summary>
        /// Adds a new Product object to the data store
        /// </summary>
        /// <param name="product">Product object to add</param>
        /// <returns>Returns the Product object after adding it to the data store</returns>
        Task<Products> AddProductAsync(Products product);

        /// <summary>
        /// Returns all products in the data store
        /// </summary>
        /// <returns>List of product objects from table</returns>
        Task<List<Products>> GetAllProductsAsync();

        /// <summary>
        /// Returns a Product object based on the given ProductID if no ProductID is present it will return null
        /// </summary>
        /// <param name="productID">ProductID to search</param>
        /// <returns>Matching Product or null</returns>
        Task<Products?> GetProductsByIDAsync(Guid? productID);

        /// <summary>
        /// Returns a Product object based on the given ProductName if no ProductName is present it will return null
        /// </summary>
        /// <param name="productName">ProductName to search</param>
        /// <returns>Matching Product or null</returns>
        Task<Products?> GetProductByNameAsync(string productName);

        /// <summary>
        /// Returns all Product objects based on the given expression
        /// </summary>
        /// <param name="predicate">LINQ expression to check</param>
        /// <returns>All matching Products with a given condition</returns>
        Task<List<Products>> GetFilteredProducts(Expression<Func<Products, bool>> predicate);

        /// <summary>
        /// Updates a product object based on the given ProductID
        /// </summary>
        /// <param name="product">Product object to update</param>
        /// <returns>Returns the updated product object</returns>
        Task<Products> UpdateProductsAsync(Products product);

        /// <summary>
        /// Deletes a Product object based on the ProductID
        /// </summary>
        /// <param name="productID">Product id (Guid) to search</param>
        /// <returns>Returns true if delete is successful; otherwise returns false</returns>
        Task<bool> DeleteProductByIDAsync(Guid productID);
    }
}
