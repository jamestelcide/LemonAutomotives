using LemonAutomotives.Core.DTO;
using LemonAutomotives.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace LemonAutomotives.UI.Controllers
{
    [Route("[controller]")]
    public class SalespersonController : Controller
    {
        private readonly ISalespersonService _salespersonService;
        public SalespersonController(ISalespersonService salespersonService)
        {
            _salespersonService = salespersonService;
        }

        [Route("[action]")]
        public async Task<IActionResult> Index(string searchBy, string? searchString)
        {
            List<SalespersonResponseDto> salespersonList = await _salespersonService.GetFilteredSalespersonsAsync(searchBy, searchString);

            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                { nameof(SalespersonResponseDto.SalespersonFirstName), "First Name" },
                { nameof(SalespersonResponseDto.SalespersonLastName), "Last Name" },
                { nameof(SalespersonResponseDto.SalespersonAddress), "Address" },
                { nameof(SalespersonResponseDto.SalespersonStartDate), "Start Date" },
                { nameof(SalespersonResponseDto.SalespersonTerminationDate), "TerminationDate" }
            };

            return View(salespersonList);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Create(SalespersonAddRequestDto salespersonRequest)
        {
            await _salespersonService.AddSalespersonAsync(salespersonRequest);
            return RedirectToAction("Index", "Salesperson");
        }

        [HttpGet]
        [Route("[action]/{salespersonID}")]
        public async Task<IActionResult> Edit(Guid salespersonID)
        {
            SalespersonResponseDto? salespersonResponse = await _salespersonService.GetSalespersonByIDAsync(salespersonID);
            if (salespersonResponse == null) { return RedirectToAction("Index"); }
            SalespersonUpdateRequestDto salespersonUpdateRequest = salespersonResponse.ToSalespersonUpdateRequest();

            return View(salespersonUpdateRequest);
        }

        [HttpPost]
        [Route("[action]/{salespersonID}")]
        public async Task<IActionResult> Edit(SalespersonUpdateRequestDto salespersonUpdateRequest)
        {
            SalespersonResponseDto? salespersonResponse = await
                _salespersonService.GetSalespersonByIDAsync(salespersonUpdateRequest.SalespersonID);

            if (salespersonResponse == null) { return RedirectToAction("Index"); }

            await _salespersonService.UpdateSalespersonAsync(salespersonUpdateRequest);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("[action]/{salespersonID}")]
        public async Task<IActionResult> Delete(Guid? salespersonID)
        {
            if (!salespersonID.HasValue)
            {
                return RedirectToAction("Index");
            }

            SalespersonResponseDto? salespersonResponse = await _salespersonService.GetSalespersonByIDAsync(salespersonID.Value);
            if (salespersonResponse == null) { return RedirectToAction("Index"); }

            return View(salespersonResponse);
        }

        [HttpPost]
        [Route("[action]/{salespersonID}")]
        public async Task<IActionResult> Delete(SalespersonUpdateRequestDto salespersonUpdateResult)
        {
            SalespersonResponseDto? salespersonResponse = await 
                _salespersonService.GetSalespersonByIDAsync(salespersonUpdateResult.SalespersonID);

            if (salespersonResponse == null) { return RedirectToAction("Index"); }

            await _salespersonService.DeleteSalespersonAsync(salespersonUpdateResult.SalespersonID);
            return RedirectToAction("Index");
        }
    }
}
