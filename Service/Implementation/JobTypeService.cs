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
    public class JobTypeService:IJobTypeService
    {
        private readonly IRepository<JobType> _JobTypeRepository;
        private readonly IMapper _mapper;

        public JobTypeService(IRepository<JobType> repository, IMapper mapper)
        {
            _JobTypeRepository = repository;
            _mapper = mapper;

        }
        public async Task<IEnumerable<JobTypeDto>> GetAllAsync()
        {

            var jobtypes = await _JobTypeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<JobTypeDto>>(jobtypes);
        }
    }
}
