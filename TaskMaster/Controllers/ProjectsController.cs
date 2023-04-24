using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TaskMaster.Controllers.Payloads.Requests;
using TaskMaster.Models.Dtos;
using TaskMaster.Services;

namespace TaskMaster.Controllers;

[Route("users/{userId}/[controller]", Name = "projects")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;
    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProjectsOfUser(Guid userId)
    {
        var projects = await _projectService.GetProjectsOfUser(userId);
        return Ok(projects);
    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> GetProjectOfUserById(Guid userId, Guid projectId)
    {
        var project = await _projectService.GetProjectOfUserById(userId, projectId);
        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(Guid userId, [FromBody] ProjectCreateRequest project)
    {
        var createdProject = await _projectService.CreateProjectOfUser(userId, project);
        return Ok(createdProject);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateProject(Guid userId, Guid projectId, [FromBody] ProjectUpdatePatchRequest request)
    {
        var updatedProject = await _projectService.UpdateProjectOfUser(userId, projectId, request);
        return Ok(updatedProject);
    }

    [HttpDelete("{projectId}")]
    public async Task<IActionResult> DeleteProject(Guid userId, Guid projectId)
    {
        var deletedProject = await _projectService.DeleteProjectOfUser(userId, projectId);
        return Ok(deletedProject);
    }
}