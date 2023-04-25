using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskMaster.Controllers.Payloads.Requests;
using TaskMaster.Exceptions;
using TaskMaster.Helpers;
using TaskMaster.Models.Dtos;
using TaskMaster.Models.Entities;
using TaskMaster.Repositories;
using TaskMaster.Validators;

namespace TaskMaster.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _uow;
    private readonly IRepository<UserEntity, Guid> _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
        _userRepository = _uow.GetRequiredRepository<UserEntity, Guid>();
    }

    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        var entities = await _userRepository.GetAll().ToListAsync();
        var result = _mapper.Map<List<UserDto>>(entities);
        return result;
    }

    public async Task<UserDto> GetUserById(Guid id)
    {
        var userEntity = await _userRepository.GetByIdAsync(id);
        if (userEntity is null)
        {
            throw new KeyNotFoundException($"User with id {id} does not exist");
        }
        
        var result = _mapper.Map<UserDto>(userEntity);
        return result;
    }

    public async Task<UserDto> AddUser(UserCreateRequest request)
    {
        // add check email later
        var validator = new UserValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new UserValidationException(validationResult.Errors);
            // var exceptions = new List<Exception>();
            // validationResult.Errors.ForEach(vf =>
            // {
            //     var item = new Exception(vf.);
            //     exceptions.Add(item);
            // });
            // throw new AggregateException(exceptions);
        }
        request.Password = SecurityUtil.Hash(request.Password);
        var userEntityToAdd = _mapper.Map<UserEntity>(request);
        var userEntity = await _userRepository.AddAsync(userEntityToAdd);
        var addResult = await _uow.CommitAsync();
        if (addResult <= 0)
        {
            throw new ApplicationException("Cannot add user");
        }
        var result = _mapper.Map<UserDto>(userEntity);
        return result;
    }

    public async Task<UserDto> UpdateUser(UserUpdateRequest request)
    {
        var userEntityToUpdate = await _userRepository.GetByIdAsync(request.Id);
        if (userEntityToUpdate is null)
        {
            throw new KeyNotFoundException($"User with id {request.Id} does not exist");
        }
        userEntityToUpdate.HashedPassword = SecurityUtil.Hash(request.Password);
        var userEntity = _userRepository.Update(userEntityToUpdate);
        var updateResult = await _uow.CommitAsync();
        if (updateResult <= 0)
        {
            throw new ApplicationException("Cannot update user");
        }
        var result = _mapper.Map<UserDto>(userEntity);
        return result;
    }

    public async Task<UserDto> DeactivateUser(Guid id)
    {
        var userEntityToUpdate = await _userRepository.GetByIdAsync(id);
        if (userEntityToUpdate is null)
        {
            throw new KeyNotFoundException($"User with id {id} does not exist");
        }
        userEntityToUpdate.Active = false;
        var userEntity = _userRepository.Update(userEntityToUpdate);
        var updateResult = await _uow.CommitAsync();
        if (updateResult <= 0)
        {
            throw new ApplicationException("Cannot deactivate user");
        }
        var result = _mapper.Map<UserDto>(userEntity);
        return result;
    }
}