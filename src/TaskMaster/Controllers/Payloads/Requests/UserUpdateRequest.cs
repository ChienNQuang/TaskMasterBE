using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Controllers.Payloads.Requests;

public record UserUpdateRequest
{
    [Required]
    public string Password { get; set; }
}