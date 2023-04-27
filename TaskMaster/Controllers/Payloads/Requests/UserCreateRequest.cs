using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Controllers.Payloads.Requests;

public record UserCreateRequest
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}