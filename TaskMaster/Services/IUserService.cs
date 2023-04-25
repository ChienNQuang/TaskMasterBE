using TaskMaster.Controllers.Payloads.Requests;
using TaskMaster.Models.Dtos;

namespace TaskMaster.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetUsers();
    Task<UserDto> GetUserById(Guid id);
    Task<UserDto> AddUser(UserCreateRequest request);
    Task<UserDto> UpdateUser(UserUpdateRequest request);
    Task<UserDto> DeactivateUser(Guid id);
}