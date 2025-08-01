using AutoMapper;
using DataAccessLayer.Entity;
using DataAccessLayer.PortalRepository;
using DTO;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class JobSkillService : IJobSkillSevice
    {
        private readonly IRepository<JobSkill> _JobSkillRepository;
        private readonly IMapper _mapper;

        public JobSkillService(IRepository<JobSkill> repository, IMapper mapper)
        {
            _JobSkillRepository = repository;
            _mapper = mapper;

        }
        public async Task<IEnumerable<JobSkillDto>> GetAllAsync()
        {

            var jobskills = await _JobSkillRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<JobSkillDto>>(jobskills);
        }
        public async Task<JobSkillDto> AddAsync(JobSkillDto jobskillDto)
        {
            var jobskill = _mapper.Map<JobSkill>(jobskillDto);
            await _JobSkillRepository.AddAsync(jobskill);
            return jobskillDto;
        }
    }
}
