using AutoMapper;
using NodaTime;
using TaskMaster.Controllers.Payloads.Requests;
using TaskMaster.Helpers;
using TaskMaster.Models.Dtos;
using TaskMaster.Models.Entities;

namespace TaskMaster.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserEntity, UserDto>()
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToDateOnly()));
        CreateMap<UserDto, UserEntity>()
            .ForMember(dest => dest.CreationDate, 
                opt => opt.MapFrom(src => LocalDate.FromDateOnly(src.CreationDate)));
        CreateMap<UserCreateRequest, UserEntity>()
            .ForMember(u => u.HashedPassword, options =>
                options.MapFrom(src => src.Password));
        CreateMap<UserUpdateRequest, UserEntity>();
    }
}