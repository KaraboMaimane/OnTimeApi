using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnTimeApi.Models;

public class UserPreference
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    
    [Required]
    public TimeSpan IdealSleepTime { get; set; }
    
    [Required]
    public TimeSpan IdealWakeTime { get; set; }
    
    [Required]
    public string WorkAddress { get; set; }
    
    [Required]
    public TimeSpan CommuteStartTime { get; set; }
    
    public TimeSpan? GymTime { get; set; }
    
    public int? GymDuration { get; set; }
}