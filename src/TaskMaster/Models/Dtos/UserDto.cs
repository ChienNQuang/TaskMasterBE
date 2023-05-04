using NodaTime;

namespace TaskMaster.Models.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public bool Active { get; set; }
    public DateOnly CreationDate { get; set; }
}