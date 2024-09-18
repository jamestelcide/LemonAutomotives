using LemonAutomotives.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonAutomotives.Core.ServiceContracts
{
    /// <summary>
    /// Represents buisness logic for managing Product entity
    /// </summary>
    public interface IProductsService
    {
        /// <summary>
        /// Returns all Products from the list
        /// </summary>
        /// <returns>All Products from the list as a list of SalespersonResponse</returns>
        Task<List<ProductResponseDto>> GetAllProductsAsync();

        /// <summary>
        /// Returns a Product object based on the given ProductID
        /// </summary>
        /// <param name="productID">guid ProductID to search</param>
        /// <returns></returns>
        Task<ProductResponseDto?> GetProductByIDAsync(Guid? productID);

        /// <summary>
        /// Returns all Product objects that matches with the given search field and search string
        /// </summary>
        /// <param name="searchBy">Search field to search</param>
        /// <param name="searchString">Search string to search</param>
        /// <returns>Returns all matching products based on the given search field and search string</returns>
        Task<List<ProductResponseDto>> GetFilteredProductsAsync(string searchBy, string? searchString);

        /// <summary>
        /// Updates the specified Product details based on the given ProductID
        /// </summary>
        /// <param name="productUpdateRequest">Product details to update, including ProductID</param>
        /// <returns>Returns the ProductResponse object after updating</returns>
        Task<ProductResponseDto> UpdateProductAsync(ProductUpdateRequestDto? productUpdateRequest);
    }
}
