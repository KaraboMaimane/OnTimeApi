using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnTimeApi.Data;

namespace OnTimeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PreferencesController: ControllerBase
{
    private readonly AppDbContext _context;
    
    public PreferencesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetPreferences()
    {
        var userId = User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }
        
        if(!Guid.TryParse(userId, out Guid userIdGuid))
        {
            return BadRequest("Invalid user id format");
        }
        
        var preferences = await _context.UserPreferences.FirstOrDefaultAsync(p => p.UserId == userIdGuid);

        if (preferences == null)
        {
            return NotFound();
        }
        
        return Ok(preferences);
    }
}