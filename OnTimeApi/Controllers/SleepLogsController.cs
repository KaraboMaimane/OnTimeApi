using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnTimeApi.Data;
using OnTimeApi.Models;



namespace OnTimeApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SleepLogsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SleepLogsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSleepLogs([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var userId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid userGuid)) { return Unauthorized(); }


            var query = _context.SleepLogs.Where(log => log.UserId == userGuid);

            if (startDate.HasValue)
            {
                query = query.Where(log => log.Date >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                query = query.Where(log => log.Date <= endDate.Value);
            }

            var sleepLogs = await query.ToListAsync();
            return Ok(sleepLogs);
        }

        [HttpPost]
        public async Task<IActionResult> AddSleepLog([FromBody] SleepLog sleepLog)
        {
            var userId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid userGuid)) { return Unauthorized(); }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            sleepLog.UserId = userGuid;
            sleepLog.Id = Guid.NewGuid();
            _context.SleepLogs.Add(sleepLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSleepLogs), new { id = sleepLog.Id }, sleepLog); // Return 201 Created

        }
    }
}