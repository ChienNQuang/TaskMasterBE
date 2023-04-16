using TaskMaster.Controllers.Payloads.Requests;
using TaskMaster.Models.Dtos;

namespace TaskMaster.Services;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetUsers();
    Task<UserDTO?> GetUserById(Guid id);
    Task<UserDTO?> AddUser(UserCreateRequest request);
    Task<UserDTO?> UpdateUser(UserUpdateRequest request);
    Task<UserDTO?> DeactivateUser(Guid id);
}