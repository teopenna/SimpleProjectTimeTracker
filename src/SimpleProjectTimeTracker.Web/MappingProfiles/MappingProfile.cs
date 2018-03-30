using AutoMapper;
using SimpleProjectTimeTracker.Web.Models;

namespace SimpleProjectTimeTracker.Web.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TimeRegistrationEntity, TimeRegistration>()
                .ForMember(dest => dest.ProjectName, m => m.MapFrom(src => src.Project.Name))
                .ForMember(dest => dest.CustomerName, m => m.MapFrom(src => src.Project.CustomerName))
                .ReverseMap();
        }
    }
}
