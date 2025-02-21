using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Entity;
using DataAccessLayer.PortalRepository;
using DTO;
using Service.Interface;

namespace Service.Implementation
{
    public class WorkExperienceService : IWorkExperienceService
    {
        private readonly IRepository<WorkExperience> _workExperienceRepository;
        private readonly IMapper _mapper;

        public WorkExperienceService(IRepository<WorkExperience> workExperienceRepository, IMapper mapper)
        {
            _workExperienceRepository = workExperienceRepository;
            _mapper = mapper;
        }
        //GetAll
        public async Task<IEnumerable<WorkExperienceDto>> GetAllAsync()
        {
            var experiences = await _workExperienceRepository.GetAllAsync();
             var experiencesDto =_mapper.Map<IEnumerable<WorkExperienceDto>>(experiences);
            return experiencesDto;
        }
        //Add
        public async Task<WorkExperienceDto> AddAsync(WorkExperienceDto workExperienceDto)
        {
            var experience =  _mapper. Map<WorkExperience>(workExperienceDto);
            await _workExperienceRepository.AddAsync(experience);
            return workExperienceDto;
        }
    }

}
