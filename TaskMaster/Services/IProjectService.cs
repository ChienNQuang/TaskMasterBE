using Microsoft.AspNetCore.JsonPatch;
using TaskMaster.Controllers.Payloads.Requests;
using TaskMaster.Models.Dtos;

namespace TaskMaster.Services;

public interface IProjectService
{
    Task<IEnumerable<ProjectDTO>> GetProjectsOfUser(Guid userId);
    Task<ProjectDTO> GetProjectOfUserById(Guid userId, Guid projectId);
    Task<ProjectDTO> CreateProjectOfUser(Guid userId, ProjectCreateRequest request);
    Task<ProjectDTO> UpdateProjectOfUser(Guid userId, Guid projectId, JsonPatchDocument<ProjectDTO> request);
    Task<ProjectDTO> DeleteProjectOfUser(Guid userId, Guid projectId);
}