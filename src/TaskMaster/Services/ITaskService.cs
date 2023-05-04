using TaskMaster.Models.Dtos;
using TaskMaster.Models.Entities;

namespace TaskMaster.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetTasksByProjectIdAsync(Guid projectId);
}