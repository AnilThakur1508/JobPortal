using AutoMapper;
using DataAccessLayer.Entity;
using DataAccessLayer.PortalRepository;
using DataAccessLayer.Repository;
using DTO;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class JobService : IJobService
    {
        private readonly IRepository<Job> _jobRepository;
        private readonly IMapper _mapper;


        public JobService(IRepository<Job> jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;

        }
        //GetAll
        public async Task<IEnumerable<JobDto>> GetAllAsync()
        {
            var job = await _jobRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<JobDto>>(job);
        }
        //GetById

        public async Task<JobDto?> GetByIdAsync(Guid id)
        {
            var job = await _jobRepository.GetByIdAsync(id);

            return job == null ? null : _mapper.Map<JobDto>(job);
        }
        //Add
        public async Task<bool> AddAsync(JobDto jobDto)
        {
            var job = _mapper.Map<Job>(jobDto);
            
            return await _jobRepository.AddAsync(job);
        }
        // Update
        public async Task<bool> UpdateAsync(Guid id, JobDto jobDto)
        {
            var job = _mapper.Map<Job>(jobDto);
            job.Id = id;
            return await _jobRepository.UpdateAsync(job);
        }
        //Delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _jobRepository.DeleteAsync(id);

        }


    }
}
