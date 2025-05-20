using System.Security.Claims;
using HabitTrackerAPI.Data;
using HabitTrackerAPI.Dto.Reflection;
using HabitTrackerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HabitTrackerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReflectionsController : ControllerBase
{
    private readonly DataContext _context;

    public ReflectionsController(DataContext context)
    {
        _context = context;
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<ActionResult<ReflectionDto>> Crear(ReflectionCreateDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var habit = await _context.Habits.FirstOrDefaultAsync(h => h.Id == dto.HabitId && h.UserId == userId);
        if (habit == null) return BadRequest("Hábito no encontrado o no te pertenece.");

        var reflection = new Reflection
        {
            Content = dto.Content,
            HabitId = dto.HabitId,
            UserId = userId,
            Date = DateTime.UtcNow
        };

        _context.Reflections.Add(reflection);
        await _context.SaveChangesAsync();

        return new ReflectionDto
        {
            Id = reflection.Id,
            Content = reflection.Content,
            Date = reflection.Date,
            HabitId = reflection.HabitId,
            HabitName = habit.Name
        };
    }

    [Authorize(Roles = "User")]
    [HttpGet("mias")]
    public async Task<ActionResult<List<ReflectionDto>>> VerMias()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var data = await _context.Reflections
            .Where(r => r.UserId == userId)
            .Include(r => r.Habit)
            .Select(r => new ReflectionDto
            {
                Id = r.Id,
                Content = r.Content,
                Date = r.Date,
                HabitId = r.HabitId,
                HabitName = r.Habit.Name
            }).ToListAsync();

        return Ok(data);
    }

    [Authorize(Roles = "Coach")]
    [HttpGet("asignado/{userId}")]
    public async Task<ActionResult<List<ReflectionDto>>> VerDeAsignado(int userId)
    {
        var coachId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var usuario = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId && u.CoachId == coachId);

        if (usuario == null) return Forbid();

        var reflexiones = await _context.Reflections
            .Where(r => r.UserId == userId)
            .Include(r => r.Habit)
            .Select(r => new ReflectionDto
            {
                Id = r.Id,
                Content = r.Content,
                Date = r.Date,
                HabitId = r.HabitId,
                HabitName = r.Habit.Name
            }).ToListAsync();

        return Ok(reflexiones);
    }
}
