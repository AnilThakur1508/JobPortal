using AutoMapper;
using DataAccessLayer.Data;
using DataAccessLayer.Entity;
using DataAccessLayer.Enum;
using DataAccessLayer.PortalRepository;
using DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IRepository<JobApplication> _jobApplicationRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRepository<Employee> _repository;

        public JobApplicationService(
            IRepository<JobApplication> jobApplicationRepository,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            IRepository<Employee> repository)
        {
            _jobApplicationRepository = jobApplicationRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _repository = repository;
        }

        public async Task<IEnumerable<JobApplicationResponseDto>> GetAllAsync()
        {
            var jobApplications = await _jobApplicationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<JobApplicationResponseDto>>(jobApplications);
        }

        public async Task ApplyForJobAsync(JobApplicationDto dto)
        {
            var employee = await _repository.FirstOrDefaultAsync(e => e.UserId == dto.EmployeeId);
            if (employee == null)
            {
                throw new Exception("Employee record not found for this user.");
            }

            dto.EmployeeId = employee.Id;

            var resume = await SaveResume(dto.Resume);

            var application = new JobApplication
            {
                Id = Guid.NewGuid(),
                JobId = dto.JobId,
                EmployeeId = dto.EmployeeId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Experience = dto.Experience,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Qualification = dto.Qualification,
                Resume = resume,
                AppliedOn = DateTime.UtcNow,
            };

            await _jobApplicationRepository.AddAsync(application);
            await _jobApplicationRepository.SaveChangesAsync();
        }

        private async Task<string> SaveResume(IFormFile file)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var folder = Path.Combine(_webHostEnvironment.WebRootPath, "resumes");
            Directory.CreateDirectory(folder); 
            var path = Path.Combine(folder, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/resumes/{fileName}";
        }

        public async Task<bool> UpdateStatusAsync(Guid Id, AppStatus appStatus)
        {
            var jobapplication = await _jobApplicationRepository.GetByIdAsync(Id);
            if (jobapplication == null) 
                return false;

            jobapplication.Status = appStatus;
                await _jobApplicationRepository.UpdateAsync(jobapplication);
                return true;
        }
        public async Task<JobApplicationResponseDto> GetByIdAsync(Guid Id)
        {
            var jobapplication = await _jobApplicationRepository.GetByIdAsync(Id);

            return jobapplication == null ? null : _mapper.Map<JobApplicationResponseDto>(jobapplication);
        }
        
        public async Task<bool> UpdateAsync(Guid Id, JobApplicationDto jobapplicationDto)
        {
            var jobapplication = _mapper.Map<JobApplication>(jobapplicationDto);
            jobapplication.Id = Id;
            return await _jobApplicationRepository.UpdateAsync(jobapplication);
        }

    }




    //public JobApplicationService(IRepository<JobApplication> jobapplicationRepository, IMapper mapper)
    //{
    //    _jobapplicationRepository = jobapplicationRepository;
    //    _mapper = mapper;

    //}
    ////GetAll
    //public async Task<IEnumerable<JobApplicationDto>> GetAllAsync()
    //{
    //    var jobapplication = await _jobapplicationRepository.GetAllAsync();
    //    return _mapper.Map<IEnumerable<JobApplicationDto>>(jobapplication);
    //}
    ////GetById

    //public async Task<JobApplicationDto?> GetByIdAsync(Guid id)
    //{
    //    var jobapplication = await _jobapplicationRepository.GetByIdAsync(id);

    //    return jobapplication == null ? null : _mapper.Map<JobApplicationDto>(jobapplication);
    //}
    ////Add
    //public async Task<bool> AddAsync(JobApplicationDto jobapplicationDto)
    //{
    //    var jobapplication = _mapper.Map<JobApplication>(jobapplicationDto);

    //    return await _jobapplicationRepository.AddAsync(jobapplication);
    //}
    //// Update
    //public async Task<bool> UpdateAsync(Guid id, JobApplicationDto jobapplicationDto)
    //{
    //    var jobapplication = _mapper.Map<JobApplication>(jobapplicationDto);
    //    jobapplication.Id = id;
    //    return await _jobapplicationRepository.UpdateAsync(jobapplication);
    //}
    ////Delete
    //public async Task<bool> DeleteAsync(Guid id)
    //{
    //    return await _jobapplicationRepository.DeleteAsync(id);

    //}



}

