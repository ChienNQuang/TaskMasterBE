using AutoMapper;
using TaskMaster.Controllers.Payloads.Requests;
using TaskMaster.Helpers;
using TaskMaster.Models.Dtos;
using TaskMaster.Models.Entities;

namespace TaskMaster.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, UserDTO>()
            .ReverseMap();
        CreateMap<UserCreateRequest, UserEntity>()
            .ForMember(u => u.HashedPassword, options =>
                options.MapFrom(src => src.Password));
        CreateMap<UserUpdateRequest, UserEntity>();
    }
}