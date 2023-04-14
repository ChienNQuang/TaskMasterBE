using Microsoft.AspNetCore.Mvc;
using TaskMaster.Models.Dtos;
using TaskMaster.Services;

namespace TaskMaster.Controllers;

[Route("users/{id}/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;
    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjectsOfUser(Guid id)
    {
        var projects = await _projectService.GetProjectsByUserIdAsync();
        return Ok(projects);
    }
    
    
}