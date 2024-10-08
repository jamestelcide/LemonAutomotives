﻿using LemonAutomotives.Core.Domain.Entities;
using System.Xml.Linq;

namespace LemonAutomotives.Core.DTO
{
    /// <summary>
    ///Dto class that is used as a return type for most CustomerService methods
    /// </summary>
    public class CustomerResponseDto
    {
        public string CustomerID { get; set; } = string.Empty;
        public string? CustomerFirstName { get; set; }
        public string? CustomerLastName { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerPhone { get; set; }
        public DateTime? CustomerStartDate { get; set; }

        //Compares current object to another object of CustomerResponse type and returns true, if both values are the same; otherwise it will return false
        public override bool Equals(object? obj)
        {
            if (obj == null) { return false; }

            if (obj.GetType() != typeof(CustomerResponseDto)) { return false; }

            CustomerResponseDto CustomerToCompare = (CustomerResponseDto)obj;

            return CustomerID == CustomerToCompare.CustomerID;
        }

        //Returns a unique key for the current object
        public override int GetHashCode()
        {
            return HashCode.Combine(CustomerID, CustomerAddress);
        }
    }

    public static class CustomerExtension
    {
        public static CustomerResponseDto ToCustomerResponse(this Customer customer)
        {
            return new CustomerResponseDto()
            {
                CustomerID = customer.CustomerID,
                CustomerFirstName = customer.CustomerFirstName,
                CustomerLastName = customer.CustomerLastName,
                CustomerAddress = customer.CustomerAddress,
                CustomerPhone = customer.CustomerPhone,
                CustomerStartDate = customer.CustomerStartDate
            };
        }
    }
}
