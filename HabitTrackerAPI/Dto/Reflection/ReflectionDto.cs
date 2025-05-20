namespace HabitTrackerAPI.Dto.Reflection;

public class ReflectionDto
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime Date { get; set; }
    public int HabitId { get; set; }
    public string HabitName { get; set; } = null!;
}