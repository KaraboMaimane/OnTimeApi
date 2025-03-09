using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnTimeApi.Data;
using OnTimeApi.Models;

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

    // GET: api/Preferences
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
    
    // POST: api/Preferences
    [HttpPost]
    public async Task<IActionResult> CreateOrUpdatePreferences([FromBody] UserPreference preferences)
    {
        var userId = User.FindFirst("sub")?.Value;

        if(string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid userGuid))
        {
            return Unauthorized();
        }


        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingPreferences = await _context.UserPreferences
            .FirstOrDefaultAsync(p => p.UserId == userGuid);

        if (existingPreferences == null)
        {
            // Create new preferences
            preferences.UserId = userGuid; // Ensure the UserId is set
            preferences.Id = Guid.NewGuid();
            _context.UserPreferences.Add(preferences);
        }
        else
        {
            // Update existing preferences
            existingPreferences.IdealSleepTime = preferences.IdealSleepTime;
            existingPreferences.IdealWakeTime = preferences.IdealWakeTime;
            existingPreferences.WorkAddress = preferences.WorkAddress;
            existingPreferences.CommuteStartTime = preferences.CommuteStartTime;
            existingPreferences.GymTime = preferences.GymTime;
            existingPreferences.GymDuration = preferences.GymDuration;
        }

        await _context.SaveChangesAsync();

        return Ok(existingPreferences ?? preferences); // Return the updated or new preferences
    }
}