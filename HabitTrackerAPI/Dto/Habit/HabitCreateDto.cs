using System.ComponentModel.DataAnnotations;

namespace HabitTrackerAPI.Dto.Habit;

public class HabitCreateDto
{
    [Required]
    public string Name { get; set; } = null!;
}