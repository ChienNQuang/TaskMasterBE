using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Models.Entities;

public class UserEntity
{
    [Key]
    public Guid Id { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Username { get; set; }
    public string HashedPassword { get; set; }
}