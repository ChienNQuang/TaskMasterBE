using TaskMaster.Models.Entities;

namespace TaskMaster.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskEntity>> GetTasksAsync();
    Task<TaskEntity> GetTaskByIdAsync();
}