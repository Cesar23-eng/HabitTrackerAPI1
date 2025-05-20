using System.ComponentModel.DataAnnotations;

namespace HabitTrackerAPI.Models;

public class Habit
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public int UserId { get; set; }
}