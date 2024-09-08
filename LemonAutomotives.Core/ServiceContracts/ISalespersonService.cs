using LemonAutomotives.Core.DTO;

namespace LemonAutomotives.Core.ServiceContracts
{
    /// <summary>
    /// Represents buisness logic for managing Salesperson entity
    /// </summary>
    public interface ISalespersonService
    {
        /// <summary>
        /// Adds a Salesperson object to the list of countries
        /// </summary>
        /// <param name="salespersonAddRequest">Salesperson to add</param>
        /// <returns>Returns the salesperson object after adding it with a newly generated salespersonID</returns>
        Task<SalespersonResponseDto> AddSalespersonAsync(SalespersonAddRequestDto? salespersonAddRequest);
        
        /// <summary>
        /// Returns all salespersons from the list
        /// </summary>
        /// <returns>All Salespersons from the list as a list of SalespersonResponse</returns>
        Task<List<SalespersonResponseDto>> GetAllSalespersonsAsync();
        
        /// <summary>
        /// Returns a Salesperson object based on the given SalespersonID
        /// </summary>
        /// <param name="salespersonID">guid SalespersonID to search</param>
        /// <returns></returns>
        Task<SalespersonResponseDto?> GetSalespersonByIDAsync(Guid? salespersonID);

        /// <summary>
        /// Returns all Salesperson objects that matches with the given search field and search string
        /// </summary>
        /// <param name="searchBy">Search field to search</param>
        /// <param name="searchString">Search string to search</param>
        /// <returns>Returns all matching salespersons based on the given search field and search string</returns>
        Task<List<SalespersonResponseDto>> GetFilteredSalespersons(string searchBy, string? searchString);
        
        /// <summary>
        /// Updates the specified Salesperson details based on the given SalespersonID
        /// </summary>
        /// <param name="salespersonUpdateRequest">Salesperson details to update, including SalespersonID</param>
        /// <returns>Returns the SalespersonResponse object after updating</returns>
        Task<SalespersonResponseDto> UpdateSalespersonAsync(SalespersonUpdateRequestDto? salespersonUpdateRequest);
        
        /// <summary>
        /// Deletes a Salesperson based on the given SalespersonID
        /// </summary>
        /// <param name="salespersonID"></param>
        /// <returns>Returns true if delete was successful; otherwise returns false</returns>
        Task<bool> DeleteSalespersonAsync(Guid? salespersonID);
    }
}
