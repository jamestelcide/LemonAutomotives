using LemonAutomotives.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonAutomotives.Core.ServiceContracts
{
    /// <summary>
    /// Represents buisness logic for managing Sales entity
    /// </summary>
    public interface ISalesService
    {
        /// <summary>
        /// Adds a Sale object to the list
        /// </summary>
        /// <param name="salesAddRequest">Sales to add</param>
        /// <returns>Returns the sales object after adding it with a newly generated salesID</returns>
        Task<SalesResponseDto> CreateSaleAsync(SalesAddRequestDto? salesAddRequest);
        /// <summary>
        /// Returns all Sales from the list
        /// </summary>
        /// <returns>All Sales from the list as a list of SalesResponse</returns>
        Task<List<SalesResponseDto>> GetAllSalesAsync();
        
        /// <summary>
        /// Returns a Sale object based on the given SaleID
        /// </summary>
        /// <param name="saleID">guid SaleID to search</param>
        /// <returns></returns>
        Task<SalesResponseDto?> GetSaleByIDAsync(Guid? saleID);

        /// <summary>
        /// Returns all Sales objects that matches with the given search field and search string
        /// </summary>
        /// <param name="searchBy">Search field to search</param>
        /// <param name="searchString">Search string to search</param>
        /// <returns>Returns all matching sales based on the given search field and search string</returns>
        Task<List<SalesResponseDto>> GetFilteredSalesAsync(string searchBy, string? searchString);
    }
}
