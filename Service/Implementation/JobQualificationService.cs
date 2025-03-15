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
    public class JobQualificationService:IJobQualificationService
    {
        private readonly IRepository<JobQualification> _JobQualificationRepository;
        private readonly IMapper _mapper;

        public JobQualificationService(IRepository<JobQualification> repository, IMapper mapper)
        {
            _JobQualificationRepository = repository;
            _mapper = mapper;

        }
        public async Task<IEnumerable<JobQualificationDto>> GetAllAsync()
        {

            var jobQualifications = await _JobQualificationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<JobQualificationDto>>(jobQualifications);
        }
    }

}

