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
    public class SkillService : ISkillsService
    {
        private readonly IRepository<Skills> _SkillRepository;
        private readonly IMapper _mapper;

        public SkillService(IRepository<Skills> repository, IMapper mapper)
        {
            _SkillRepository = repository;
            _mapper = mapper;

        }
        public async Task<IEnumerable<SkillsDto>> GetAllAsync()
        {

            var skills = await _SkillRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SkillsDto>>(skills);
        }
    }
}    
