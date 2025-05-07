using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using vin_db.Services;

namespace vin_db.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VinController : ControllerBase
    {
        private readonly IVinService _vinService;

        public VinController(IVinService vinService)
        {
            _vinService = vinService;
        }
        [HttpPost]
        public async Task<IActionResult> UploadVins([FromBody] string csvVinList)
        {
            var vinList = await _vinService.Parse(csvVinList);
            //parse csv into object
            //run validation
                //VIN regex
                //valid date range
                //all fields present
                //dealerId check?
            //record validation failures
            //insert validation successes to queue table

            var validationResponse = await _vinService.Validate(vinList);

            vinList.RemoveAll(x => validationResponse.InvalidVins.Contains(x.Vin));

            await _vinService.InsertVinRecords(vinList);

            await _vinService.SaveCsv(csvVinList);

            return Ok(validationResponse.ResponseMessage);
        }

        [HttpGet]
        [Route("/{vin}")]
        public IActionResult GetVinData(string vin)
        {
            if (!_vinService.ValidateVin(vin))
            {
                return BadRequest("Invalid VIN");
            }

            var response = _vinService.GetVinData(vin);

            if(response == null)
            {
                //no content? 
                return NotFound("No record for supplied VIN");
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> SearchVinData(DateTime? modifiedAfter,
            int? dealerId,
            int pageSize = 25, 
            int pageIndex = 0 )
        {
            if (pageSize < 0) return BadRequest("Page size should be a positive integer");
            if (pageIndex < 0) return BadRequest("Page number should be a positive integer");

            return Ok(await _vinService.SearchVins(pageIndex,pageSize, modifiedAfter, dealerId));
        }
    }
}
