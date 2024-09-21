using LemonAutomotives.Core.Domain.Entities;
using LemonAutomotives.Core.Domain.RepositoryContracts;
using LemonAutomotives.Core.DTO;
using LemonAutomotives.Core.ServiceContracts;
using System.Linq.Expressions;

namespace LemonAutomotives.Core.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<DiscountResponseDto> AddDiscountAsync(DiscountAddRequestDto? discountAddRequest)
        {
            // Validation: discountAddRequest parameter can't be null
            if (discountAddRequest == null)
            {
                throw new ArgumentNullException(nameof(discountAddRequest));
            }

            // Convert the DiscountAddRequestDto to a Discount object
            Discount discount = discountAddRequest.ToDiscount();

            // Generate a new DiscountID
            discount.DiscountID = Guid.NewGuid();

            await _discountRepository.AddDiscountAsync(discount);

            return discount.ToDiscountResponse();
        }

        public async Task<List<DiscountResponseDto>> GetAllDiscountsAsync()
        {
            List<Discount> discounts = await _discountRepository.GetAllDiscountsAsync();
            return discounts.Select(discount => discount.ToDiscountResponse()).ToList();
        }

        public async Task<DiscountResponseDto?> GetDiscountByIDAsync(Guid? discountID)
        {
            if (discountID == null) { return null; }
            Discount? discount = await _discountRepository.GetDiscountByIDAsync(discountID.Value);

            if (discount == null) { return null; }
            return discount.ToDiscountResponse();
        }

        public async Task<List<DiscountResponseDto>> GetFilteredDiscountsAsync(string searchBy, string? searchString)
        {
            List<Discount> discounts;

            discounts = searchBy switch
            {
                nameof(DiscountResponseDto.BeginDate) =>
                await _discountRepository.GetFilteredDiscounts(d =>
                    d.BeginDate.ToString("dd MM yyyy").Contains(searchString ?? string.Empty)),

                nameof(DiscountResponseDto.EndDate) =>
                await _discountRepository.GetFilteredDiscounts(d =>
                    d.EndDate.ToString("dd MM yyyy").Contains(searchString ?? string.Empty)),

                nameof(DiscountResponseDto.DiscountPercentage) =>
                await _discountRepository.GetFilteredDiscounts(d =>
                    d.DiscountPercentage.ToString().Contains(searchString ?? string.Empty)),

                nameof(DiscountResponseDto.ProductID) =>
                await _discountRepository.GetFilteredDiscounts(d =>
                    d.ProductID.ToString().Contains(searchString ?? string.Empty)),

                _ => await _discountRepository.GetAllDiscountsAsync()
            };

            return discounts.Select(d => d.ToDiscountResponse()).ToList();
        }

        public async Task<DiscountResponseDto> UpdateDiscountAsync(DiscountUpdateRequestDto? discountUpdateRequest)
        {
            if (discountUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(discountUpdateRequest));
            }

            Discount? matchingDiscount = await _discountRepository.GetDiscountByIDAsync(discountUpdateRequest.DiscountID);
            if (matchingDiscount == null)
            {
                throw new ArgumentNullException(nameof(discountUpdateRequest.DiscountID), "DiscountID not found.");
            }

            matchingDiscount.BeginDate = discountUpdateRequest.BeginDate;
            matchingDiscount.EndDate = discountUpdateRequest.EndDate;
            matchingDiscount.DiscountPercentage = discountUpdateRequest.DiscountPercentage;
            matchingDiscount.ProductID = discountUpdateRequest.ProductID;

            await _discountRepository.UpdateDiscountAsync(matchingDiscount);

            return matchingDiscount.ToDiscountResponse();
        }

        public async Task<bool> DeleteDiscountAsync(Guid? discountID)
        {
            if (discountID == null)
            {
                throw new ArgumentNullException(nameof(discountID));
            }

            Discount? discount = await _discountRepository.GetDiscountByIDAsync(discountID.Value);

            if (discount == null) { return false; }

            await _discountRepository.DeleteDiscountByIDAsync(discountID.Value);

            return true;
        }
    }
}
