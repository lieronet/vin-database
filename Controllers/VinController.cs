using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using vin_db.Models;
using vin_db.Services;

namespace vin_db.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VinController : ControllerBase
    {
        private readonly IVinService _vinService;
        private readonly Configuration _appConfig;

        public VinController(IVinService vinService, IOptions<Configuration> appConfig)
        {
            _vinService = vinService;
            _appConfig = appConfig.Value;
        }
        [HttpPost]
        public async Task<IActionResult> UploadVins([FromBody] string csvVinList)
        {
            if (System.IO.File.Exists(_appConfig.TestFile))
            {
                csvVinList = System.IO.File.ReadAllText(_appConfig.TestFile);
            }

            var parseResponse = await _vinService.Parse(csvVinList);

            parseResponse.VinRecords.RemoveAll(x => parseResponse.Errors.Where(y=>!string.IsNullOrEmpty(y.Vin)).Select(y=>y.Vin).Contains(x.Vin));

            await _vinService.InsertVinList(parseResponse.VinRecords);

            await _vinService.SaveCsv(csvVinList);

            return Ok(parseResponse.Errors);
        }

        [HttpGet]
        [Route("/{vin}")]
        public async Task<IActionResult> GetVinData(string vin)
        {
            if ( !await _vinService.ValidateVin(vin))
            {
                return BadRequest("Invalid VIN");
            }

            var response = _vinService.GetVinRecord(vin);

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
            if(dealerId!= null && dealerId > 99999) return BadRequest("DealerId should be a maximum of five digits");
            if(modifiedAfter != null && modifiedAfter > DateTime.Now) return BadRequest("Modified date should be less than current date");

            return Ok(await _vinService.SearchVinRecords(pageIndex,pageSize, modifiedAfter, dealerId));
        }
    }
}
