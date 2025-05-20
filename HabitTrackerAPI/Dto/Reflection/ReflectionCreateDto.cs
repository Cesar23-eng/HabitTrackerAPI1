using System.ComponentModel.DataAnnotations;

namespace HabitTrackerAPI.Dto.Reflection;

public class ReflectionCreateDto
{
    [Required]
    public string Content { get; set; } = null!;

    [Required]
    public int HabitId { get; set; }
}