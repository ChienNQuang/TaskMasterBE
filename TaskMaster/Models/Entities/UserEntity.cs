using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Models.Entities;

public class UserEntity : Entity<Guid>
{
    [EmailAddress]
    [MaxLength(320)]
    public string Email { get; set; }
    [MaxLength(100)]
    public string Username { get; set; }
    [MaxLength(256)]
    public string HashedPassword { get; set; }

    public bool Active { get; set; } = true;
    public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}