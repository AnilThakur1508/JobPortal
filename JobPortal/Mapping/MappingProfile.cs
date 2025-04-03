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
            CreateMap<QualificationDto, EmployeeQualification>().ReverseMap();
            CreateMap<EmployeeDto, Employee>().ReverseMap();
            CreateMap<WorkExperienceDto, WorkExperience>().ReverseMap();
            CreateMap<EmployerDto, Employer>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore()) // ✅ Ignore Id on updates
    .ReverseMap();
            CreateMap<CountryDto, Country>().ReverseMap();
            CreateMap<StateDto, State>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<JobDto,Job>()
                .ForMember(dest=>dest.EmployerId,opt=>opt.MapFrom(src=>src.EmployerId))
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                   
                .ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<SkillsDto, Skill>().ReverseMap();
            CreateMap<JobSkillDto, JobSkill>().ReverseMap();
            CreateMap<JobApplicationDto, JobApplication>().ReverseMap();
            CreateMap<ResumeDto, Resume>().ReverseMap(); 
            CreateMap<JobCourseDto, JobCourse>().ReverseMap();
            CreateMap<CourseDto, Course>().ReverseMap();

           

        }
    }
}
