using TaskMaster.Models.Entities;

namespace TaskMaster.Services;

public class TaskService : ITaskService
{
    public Task<IEnumerable<TaskEntity>> GetTasksAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TaskEntity> GetTaskByIdAsync()
    {
        throw new NotImplementedException();
    }
}