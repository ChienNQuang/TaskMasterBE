using AutoMapper;
using NodaTime;
using TaskMaster.Controllers.Payloads.Requests;
using TaskMaster.Models.Dtos;
using TaskMaster.Models.Entities;

namespace TaskMaster.MappingProfiles;

public class ProjectMappingProfile : Profile
{
    public ProjectMappingProfile()
    {
        CreateMap<ProjectEntity, ProjectDto>()
            .ForMember(dest => dest.StartDate,
                opt => opt.MapFrom(src => src.StartDate.ToDateTimeUnspecified()))
            .ForMember(dest => dest.EndDate,
                opt => opt.MapFrom(src => src.EndDate.ToDateTimeUnspecified()))
            .ForMember(dest => dest.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToDateOnly()))
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<ProjectCreateRequest, ProjectEntity>()
            .ForMember(dest => dest.StartDate,
                opt => opt.MapFrom(src => LocalDateTime.FromDateTime(src.StartDate)))
            .ForMember(dest => dest.EndDate,
                opt => opt.MapFrom(src => LocalDateTime.FromDateTime(src.EndDate)));
        
    }
}