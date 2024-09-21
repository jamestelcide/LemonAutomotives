using LemonAutomotives.Core.DTO;

namespace LemonAutomotives.Core.ServiceContracts
{
    /// <summary>
    /// Represents business logic for managing Discount entity
    /// </summary>
    public interface IDiscountService
    {
        /// <summary>
        /// Adds a Discount object to the list
        /// </summary>
        /// <param name="discountAddRequest">Discount to add</param>
        /// <returns>Returns the Discount object after adding it with a newly generated DiscountID</returns>
        Task<DiscountResponseDto> AddDiscountAsync(DiscountAddRequestDto? discountAddRequest);

        /// <summary>
        /// Returns all discounts from the list
        /// </summary>
        /// <returns>All Discounts from the list as a list of DiscountResponseDto</returns>
        Task<List<DiscountResponseDto>> GetAllDiscountsAsync();

        /// <summary>
        /// Returns a Discount object based on the given DiscountID
        /// </summary>
        /// <param name="discountID">GUID DiscountID to search</param>
        /// <returns>Returns the DiscountResponseDto object or null if not found</returns>
        Task<DiscountResponseDto?> GetDiscountByIDAsync(Guid? discountID);

        /// <summary>
        /// Returns all Discount objects that match with the given search criteria
        /// </summary>
        /// <param name="searchBy">Search field to search</param>
        /// <param name="searchString">Search string to search</param>
        /// <returns>Returns all matching discounts based on the search criteria</returns>
        Task<List<DiscountResponseDto>> GetFilteredDiscountsAsync(string searchBy, string? searchString);

        /// <summary>
        /// Updates the specified Discount details based on the given DiscountID
        /// </summary>
        /// <param name="discountUpdateRequest">Discount details to update, including DiscountID</param>
        /// <returns>Returns the DiscountResponseDto object after updating</returns>
        Task<DiscountResponseDto> UpdateDiscountAsync(DiscountUpdateRequestDto? discountUpdateRequest);

        /// <summary>
        /// Deletes a Discount based on the given DiscountID
        /// </summary>
        /// <param name="discountID">GUID DiscountID to delete</param>
        /// <returns>Returns true if delete was successful; otherwise returns false</returns>
        Task<bool> DeleteDiscountAsync(Guid? discountID);
    }
}
