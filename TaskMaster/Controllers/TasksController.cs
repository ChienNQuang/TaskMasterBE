using Microsoft.AspNetCore.Mvc;
using TaskMaster.Models.Entities;
using TaskMaster.Repositories;
using TaskMaster.Services;

namespace TaskMaster.Controllers;

[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    public TasksController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    [HttpGet()]
    public IActionResult GetTasksByProject()
    {
        var taskRepo = _uow.GetRequiredRepository<TaskEntity, Guid>();
        var tasks = taskRepo.GetAll();
        // var tasks = await _service.GetTasksAsync();
        return Ok(tasks);
    }
}