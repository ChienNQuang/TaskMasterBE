using TaskMaster.Models.Dtos;

namespace TaskMaster.Services;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetUsers();
    Task<UserDTO> GetUserById(Guid id);
    Task<UserDTO> AddUser(UserDTO user);
    Task<UserDTO> UpdateUser(UserDTO user);
    Task<UserDTO> DeactivateUser(Guid id);
}