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
            CreateMap<EmployeeDto, Employee>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
            CreateMap<WorkExperienceDto, WorkExperience>().ReverseMap();
            CreateMap<EmployerDto, Employer>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
            CreateMap<CountryDto, Country>().ReverseMap();
            CreateMap<StateDto, State>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<JobDto,Job>().ForMember(dest=>dest.EmployerId,opt=>opt.MapFrom(src=>src.EmployerId)).ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<SkillsDto, Skill>().ReverseMap();
            CreateMap<JobSkillDto, JobSkill>().ReverseMap();
            CreateMap<JobApplicationDto, JobApplication>().ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId)).ReverseMap();
            CreateMap<ResumeDto, Resume>().ReverseMap(); 
            CreateMap<JobCourseDto, JobCourse>().ReverseMap();
            CreateMap<JobTypeDto, JobType>().ReverseMap();
            CreateMap<ExperienceLevelDto, ExperienceLevel>().ReverseMap();
            CreateMap<CourseDto, Course>().ReverseMap();
            CreateMap<Job, JobDto>().ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Employer.CompanyName)).ReverseMap()
           .ForMember(dest => dest.Employer, opt => opt.Ignore()); 
            CreateMap<Job, JobResponseDto>().ForMember(dest => dest.ExperienceLevelName, opt => opt.MapFrom(src => src.ExperienceLevel.Name)).ForMember(dest => dest.JobTypeName, opt => opt.MapFrom(src => src.JobType.Name)).ReverseMap();
            CreateMap<JobApplication, JobApplicationResponseDto>().ReverseMap();


        }
    }
    
}
