using LemonAutomotives.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonAutomotives.Core.DTO
{
    /// <summary>
    /// Represents the DTO class that contains the Sales details to update
    /// </summary>
    public class SalesUpdateRequestDto
    {
        public Guid SaleID { get; set; }

        [Required(ErrorMessage = "Product Name can not be blank")]
        public Guid ProductID { get; set; }

        [Required(ErrorMessage = "SalespersonID can not be blank")]
        public Guid SalespersonID { get; set; }

        [Required(ErrorMessage = "CustomerID can not be blank")]
        public Guid CustomerID { get; set; }

        /// <summary>
        /// Converts the current object of SalesAddRequest into a new object of the Sales type
        /// </summary>
        /// <returns>Returns Sales object</returns>
        public Sales ToSales()
        {
            return new Sales()
            {
                ProductID = ProductID,
                SalespersonID = SalespersonID,
                CustomerID = CustomerID
            };
        }
    }
}
