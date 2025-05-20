using HabitTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HabitTrackerAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Habit> Habits => Set<Habit>();
    public DbSet<Reflection> Reflections => Set<Reflection>();
}