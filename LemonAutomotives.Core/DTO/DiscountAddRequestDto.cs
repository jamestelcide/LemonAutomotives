using LemonAutomotives.Core.Domain.Entities;

namespace LemonAutomotives.Core.DTO
{
    /// <summary>
    /// DTO class for adding a new Discount
    /// </summary>
    public class DiscountAddRequestDto
    {
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public double DiscountPercentage { get; set; }
        public Guid ProductID { get; set; }

        /// <summary>
        /// Converts the current object of DiscountAddRequestDto into a new object of the Discount type
        /// </summary>
        /// <returns>Returns Discount object</returns>
        public Discount ToDiscount()
        {
            return new Discount()
            {
                BeginDate = BeginDate,
                EndDate = EndDate,
                DiscountPercentage = DiscountPercentage,
                ProductID = ProductID
            };
        }
    }
}
