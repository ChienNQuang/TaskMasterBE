using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using TaskMaster.Controllers.Payloads.Requests;
using TaskMaster.Models.Dtos;
using TaskMaster.Models.Entities;
using TaskMaster.Repositories;

namespace TaskMaster.Services;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IRepository<UserEntity, Guid> _userRepository;
    private readonly IRepository<ProjectEntity, Guid> _projectRepository;

    public ProjectService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
        _userRepository = _uow.GetRequiredRepository<UserEntity, Guid>();
        _projectRepository = _uow.GetRequiredRepository<ProjectEntity, Guid>();
    }
    public async Task<IEnumerable<ProjectDTO>> GetProjectsOfUser(Guid userId)
    {
        var projectEntities = _projectRepository.GetAll();
        var projects = projectEntities.Where(p => p.Owner.Id == userId);
        var result = _mapper.Map<List<ProjectDTO>>(await projects.ToListAsync());
        return result;
    }

    public async Task<ProjectDTO> GetProjectOfUserById(Guid userId, Guid projectId)
    {
        var user = await GetUserById(userId);

        var project = await _projectRepository.GetByIdAsync(projectId);
        if (project is null || project.Owner.Id.Equals(user.Id))
        {
            throw new KeyNotFoundException("Project does not exist!");
        }

        var result = _mapper.Map<ProjectDTO>(project);
        return result;
    }

    public async Task<ProjectDTO> CreateProjectOfUser(Guid userId, ProjectCreateRequest request)
    {
        var user = await GetUserById(userId);

        var project = _mapper.Map<ProjectDTO>(request);
        project.Owner = _mapper.Map<UserDTO>(user);
        var projectToAdd = _mapper.Map<ProjectEntity>(project);
        var addedProject = await _projectRepository.AddAsync(projectToAdd);
        var addedResult = await _uow.CommitAsync();
        if (addedResult <= 0) throw new ApplicationException("Something went wrong");
        var result = _mapper.Map<ProjectDTO>(addedProject);
        return result;
    }

    public async Task<ProjectDTO> UpdateProjectOfUser(Guid userId, Guid projectId, JsonPatchDocument<ProjectDTO> request)
    {
        var user = await GetUserById(userId);

        var projectEntity = await _projectRepository.GetByIdAsync(projectId);
        if (projectEntity is null)
        {
            throw new KeyNotFoundException("Project does not exist!");
        }

        var project = _mapper.Map<ProjectDTO>(projectEntity);
        request.ApplyTo(project);
        return project;
    }

    public Task<ProjectDTO> DeleteProjectOfUser(Guid userId, Guid projectId)
    {
        throw new NotImplementedException();
    }
    
    private async Task<UserEntity> GetUserById(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
        {
            throw new KeyNotFoundException("User does not exist!");
        }

        return user;
    }
}