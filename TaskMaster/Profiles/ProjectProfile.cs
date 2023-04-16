using AutoMapper;
using TaskMaster.Controllers.Payloads.Requests;
using TaskMaster.Models.Dtos;
using TaskMaster.Models.Entities;

namespace TaskMaster.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<ProjectEntity, ProjectDTO>()
            .ReverseMap();

        CreateMap<ProjectCreateRequest, ProjectDTO>()
            .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(p => p.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(p => p.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(p => p.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(p => p.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        
    }
}