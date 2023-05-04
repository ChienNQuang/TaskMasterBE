using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace TaskMaster.Models.Entities;

public class UserEntity : Entity<Guid>
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string HashedPassword { get; set; }
    public bool Active { get; set; }
    public LocalDate CreationDate { get; set; }
}