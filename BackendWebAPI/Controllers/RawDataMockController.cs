using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace BackendWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RawDataMockController : ControllerBase
    {

        [HttpGet("[action]")]
        public ActionResult<List<Group>> GetAllGroups()
        {
            var result = new List<Group>()
            {
                //TODO make mock data
            };

            return Ok(result);

        }


        [HttpGet("[action]")]
        public ActionResult<List<Head>> GetAllHeads()
        {
            var result = new List<Head>()
            {
                //TODO make mock data
            };

            return Ok(result);
        }

        [HttpGet("[action]")]
        public ActionResult<List<Test>> GetAllTests()
        {
            var result = new List<Test>()
            {
                //TODO make mock data
            };

            return Ok(result);
        }
    }
}
