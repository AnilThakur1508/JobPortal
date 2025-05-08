using AutoMapper;
using DataAccessLayer.Data;
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
        private readonly IRepository<JobCourse> _jobcourseRepository;
        private readonly IRepository<JobSkill> _jobskillRepository;
        private readonly IRepository<Skill> _skillRepositry;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public JobService(IRepository<Job> jobRepository, IMapper mapper, IRepository<Employer> repository, IRepository<AppUser> userrepository,
            IRepository<JobCourse> jobcourseRepository, IRepository<JobSkill> jobskillRepository, IRepository<Skill> skillRepositry, IRepository<Category> categoryRepository, IRepository<Address> addressRepository, AppDbContext context)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
            _repository = repository;
            _userRepository = userrepository;
            _jobcourseRepository = jobcourseRepository;
            _jobskillRepository = jobskillRepository;
            _skillRepositry = skillRepositry;
            _categoryRepository = categoryRepository;
            _addressRepository = addressRepository;
            _context = context;
        }
        public async Task<IEnumerable<JobResponseDto>> GetAllAsync()
        {
            var jobs = await _context.Jobs
                .Include(j => j.ExperienceLevel)
                .Include(j => j.JobType)
                .ToListAsync();

            return _mapper.Map<IEnumerable<JobResponseDto>>(jobs);
        }
        public async Task<JobDto?> GetByIdAsync(Guid id)
        {
           
            var job = await _jobRepository.GetByIdAsync(id);
            if (job == null)
                return null;

           
            var jobCourses = await _jobcourseRepository.GetListAsync(jc => jc.JobId == id);
            var jobSkills = await _jobskillRepository.GetListAsync(js => js.JobId == id);

            
            var skillIds = jobSkills.Select(js => js.SkillId).ToList();

            
            var skills = await _skillRepositry.GetListAsync(s => skillIds.Contains(s.Id));


            var categoryId = skills.Select(s => s.CategoryId).FirstOrDefault();

            
            var courseIdsString = string.Join(",", jobCourses.Select(jc => jc.CourseId));
            var skillIdsString = string.Join(",", skillIds);

            
            var jobDto = _mapper.Map<JobDto>(job);
            jobDto.CourseIds = courseIdsString;
            jobDto.SkillIds = skillIdsString;

            return jobDto;
        } 
        public async Task<bool> AddAsync(JobDto jobDto)
        {
            var employer = await _repository.FirstOrDefaultAsync(e => e.UserId == jobDto.EmployerId);
            if (employer == null)
            {
                throw new Exception("Employer record not found for this user.");
            }

            
            jobDto.EmployerId = employer.Id;

            var job = _mapper.Map<Job>(jobDto);

           
            var jobAdded = await _jobRepository.AddAsync(job);
            if (!jobAdded) return false;

            await _jobRepository.SaveChangesAsync(); 

           
            if (!string.IsNullOrEmpty(jobDto.CourseIds))
            {
                var jobCourses = jobDto.CourseIds.Split(',')
                                    .Select(id => new JobCourse
                                    {
                                        Id = Guid.NewGuid(),
                                        JobId = job.Id,
                                        CourseId = Guid.Parse(id.Trim())
                                    }).ToList();

                await _jobcourseRepository.AddRangeAsync(jobCourses);
                await _jobcourseRepository.SaveChangesAsync(); 
            }
            
            if (!string.IsNullOrEmpty(jobDto.SkillIds))
            {
                var jobSkills = jobDto.SkillIds.Split(',')
                                   .Select(id => new JobSkill
                                   {
                                       Id = Guid.NewGuid(),
                                       JobId = job.Id,
                                       SkillId = Guid.Parse(id.Trim())
                                   }).ToList();

                await _jobskillRepository.AddRangeAsync(jobSkills);
                await _jobskillRepository.SaveChangesAsync(); 
            }

            return true;
        }
        public async Task<bool> UpdateAsync(Guid id, JobDto jobDto)
        {
            var existingJob = await _jobRepository.GetByIdAsync(id);
            if (existingJob == null)
            {
                throw new KeyNotFoundException("Job not found.");
            }

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
            var isUpdated = await _jobRepository.UpdateAsync(existingJob);
            await _jobRepository.SaveChangesAsync(); 

           
            var existingJobCourses = await _jobcourseRepository.GetListAsync(jc => jc.JobId == id);
            var existingCourseIds = existingJobCourses.Select(jc => jc.CourseId).ToList();
            var newCourseIds = jobDto.CourseIdList ?? new List<Guid>();

            var coursesToAdd = newCourseIds.Except(existingCourseIds).ToList();
            var coursesToRemove = existingCourseIds.Except(newCourseIds).ToList();

            if (coursesToRemove.Any())
            {
                var jobCoursesToRemove = existingJobCourses.Where(jc => coursesToRemove.Contains(jc.CourseId)).ToList();
                await _jobcourseRepository.RemoveRangeAsync(jobCoursesToRemove);
            }

            var jobCoursesToAdd = coursesToAdd.Select(courseId => new JobCourse
            {
                Id = Guid.NewGuid(),
                JobId = id,
                CourseId = courseId
            }).ToList();

            if (jobCoursesToAdd.Any())
            {
                await _jobcourseRepository.AddRangeAsync(jobCoursesToAdd);
            }

            await _jobcourseRepository.SaveChangesAsync(); 

            
            var existingJobSkills = await _jobskillRepository.GetListAsync(js => js.JobId == id);
            var existingSkillIds = existingJobSkills.Select(js => js.SkillId).ToList();
            var newSkillIds = jobDto.SkillIdList ?? new List<Guid>();

            var skillsToAdd = newSkillIds.Except(existingSkillIds).ToList();
            var skillsToRemove = existingSkillIds.Except(newSkillIds).ToList();

            if (skillsToRemove.Any())
            {
                var jobSkillsToRemove = existingJobSkills.Where(js => skillsToRemove.Contains(js.SkillId)).ToList();
                await _jobskillRepository.RemoveRangeAsync(jobSkillsToRemove);
            }

            var jobSkillsToAdd = skillsToAdd.Select(skillId => new JobSkill
            {
                Id = Guid.NewGuid(),
                JobId = id,
                SkillId = skillId
            }).ToList();

            if (jobSkillsToAdd.Any())
            {
                await _jobskillRepository.AddRangeAsync(jobSkillsToAdd);
            }

            await _jobskillRepository.SaveChangesAsync(); 

            return isUpdated;
        }


       

        public async Task<bool> DeleteAsync(Guid id)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            if (job == null)
            {
                throw new KeyNotFoundException("Job not found.");
            }
            var jobCourses = await _jobcourseRepository.GetListAsync(jc => jc.JobId == id);
            if (jobCourses.Any())
            {
                await _jobcourseRepository.RemoveRangeAsync(jobCourses);
            }
            var jobSkills = await _jobskillRepository.GetListAsync(js => js.JobId == id);
            if (jobSkills.Any())
            {
                await _jobskillRepository.RemoveRangeAsync(jobSkills);
            }
            var isDeleted = await _jobRepository.DeleteAsync(id);
            await _jobcourseRepository.SaveChangesAsync();
            await _jobskillRepository.SaveChangesAsync();
            await _jobRepository.SaveChangesAsync();
            return isDeleted;
        }
        public async Task<List<CategoryCountDto>> GetJobCountsByCategoryAsync()
        {
             var jobs = await _jobRepository.GetAllAsync();
            var jobDto = _mapper.Map<List<JobDto>>(jobs);
            var categories = await _categoryRepository.GetAllAsync();
            var categoryDict = categories.ToDictionary(c => c.Id, c => new { c.Name, c.Icon });
            var categoryCounts = jobDto
                .GroupBy(j => j.CategoryId)
                .Select(g => new CategoryCountDto
                {
                    CategoryId = g.Key,
                    CategoryName = categoryDict.ContainsKey(g.Key) ? categoryDict[g.Key].Name : "Unknown",
                    CategoryIcon = categoryDict.ContainsKey(g.Key) ? categoryDict[g.Key].Icon : string.Empty,
                    CategoryCount = g.Count()
                })
                .ToList();
             return categoryCounts;
        }
        public async Task<IEnumerable<JobDto>> GetFeaturedJobsAsync()
        {
            var featuredJobs = await _jobRepository.GetQueryable()
                .Include(x => x.Employer)
                
                .ToListAsync();

            var employerUserIds = featuredJobs
                .Select(j => j.Employer.UserId)
                .Distinct()
                .ToList();

           
            var addresses = await _addressRepository.GetQueryable()

                .Where(a => employerUserIds.Contains(a.UserId))
                .ToListAsync();

            var addressDict = addresses.ToDictionary(a => a.UserId, a => a);

            var jobDtos = _mapper.Map<List<JobDto>>(featuredJobs);

            foreach (var jobDto in jobDtos)
            {
                var job = featuredJobs.FirstOrDefault(j => j.Id == jobDto.Id);
                if (job != null && addressDict.TryGetValue(job.Employer.UserId, out var address))
                {
                    jobDto.Address = _mapper.Map<AddressDto>(address);
                }
            }

            return jobDtos;
        }
        public async Task<(List<JobDto> Jobs, int TotalCount)> GetJobsAsync(JobFilterDto filters, int pageNumber, int pageSize)
        {
            var baseQuery = _jobRepository.GetQueryable();

            if (filters != null)
            {
                if (filters.CategoryId.HasValue)
                    baseQuery = baseQuery.Where(j => j.CategoryId == filters.CategoryId.Value);

                if (filters.JobTypeId.HasValue)
                    baseQuery = baseQuery.Where(j => j.JobTypeId == filters.JobTypeId.Value);

                if (filters.ExperienceLevelId.HasValue)
                    baseQuery = baseQuery.Where(j => j.ExperienceLevelId == filters.ExperienceLevelId.Value);

            }
           var query = baseQuery.Include(j => j.Employer);
           var totalCount = await query.CountAsync();
           var pagedJobs = await query
                .OrderByDescending(j => j.PublishDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
          var employerUserIds = pagedJobs
                .Select(j => j.Employer.UserId)
                .Distinct()
                .ToList();
          var addresses = await _addressRepository.GetQueryable()
                .Where(a => employerUserIds.Contains(a.UserId))
                .ToListAsync();
         var addressDict = addresses.ToDictionary(a => a.UserId, a => a);
         var jobDtos = _mapper.Map<List<JobDto>>(pagedJobs);

            foreach (var jobDto in jobDtos)
            {
                var job = pagedJobs.FirstOrDefault(j => j.Id == jobDto.Id);
                if (job != null && addressDict.TryGetValue(job.Employer.UserId, out var address))
                {
                    jobDto.Address = _mapper.Map<AddressDto>(address);
                }
            }

            return (jobDtos, totalCount);
        }
        public async Task<JobDetailDto> GetJobDetailByIdAsync(Guid id)
        {
            var job = await _jobRepository.GetQueryable()
                .Include(j => j.Employer)
                .Include(j => j.JobType)
                .FirstOrDefaultAsync(j => j.Id == id);
               if (job == null)
                return null;
            var jobDetailDto = new JobDetailDto
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                Salary = job.Salary,
                JobTypeName = job.JobType?.Name,
                CompanyName = job.Employer?.CompanyName,
                PublishDate = job.PublishDate,
                ExpiryDate = job.ExpiryDate,
                
            };
            var address = await _addressRepository.FirstOrDefaultAsync(a => a.UserId == job.Employer.UserId);
            if (address != null)
            {
                jobDetailDto.Address = _mapper.Map<AddressDto>(address);
            }
            return jobDetailDto;
        }
        public async Task<List<Job>> SearchJobsAsync(string keyword)
        {
                 return await _context.Jobs
                .Where(j => j.Title.Contains(keyword))
                .ToListAsync();
        }











    }
}








