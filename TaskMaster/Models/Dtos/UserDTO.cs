using NodaTime;

namespace TaskMaster.Models.Dtos;

public class UserDTO
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public bool Active { get; set; }
    public LocalDate CreationDate { get; set; }
}