using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace BackendWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class BaseAnalysisController : ControllerBase
    {
        private CommonDbContext _context;

        public class FromToRequest
        {
            public DateTime from { get; set; }
            public DateTime to { get; set; }
        }


        public BaseAnalysisController(CommonDbContext context)
        {
            _context = context;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<List<AvgPassRateResponse>>> GetAvgPassRate([FromBody] FromToRequest req)
        {
            DateTime from = req.from;
            DateTime to = req.to;

            var prodGroups = _context.Heads
                .Where(x => x.TimeStamp >= from)
                .Where(x => x.TimeStamp <= to)
                .GroupBy(x => x.Product_SFIdString)
                .Select(x => x.ToList())
                .ToList();



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

            return Ok(result);
        }

        public class PayloadDuration
        { 
            public DateTime From { get; set; }
            public DateTime To { get; set; }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<List<TestDurationResponse>>> GetTestDurations([FromBody] PayloadDuration payload)
        {
            var prodGroups = _context.Heads
               .Where(x => x.TimeStamp >= payload.From)
               .Where(x => x.TimeStamp <= payload.To)
               .GroupBy(x => x.Product_SFIdString)
               .Select(x => x.ToList())
               .ToList();


            var result = new List<TestDurationResponse> {
                new TestDurationResponse
                {
                    TestDuration = 32.4,
                    Code = "N/A",
                    Family = "IL4",
                    HwVersion = "N/A",
                    Name = "IL4 PG24A",
                    SFCode = "PG24A",
                    SFIdString = "PG24ANV21510A00",
                    SFSN = "21510A00",
                    SN = "N/A"
                },
                new TestDurationResponse
                {
                    TestDuration = 15.3,
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

            result = result.OrderBy(x => x.TestDuration).ToList();

            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<WeeklyPassRateResponse>>> GetWeeklyPassRate()
        {
            //TODO query analisis analysis
            //LIB.GETAVG(from, to)

            var result = new List<WeeklyPassRateResponse> {
                new WeeklyPassRateResponse
                {
                    WeeklyPassRate = new double[] {  0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7 },
                    Code = "N/A",
                    Family = "IL4",
                    HwVersion = "N/A",
                    Name = "IL4 PG24A",
                    SFCode = "PG24A",
                    SFIdString = "PG24ANV21510A00",
                    SFSN = "21510A00",
                    SN = "N/A"
                },
                new WeeklyPassRateResponse
                {
                    WeeklyPassRate = new double[] {  0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7 }.Reverse().ToArray(),
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

        [HttpGet("[action]")]
        public async Task<ActionResult<List<AvgPassRateResponse>>> GetMergedWeeklyPassRate()
        {
            //TODO query analisis analysis
            //LIB.GETAVG(from, to)

            var result = new double[] { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7 };

            return Ok(result);
        }
    }
}
