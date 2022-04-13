using BackendWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace BackendWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RawDataController : ControllerBase
    {
        private CommonDbContext _context;
        private ILogger<RawDataController> _log;

        public RawDataController(CommonDbContext context, ILogger<RawDataController> log)
        {
            _context = context;
            _log = log;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Head>>> GetAllHeads()
        {
            return await _context.Heads.ToListAsync();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Group>>> GetAllGroups()
        {
            return await _context.Groups.ToListAsync();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Test>>> GetAllTests()
        {
            return await _context.Tests.ToListAsync();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<int>> AddNewHead([FromBody] Head head)
        {
            _context.Heads.Add(head);
            int _id = (await _context.Heads.FindAsync(head))!.Id;
            await _context.SaveChangesAsync();

            return Ok(_id);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<int>> AddNewTest([FromBody] Test test)
        {
            _context.Tests.Add(test);
            int _id = (await _context.Tests.FindAsync(test))!.Id;
            await _context.SaveChangesAsync();

            return Ok(_id);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<int>> AddNewGroup([FromBody] Group group)
        {
            _context.Groups.Add(group);
            int _id = (await _context.Groups.FindAsync(group))!.Id;
            await _context.SaveChangesAsync();

            return Ok(_id);
        }
    }
}
