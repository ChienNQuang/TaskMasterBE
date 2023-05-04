namespace TaskMaster.Controllers.Payloads.Requests;

public class AuthenticateRequest
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
}