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
            CreateMap<EmployeeDto, Employee>().ReverseMap();
            CreateMap<WorkExperienceDto, WorkExperience>().ReverseMap();
            CreateMap<EmployerDto, Employer>().ReverseMap();
            CreateMap<CountryDto, Country>().ReverseMap();
            CreateMap<StateDto, State>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
            CreateMap<JobDto,Job>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<SkillsDto, Skill>().ReverseMap();
            CreateMap<JobCategoriesDto, JobCategories>().ReverseMap();
            CreateMap<JobApplicationDto, JobApplication>().ReverseMap();
            CreateMap<ResumeDto, Resume>().ReverseMap(); 
            CreateMap<JobQualificationDto, JobQualification>().ReverseMap();

           

        }
    }
}
