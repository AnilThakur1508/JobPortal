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
    public class JobApplicationService:IJobApplicationService
    {
        
        
            private readonly IRepository<JobApplication> _jobapplicationRepository;
            private readonly IMapper _mapper;


            public JobApplicationService(IRepository<JobApplication> jobapplicationRepository, IMapper mapper)
            {
                _jobapplicationRepository = jobapplicationRepository;
                _mapper = mapper;

            }
            //GetAll
            public async Task<IEnumerable<JobApplicationDto>> GetAllAsync()
            {
                var jobapplication = await _jobapplicationRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<JobApplicationDto>>(jobapplication);
            }
            //GetById

            public async Task<JobApplicationDto?> GetByIdAsync(Guid id)
            {
                var jobapplication = await _jobapplicationRepository.GetByIdAsync(id);

                return jobapplication == null ? null : _mapper.Map<JobApplicationDto>(jobapplication);
            }
            //Add
            public async Task<bool> AddAsync(JobApplicationDto jobapplicationDto)
            {
                var jobapplication = _mapper.Map<JobApplication>(jobapplicationDto);

                return await _jobapplicationRepository.AddAsync(jobapplication);
            }
            // Update
            public async Task<bool> UpdateAsync(Guid id, JobApplicationDto jobapplicationDto)
            {
                var jobapplication = _mapper.Map<JobApplication>(jobapplicationDto);
                jobapplication.Id = id;
                return await _jobapplicationRepository.UpdateAsync(jobapplication);
            }
            //Delete
            public async Task<bool> DeleteAsync(Guid id)
            {
                return await _jobapplicationRepository.DeleteAsync(id);

            }


        
    }
}
