using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace BackendWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class BaseAnalysisController : ControllerBase
    {

        [HttpPost("[action]")]
        public async Task<ActionResult<List<AvgPassRateResponse>>> GetAvgPassRate([FromBody] DateTime from, [FromBody] DateTime to)
        {
            //TODO query analisis analysis
            //LIB.GETAVG(from, to)

            var result = new List<AvgPassRateResponse> {
                new AvgPassRateResponse
                {
                    AvgPassRate = 0.8,
                    Code = "N/A",
                    Family = "IL4",
                    HwVersion = "N/A",
                    Name = "IL4 PG24A",
                    SFCode = "PG24A",
                    SFIdString = "PG24ANV21510A00",
                    SFSN = "21510A00",
                    SN = "N/A"
                },
                new AvgPassRateResponse
                {
                    AvgPassRate = 0.2,
                    Code = "N/A",
                    Family = "IL5",
                    HwVersion = "N/A",
                    Name = "IL5 PG24B",
                    SFCode = "PG24B",
                    SFIdString = "PG24BNV21510A00",
                    SFSN = "21510B00",
                    SN = "N/A"
                },
            };

            return Ok(result);
        }
    }
}
