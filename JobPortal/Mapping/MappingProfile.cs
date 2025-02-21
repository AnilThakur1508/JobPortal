using AutoMapper;
using DataAccessLayer.Entity;
using DTO;

namespace JobPortal.Mapping
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, AppUser>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)).ReverseMap();
            CreateMap<QualificationDto, Qualification>().ReverseMap();
            CreateMap<EmployeDto, Employe>().ReverseMap();
            CreateMap<WorkExperienceDto, WorkExperience>().ReverseMap();
        }
    }
}
