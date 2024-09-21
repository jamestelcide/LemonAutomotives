using LemonAutomotives.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace LemonAutomotives.Core.DTO
    {
        /// <summary>
        /// DTO class that is used as return type for most DiscountService methods
        /// </summary>
        public class DiscountResponseDto
        {
            public Guid DiscountID { get; set; }
            public DateTime BeginDate { get; set; }
            public DateTime EndDate { get; set; }
            public double DiscountPercentage { get; set; }
            public Guid ProductID { get; set; }

            // Compares current object to another object of DiscountResponseDto type and returns true if both values are the same; otherwise it will return false
            public override bool Equals(object? obj)
            {
                if (obj == null) { return false; }

                if (obj.GetType() != typeof(DiscountResponseDto)) { return false; }

                DiscountResponseDto discountToCompare = (DiscountResponseDto)obj;

                return DiscountID == discountToCompare.DiscountID;
            }

            // Returns a unique key for the current object
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            // Converts this DTO into a request DTO if needed
            public DiscountUpdateRequestDto ToDiscountUpdateRequest()
            {
                return new DiscountUpdateRequestDto()
                {
                    DiscountID = DiscountID,
                    BeginDate = BeginDate,
                    EndDate = EndDate,
                    DiscountPercentage = DiscountPercentage,
                    ProductID = ProductID
                };
            }
        }

        public static class DiscountExtension
        {
            // Converts from Discount object to DiscountResponseDto object
            public static DiscountResponseDto ToDiscountResponse(this Discount discount)
            {
                return new DiscountResponseDto()
                {
                    DiscountID = discount.DiscountID,
                    BeginDate = discount.BeginDate,
                    EndDate = discount.EndDate,
                    DiscountPercentage = discount.DiscountPercentage,
                    ProductID = discount.ProductID
                };
            }
        }
    }