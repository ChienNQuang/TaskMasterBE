using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using TaskMaster.Controllers.Payloads.Requests;
using TaskMaster.Exceptions;
using TaskMaster.Models.Dtos;
using TaskMaster.Models.Entities;
using TaskMaster.Repositories;
using TaskMaster.Validators;

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
    public async Task<IEnumerable<ProjectDto>> GetProjectsOfUser(Guid userId)
    {
        await GetUserById(userId);
        var projectEntities = _projectRepository.GetAll();
        var projects = projectEntities.Where(p => p.Owner.Id == userId);
        var result = _mapper.Map<List<ProjectDto>>(await projects.ToListAsync());
        return result;
    }

    public async Task<ProjectDto> GetProjectOfUserById(Guid userId, Guid projectId)
    {
        var user = await GetUserById(userId);

        var project = await _projectRepository.GetByIdAsync(projectId);
        if (project is null || !project.Owner.Id.Equals(user.Id))
        {
            throw new KeyNotFoundException("Project does not exist!");
        }

        var result = _mapper.Map<ProjectDto>(project);
        return result;
    }

    public async Task<ProjectDto> CreateProjectOfUser(Guid userId, ProjectCreateRequest request)
    {
        if (request is null)
        {
            throw new ArgumentException(
                "The request was invalid because one or more properties were not correctly formatted");
        }
        var validator = ProjectCreateRequestValidator.New();
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new RequestValidationException(validationResult.Errors);
        }
        var user = await GetUserById(userId);
        if (user.Active is false)
        {
            throw new ArgumentException($"User with id {userId} is deactivated!");
        }

        var projectToAdd = _mapper.Map<ProjectEntity>(request);
        projectToAdd.Owner = user;
        var addedProject = await _projectRepository.AddAsync(projectToAdd);
        var addResult = await _uow.CommitAsync();
        if (addResult <= 0)
        {
            throw new DbUpdateException("Something went wrong");
        }
        var result = _mapper.Map<ProjectDto>(addedProject);
        return result;
    }

    public async Task<ProjectDto> UpdateProjectOfUser(Guid userId, Guid projectId, ProjectUpdatePatchRequest request)
    {
        if (request is null)
        {
            throw new ArgumentException(
                "The request was invalid because one or more properties were not correctly formatted");
        }
        var user = await GetUserById(userId);
        if (user.Active is false)
        {
            throw new ArgumentException($"User with id {userId} is deactivated!");
        }

        var projectEntity = await _projectRepository.GetByIdAsync(projectId);
        if (projectEntity is null || !projectEntity.Owner.Id.Equals(user.Id))
        {
            throw new KeyNotFoundException("Project does not exist!");
        }

        var validator = ProjectUpdatePatchRequestValidator.New(_projectRepository, projectId);
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new RequestValidationException(validationResult.Errors);
        }

        if (request.Description?.Value is not null)
        {
            projectEntity.Description = request.Description.Value;
        }

        if (request.StartDate is not null)
        {
            projectEntity.StartDate = LocalDateTime.FromDateTime(request.StartDate!.Value);
        }
        
        if (request.EndDate is not null)
        {
            projectEntity.EndDate = LocalDateTime.FromDateTime(request.EndDate.Value);
        }

        if (request.Status is not null)
        {
            projectEntity.Status = request.Status.Value;
        }

        var updatedProject = _projectRepository.Update(projectEntity);
        var updateResult = await _uow.CommitAsync();
        if (updateResult <= 0)
        {
            throw new DbUpdateException("Cannot update!");
        }

        var project = _mapper.Map<ProjectDto>(updatedProject);
        return project;
    }

    public async Task<ProjectDto> DeleteProjectOfUser(Guid userId, Guid projectId)
    {
        var user = await GetUserById(userId);
        if (user.Active is false)
        {
            throw new ArgumentException($"User with id {userId} is deactivated!");
        }

        var projectEntity = await _projectRepository.GetByIdAsync(projectId);
        if (projectEntity is null)
        {
            throw new KeyNotFoundException("Project does not exist!");
        }

        var deletedProject = _projectRepository.Remove(projectEntity.Id);
        var deleteResult = await _uow.CommitAsync();
        if (deleteResult <= 0)
        {
            throw new DbUpdateException($"Cannot delete project with id {projectId}");
        }

        var project = _mapper.Map<ProjectDto>(deletedProject);
        return project;
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