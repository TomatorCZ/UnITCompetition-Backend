using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace BackendWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class BaseAnalysisController : ControllerBase
    {
        private CommonDbContext _context;

        public BaseAnalysisController(CommonDbContext context)
        {
            _context = context;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<List<AvgPassRateResponse>>> GetAvgPassRate([FromBody] DateTime from, [FromBody] DateTime to)
        {
            List<IGrouping<string, Head>> prodGroups = await _context.Heads
                .Where(x => x.TimeStamp >= from)
                .Where(x => x.TimeStamp <= to)
                .GroupBy(x => x.Product_SFIdString)
                //.DistinctBy(x => x.Product_SFIdString)
                .ToListAsync();



            var result = new List<AvgPassRateResponse>();
            foreach (var prodGroup in prodGroups)
            {
                int passCounter = 0;
                int totalCounter = 0;
                foreach (var product in prodGroup)
                {
                    totalCounter++;

                    if (product.Result_Value == ResultTestEnum.PASS)
                        passCounter++;
                }
                var exProduct = prodGroup.First();

                var prodResult = new AvgPassRateResponse()
                {
                    AvgPassRate = passCounter / totalCounter,
                    Code = exProduct.Product_Code,
                    Family = exProduct.Product_Family,
                    HwVersion = exProduct.Product_HwVersion,
                    Name = exProduct.Product_Name,
                    SFCode = exProduct.Product_SFCode,
                    SFIdString = exProduct.Product_SFIdString,
                    SFSN = exProduct.Product_SFSN,
                    SN = exProduct.Product_SN
                };

                result.Add(prodResult);
            }




            //var result = new List<AvgPassRateResponse> {
            //    new AvgPassRateResponse
            //    {
            //        AvgPassRate = 0.8,
            //        Code = "N/A",
            //        Family = "IL4",
            //        HwVersion = "N/A",
            //        Name = "IL4 PG24A",
            //        SFCode = "PG24A",
            //        SFIdString = "PG24ANV21510A00",
            //        SFSN = "21510A00",
            //        SN = "N/A"
            //    },
            //    new AvgPassRateResponse
            //    {
            //        AvgPassRate = 0.2,
            //        Code = "N/A",
            //        Family = "IL5",
            //        HwVersion = "N/A",
            //        Name = "IL5 PG24B",
            //        SFCode = "PG24B",
            //        SFIdString = "PG24BNV21510A00",
            //        SFSN = "21510B00",
            //        SN = "N/A"
            //    },
            //};

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetAvgTestLength([FromBody] DateTime from, [FromBody] DateTime to)
        {


            return Ok();
        }
    }
}
