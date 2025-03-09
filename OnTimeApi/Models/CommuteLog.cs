using System.ComponentModel.DataAnnotations;

namespace OnTimeApi.Models;

public class CommuteLog
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public TimeSpan EstimatedCommuteTime { get; set; }
    
    [Required]
    public TimeSpan ActualArrivalTime { get; set; }
}