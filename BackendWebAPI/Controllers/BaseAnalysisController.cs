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
                .GroupBy(x => x.Product_Name)
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
                    AvgPassRate = (double)passCounter / totalCounter,
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

        [HttpPost("[action]")]
        public async Task<ActionResult<List<TestDurationResponse>>> GetTestDurations([FromBody] FromToRequest payload)
        {
            var prodGroups = _context.Heads
               .Where(x => x.TimeStamp >= payload.from)
               .Where(x => x.TimeStamp <= payload.to)
               .GroupBy(x => x.Product_Name)
               .Select(x => x.ToList())
               .ToList();

            var result = new List<TestDurationResponse>();
            foreach (var prodGroup in prodGroups)
            {
                float totalCounter = 0;
                foreach (var product in prodGroup)
                {
                    totalCounter += product.TestTotalTime;
                }

                var exProduct = prodGroup.First();

                var prodResult = new TestDurationResponse()
                {
                    TestDuration = (double)totalCounter / prodGroup.Count,
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

        [HttpGet("[action]")]
        public async Task<ActionResult<List<WeeklyPassRateResponse>>> GetWeeklyPassRate()
        {
            DateTime to = Convert.ToDateTime("2022-03-01 00:39:50.8");//DateTime.Now;
            DateTime from = new DateTime(to.Ticks - (TimeSpan.TicksPerDay * 7)); //week ago
            //for mocking pass rate
            Random r = new Random();
            var wekkGrouppedByHead = _context.Heads
               .Where(x => x.TimeStamp >= from)
               .Where(x => x.TimeStamp <= to)
               .GroupBy(x => x.Product_Name)
               .Select(x => x.ToList())
               .ToList();

            var result = new List<WeeklyPassRateResponse>();

            //iterate over all heads
            foreach (var prodGroup in wekkGrouppedByHead)
            {
                double[] passes = new double[7];

                from = to;
                //for each day
                for (int i = 1; i <= 7; i++)
                {
                    to = from;
                    from = new DateTime(from.Ticks - TimeSpan.TicksPerDay * i);

                    var headsInDay = prodGroup
                        .Where(x => x.TimeStamp >= from)
                        .Where(x => x.TimeStamp <= to)
                        .ToList();
                    if (headsInDay.Count == 0)
                        continue;

                    int passCounter = 0;
                    int totalCounter = 0;
                    foreach (var product in headsInDay)
                    {
                        totalCounter++;

                        if (product.Result_Value == ResultTestEnum.PASS)
                            passCounter++;
                    }

                    //for mocking pass rates
                    double rDouble = r.NextDouble();
                    passes[i - 1] = Math.Min(rDouble + 0.5, 1.0);

                    //passes[i - 1] = (double)passCounter / totalCounter;

                    //var exProduct = prodGroup.First();
                }

                var exProduct = prodGroup.First();

                var prodResult = new WeeklyPassRateResponse()
                {
                    WeeklyPassRate = passes,
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



            //var result = new List<WeeklyPassRateResponse> {
            //    new WeeklyPassRateResponse
            //    {
            //        WeeklyPassRate = new double[] {  0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7 },
            //        Code = "N/A",
            //        Family = "IL4",
            //        HwVersion = "N/A",
            //        Name = "IL4 PG24A",
            //        SFCode = "PG24A",
            //        SFIdString = "PG24ANV21510A00",
            //        SFSN = "21510A00",
            //        SN = "N/A"
            //    },
            //    new WeeklyPassRateResponse
            //    {
            //        WeeklyPassRate = new double[] {  0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7 }.Reverse().ToArray(),
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

        [HttpGet("[action]")]
        public async Task<ActionResult<List<AvgPassRateResponse>>> GetMergedWeeklyPassRate()
        {
            //TODO query analisis analysis
            //LIB.GETAVG(from, to)

            var result = new double[] { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7 };

            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<AllertResponse>>> Allert()
        {
            var result = new List<AllertResponse>();
            result.Add(new AllertResponse()
            {
                ProductName = "IL4 PG24A",
                Severenity = 1,
                Reason = "Tests duration are slowed by 25% in this week."
            });

            result.Add(new AllertResponse()
            {
                ProductName = "IL4 PG24D",
                Severenity = 2,
                Reason = "Tests failures increased by 30 % in this week."
            });

            return Ok(result);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<List<WeeklyStatsResponse>>> GetWeeklyStats()
        {
            //TODO query analisis analysis
            //LIB.GETAVG(from, to)

            var result = new WeeklyStatsResponse
            {
                Passed = 542,
                Failed = 13,
                NumberOfGroups = 60,
                AverageRunTime = 53.6,
                PassedDiff = 0.05
            };

            return Ok(result);
        }
    }
}
