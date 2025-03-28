using AutoMapper;
using DataAccessLayer.Entity;
using DataAccessLayer.PortalRepository;
using DataAccessLayer.Repository;
using DTO;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRepository<Employer> _repository;
        private readonly IRepository<AppUser> _userRepository;

        private readonly IMapper _mapper;


        public JobService(IRepository<Job> jobRepository, IMapper mapper, IRepository<Employer> repository,IRepository<AppUser> userrepository)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
            _repository=repository;
            _userRepository=userrepository;

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
        ////Add
        //public async Task<bool> AddAsync(JobDto jobDto)
        //{
        //    var employer = await _userRepository.FirstOrDefaultAsync(u => u.Id == jobDto.EmployerId.ToString());

        //    if (employer == null)
        //    {
        //        throw new Exception("Employer not found.");
        //    }

        //    jobDto.EmployerId =Guid.Parse(employer.Id); // Assign Employer's ID to JobDto
        //    var job = _mapper.Map<Job>(jobDto); // Map DTO to Entity

        //    return await _jobRepository.AddAsync(job);
        //}


        public async Task<bool> AddAsync(JobDto jobDto)
        {
            // First find the employer by user ID
            var employer = await _repository.FirstOrDefaultAsync(e => e.UserId == jobDto.EmployerId);

            if (employer == null)
            {
                throw new Exception("Employer record not found for this user.");
            }

            // Now use the employer's ID (not user ID) for the job
            jobDto.EmployerId = employer.Id;
            var job = _mapper.Map<Job>(jobDto);

            return await _jobRepository.AddAsync(job);
        }
        public async Task<bool> UpdateAsync(Guid id, JobDto jobDto)
        {
            var existingJob = await _jobRepository.GetByIdAsync(id);
            if (existingJob == null)
            {
                throw new KeyNotFoundException("Job not found.");
            }

            // Debugging log to check what EmployerId is being received
            Console.WriteLine($"Received EmployerId: {jobDto.EmployerId}");

            if (jobDto.EmployerId == Guid.Empty || jobDto.EmployerId == null)
            {
                throw new ArgumentException("Invalid Employer ID.");
            }

            var employerExists = await _repository.AnyAsync(e => e.Id == jobDto.EmployerId);
            if (!employerExists)
            {
                throw new KeyNotFoundException($"Employer with ID {jobDto.EmployerId} does not exist.");
            }

            _mapper.Map(jobDto, existingJob);
            return await _jobRepository.UpdateAsync(existingJob);
        }




        //Delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _jobRepository.DeleteAsync(id);

        }

       
    }
}
