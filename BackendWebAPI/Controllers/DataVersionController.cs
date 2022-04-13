using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace BackendWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class DataVersionController : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<ActionResult<int>> GetDataVersion()
        {
            //TODO query analisis analysis
            //LIB.GETAVG(from, to)

            var result = new Random().Next(1, 100);

            return Ok(result);
        }
    }
}
