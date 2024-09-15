using LemonAutomotives.Core.DTO;
using LemonAutomotives.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace LemonAutomotives.UI.Controllers
{
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Route("[action]")]
        public async Task<IActionResult> Index(string searchBy, string? searchString)
        {
            List<CustomerResponseDto> customerList = await _customerService.GetFilteredCustomersAsync(searchBy, searchString);

            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                { nameof(CustomerResponseDto.CustomerFirstName), "First Name" },
                { nameof(CustomerResponseDto.CustomerLastName), "Last Name" },
                { nameof(CustomerResponseDto.CustomerAddress), "Address" },
                { nameof(CustomerResponseDto.CustomerStartDate), "Start Date" }
            };

            return View(customerList);
        }
    }
}
