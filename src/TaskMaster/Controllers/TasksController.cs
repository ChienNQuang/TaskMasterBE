using Microsoft.AspNetCore.Mvc;
using TaskMaster.Models.Entities;
using TaskMaster.Repositories;
using TaskMaster.Services;

namespace TaskMaster.Controllers;

[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    public TasksController(IUnitOfWork uow, ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetTasksByProject(Guid projectId)
    {
        var tasks = await _taskService.GetTasksByProjectIdAsync(projectId);
        return Ok(tasks);
    }
}