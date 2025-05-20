using System.ComponentModel.DataAnnotations;

namespace HabitTrackerAPI.Dto.User;

public class UserRegisterDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = null!;


    [Required, MinLength(6)]
    public string Password { get; set; }

    [Required]
    public string Role { get; set; } // "User", "Coach", "Admin"
}