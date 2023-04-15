using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskMaster.Controllers.Payloads.Requests;
using TaskMaster.Helpers;
using TaskMaster.Models.Dtos;
using TaskMaster.Models.Entities;
using TaskMaster.Repositories;

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

    public async Task<IEnumerable<UserDTO>> GetUsers()
    {
        var entities = await _userRepository.GetAll().ToListAsync();
        var result = _mapper.Map<List<UserDTO>>(entities);
        return result;
    }

    public async Task<UserDTO?> GetUserById(Guid id)
    {
        var userEntity = await _userRepository.GetByIdAsync(id);
        var result = _mapper.Map<UserDTO>(userEntity);
        return result;
    }

    public async Task<UserDTO?> AddUser(UserCreateRequest user)
    {
        // add check email later
        user.Password = SecurityUtil.Hash(user.Password);
        var userEntityToAdd = _mapper.Map<UserEntity>(user);
        var userEntity = await _userRepository.AddAsync(userEntityToAdd);
        var addResult = await _uow.CommitAsync();
        if (addResult <= 0) return null;
        var result = _mapper.Map<UserDTO>(userEntity);
        return result;
    }

    public async Task<UserDTO?> UpdateUser(UserUpdateRequest user)
    {
        var userEntityToUpdate = await _userRepository.GetByIdAsync(user.Id);
        if (userEntityToUpdate is null)
        {
            throw new KeyNotFoundException();
        }
        userEntityToUpdate.HashedPassword = SecurityUtil.Hash(user.Password);
        var userEntity = _userRepository.Update(userEntityToUpdate);
        var updateResult = await _uow.CommitAsync();
        if (updateResult <= 0) return null;
        var result = _mapper.Map<UserDTO>(userEntity);
        return result;
    }

    public async Task<UserDTO?> DeactivateUser(Guid id)
    {
        var userEntityToUpdate = await _userRepository.GetByIdAsync(id);
        if (userEntityToUpdate is null) return null;
        userEntityToUpdate.Active = false;
        var userEntity = _userRepository.Update(userEntityToUpdate);
        var updateResult = await _uow.CommitAsync();
        if (updateResult <= 0) return null;
        var result = _mapper.Map<UserDTO>(userEntity);
        return result;
    }
}