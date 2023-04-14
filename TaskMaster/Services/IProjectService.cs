using TaskMaster.Models.Dtos;

namespace TaskMaster.Services;

public interface IProjectService
{
    Task<IEnumerable<ProjectDTO>> GetProjectsByUserIdAsync();
}