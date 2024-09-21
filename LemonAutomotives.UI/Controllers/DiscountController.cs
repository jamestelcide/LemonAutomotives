using LemonAutomotives.Core.DTO;
using LemonAutomotives.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace LemonAutomotives.UI.Controllers
{
    [Route("[controller]")]
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [Route("[action]")]
        public async Task<IActionResult> Index(string searchBy, string? searchString)
        {
            List<DiscountResponseDto> discountList = await
                _discountService.GetFilteredDiscountsAsync(searchBy, searchString);

            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                { nameof(DiscountResponseDto.BeginDate), "Begin Date" },
                { nameof(DiscountResponseDto.EndDate), "End Date" },
                { nameof(DiscountResponseDto.DiscountPercentage), "Discount Percentage" },
                { nameof(DiscountResponseDto.ProductID), "Product ID" }
            };

            return View(discountList);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Create(DiscountAddRequestDto discountRequest)
        {
            await _discountService.AddDiscountAsync(discountRequest);
            return RedirectToAction("Index", "Discount");
        }

        [HttpGet]
        [Route("[action]/{discountID}")]
        public async Task<IActionResult> Edit(Guid discountID)
        {
            DiscountResponseDto? discountResponse = await _discountService.GetDiscountByIDAsync(discountID);
            if (discountResponse == null) { return RedirectToAction("Index"); }
            DiscountUpdateRequestDto discountUpdateRequest = discountResponse.ToDiscountUpdateRequest();

            return View(discountUpdateRequest);
        }

        [HttpPost]
        [Route("[action]/{discountID}")]
        public async Task<IActionResult> Edit(DiscountUpdateRequestDto discountUpdateRequest)
        {
            DiscountResponseDto? discountResponse = await
                _discountService.GetDiscountByIDAsync(discountUpdateRequest.DiscountID);

            if (discountResponse == null) { return RedirectToAction("Index"); }

            await _discountService.UpdateDiscountAsync(discountUpdateRequest);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("[action]/{discountID}")]
        public async Task<IActionResult> Delete(Guid? discountID)
        {
            if (!discountID.HasValue)
            {
                return RedirectToAction("Index");
            }

            DiscountResponseDto? discountResponse = await _discountService.GetDiscountByIDAsync(discountID.Value);
            if (discountResponse == null) { return RedirectToAction("Index"); }

            return View(discountResponse);
        }

        [HttpPost]
        [Route("[action]/{discountID}")]
        public async Task<IActionResult> Delete(DiscountUpdateRequestDto discountUpdateRequest)
        {
            DiscountResponseDto? discountResponse = await
                _discountService.GetDiscountByIDAsync(discountUpdateRequest.DiscountID);

            if (discountResponse == null) { return RedirectToAction("Index"); }

            await _discountService.DeleteDiscountAsync(discountUpdateRequest.DiscountID);
            return RedirectToAction("Index");
        }
    }
}
