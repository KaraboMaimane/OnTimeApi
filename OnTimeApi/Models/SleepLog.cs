using System.ComponentModel.DataAnnotations;

namespace OnTimeApi.Models;

public class SleepLog
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    public DateTime Created_At { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public TimeSpan ActualSleepTime { get; set; }
    
    [Required]
    public TimeSpan ActualWakeTime { get; set; }
}