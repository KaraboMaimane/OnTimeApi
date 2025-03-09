using System.ComponentModel.DataAnnotations;

namespace OnTimeApi.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }

    public DateTime Created_At { get; set; }
}