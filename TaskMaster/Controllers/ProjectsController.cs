using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TaskMaster.Controllers.Payloads.Requests;
using TaskMaster.Controllers.Payloads.Responses;
using TaskMaster.Models.Dtos;
using TaskMaster.Services;

namespace TaskMaster.Controllers;

[Route("users/{userId:guid}/[controller]", Name = "projects")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;
    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjectsOfUser(Guid userId)
    {
        var projects = await _projectService.GetProjectsOfUser(userId);
        return Ok(ApiResponse<IEnumerable<ProjectDto>>.Succeed(projects));
    }

    [HttpGet("{projectId:guid}")]
    public async Task<ActionResult<ProjectDto>> GetProjectOfUserById(Guid userId, Guid projectId)
    {
        var project = await _projectService.GetProjectOfUserById(userId, projectId);
        return Ok(ApiResponse<ProjectDto>.Succeed(project));
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDto>> CreateProject(Guid userId, [FromBody] ProjectCreateRequest project)
    {
        var createdProject = await _projectService.CreateProjectOfUser(userId, project);
        ApiResponse<ProjectDto>.Succeed(createdProject);
        return Created("", ApiResponse<ProjectDto>.Succeed(createdProject));
    }

    [HttpPatch("{projectId:guid}")]
    public async Task<ActionResult<ProjectDto>> UpdateProject(Guid userId, Guid projectId, [FromBody] ProjectUpdatePatchRequest request)
    {
        var updatedProject = await _projectService.UpdateProjectOfUser(userId, projectId, request);
        return Ok(ApiResponse<ProjectDto>.Succeed(updatedProject));
    }

    [HttpDelete("{projectId:guid}")]
    public async Task<ActionResult<ProjectDto>> DeleteProject(Guid userId, Guid projectId)
    {
        var deletedProject = await _projectService.DeleteProjectOfUser(userId, projectId);
        return Ok(ApiResponse<ProjectDto>.Succeed(deletedProject));
    }
}