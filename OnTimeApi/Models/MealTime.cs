using System.ComponentModel.DataAnnotations;

namespace OnTimeApi.Models;

public class MealTime
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public TimestampAttribute CreatedAt { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public string DayOfWeek { get; set; }
    
    [Required]
    public string MealType { get; set; }
    
    [Required]
    public TimeSpan Time { get; set; }
}