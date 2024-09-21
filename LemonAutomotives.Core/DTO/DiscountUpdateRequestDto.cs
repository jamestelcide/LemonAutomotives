using LemonAutomotives.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace LemonAutomotives.Core.DTO
{
    /// <summary>
    /// Represents the DTO class that contains the Discount details to update
    /// </summary>
    public class DiscountUpdateRequestDto
    {
        public Guid DiscountID { get; set; }

        [Required(ErrorMessage = "Begin date cannot be blank")]
        public DateTime BeginDate { get; set; }

        [Required(ErrorMessage = "End date cannot be blank")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Discount percentage cannot be blank")]
        [Range(0, 100, ErrorMessage = "Discount percentage must be between 0 and 100")]
        public double DiscountPercentage { get; set; }

        [Required(ErrorMessage = "Product ID cannot be blank")]
        public Guid ProductID { get; set; }

        /// <summary>
        /// Converts the current object of DiscountUpdateRequestDto into a new object of the Discount type
        /// </summary>
        /// <returns>Returns Discount object</returns>
        public Discount ToDiscount()
        {
            return new Discount()
            {
                DiscountID = DiscountID,
                BeginDate = BeginDate,
                EndDate = EndDate,
                DiscountPercentage = DiscountPercentage,
                ProductID = ProductID
            };
        }
    }
}
