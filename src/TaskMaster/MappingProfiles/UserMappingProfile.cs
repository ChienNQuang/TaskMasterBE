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
        CreateMap<UserCreateRequest, UserEntity>()
            .ForMember(u => u.HashedPassword, 
                opt => opt.MapFrom(src => src.Password))
            .ForMember(u => u.Active, 
                opt => opt.MapFrom(src => true))
            .ForMember(u => u.CreationDate, 
                opt => opt.MapFrom(src => SystemClock.Instance.GetCurrentInstant().InUtc().Date))
            .ForMember(u => u.Id, 
                opt => opt.MapFrom(src => Guid.Empty));
    }
}