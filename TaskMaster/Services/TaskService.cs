using TaskMaster.Models.Dtos;
using TaskMaster.Models.Entities;

namespace TaskMaster.Services;

public class TaskService : ITaskService
{
    public Task<IEnumerable<TaskDto>> GetTasksByProjectIdAsync(Guid projectId)
    {
        throw new NotImplementedException();
    }
}