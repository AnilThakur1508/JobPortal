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
    public class ExperienceLevelService:IExperienceLevelService
    {
        private readonly IRepository<ExperienceLevel> _ExperienceLevelRepository;
        private readonly IMapper _mapper;

        public ExperienceLevelService(IRepository<ExperienceLevel> repository, IMapper mapper)
        {
            _ExperienceLevelRepository = repository;
            _mapper = mapper;

        }
        public async Task<IEnumerable<ExperienceLevelDto>> GetAllAsync()
        {

            var experienceLevels = await _ExperienceLevelRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ExperienceLevelDto>>(experienceLevels);
        }
    }
}
