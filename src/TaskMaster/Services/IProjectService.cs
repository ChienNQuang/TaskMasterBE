using Microsoft.AspNetCore.JsonPatch;
using TaskMaster.Controllers.Payloads.Requests;
using TaskMaster.Models.Dtos;

namespace TaskMaster.Services;

public interface IProjectService
{
    Task<IEnumerable<ProjectDto>> GetProjectsOfUser(Guid userId);
    Task<ProjectDto> GetProjectOfUserById(Guid userId, Guid projectId);
    Task<ProjectDto> CreateProjectOfUser(Guid userId, ProjectCreateRequest request);
    Task<ProjectDto> UpdateProjectOfUser(Guid userId, Guid projectId, ProjectUpdatePatchRequest request);
    Task<ProjectDto> DeleteProjectOfUser(Guid userId, Guid projectId);
}