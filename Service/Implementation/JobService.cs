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
        private readonly IRepository<JobCourse> _jobcourseRepository;
        private readonly IRepository<JobSkill> _jobskillRepository;
        private readonly IRepository<Skill> _skillRepositry;
        private readonly IRepository<Category> _categoryRepository;

        private readonly IMapper _mapper;
        

        public JobService(IRepository<Job> jobRepository, IMapper mapper, IRepository<Employer> repository,IRepository<AppUser> userrepository,
            IRepository<JobCourse> jobcourseRepository,IRepository<JobSkill> jobskillRepository, IRepository<Skill> skillRepositry, IRepository<Category> categoryRepository)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
            _repository = repository;
            _userRepository = userrepository;
            _jobcourseRepository = jobcourseRepository;
            _jobskillRepository = jobskillRepository;
            _skillRepositry = skillRepositry;
            _categoryRepository = categoryRepository;


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
            // Fetch job with the given ID
            var job = await _jobRepository.GetByIdAsync(id);
            if (job == null)
            {
                return null;
            }

            // Fetch JobCourse entries for this job
            var jobCourses = await _jobcourseRepository.GetListAsync(jc => jc.JobId == id);

            // Fetch JobSkill entries for this job
            var jobSkills = await _jobskillRepository.GetListAsync(js => js.JobId == id);

            // Extract Skill IDs
            var skillIds = jobSkills.Select(js => js.SkillId).ToList();


            // Fetch Skills based on SkillIds
            var skills = await _skillRepositry.GetListAsync(s => skillIds.Contains(s.Id));

            // Extract Unique CategoryIds from Skills
            var categoryIds = skills.Select(s => s.CategoryId).Distinct().ToList();

            // Convert List<Guid> to Comma-Separated String
            var courseIdsString = string.Join(",", jobCourses.Select(jc => jc.CourseId));
            var skillIdString = string.Join(",", skillIds);
            var categoryIdsString = string.Join(",", categoryIds); // Convert CategoryIds to string

            // Map job to Dto
            var jobDto = _mapper.Map<JobDto>(job);

            // Assign the comma-separated string to Dto
            jobDto.CourseIds = courseIdsString;
            jobDto.SkillIds = skillIdString;
            jobDto.CategoryIds = categoryIdsString; // Add CategoryIds to DTO

            return jobDto;
        }






        //public async Task<bool> AddAsync(JobDto jobDto)
        //{
        //    // First find the employer by user ID
        //    var employer = await _repository.FirstOrDefaultAsync(e => e.UserId == jobDto.EmployerId);

        //    if (employer == null)
        //    {
        //        throw new Exception("Employer record not found for this user.");
        //    }

        //    // Now use the employer's ID (not user ID) for the job
        //    jobDto.EmployerId = employer.Id;
        //    var job = _mapper.Map<Job>(jobDto);

        //    return await _jobRepository.AddAsync(job);
        //}
        public async Task<bool> AddAsync(JobDto jobDto)
        {
            // Find employer by UserId
            var employer = await _repository.FirstOrDefaultAsync(e => e.UserId == jobDto.EmployerId);
            if (employer == null)
            {
                throw new Exception("Employer record not found for this user.");
            }

            // Assign correct EmployerId
            jobDto.EmployerId = employer.Id;

            // Map DTO to Job entity
            var job = _mapper.Map<Job>(jobDto);

            // Save Job in database
            var jobAdded = await _jobRepository.AddAsync(job);
            if (!jobAdded) return false;

            await _jobRepository.SaveChangesAsync(); // Ensure JobId is available

            // Add JobCourse entries (if courseIds exist)
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
                await _jobcourseRepository.SaveChangesAsync(); // Persist JobCourse entries
            }
            // Add JobSkill entries (if SkillIds exist)
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
                await _jobskillRepository.SaveChangesAsync(); // Persist JobSkill entries
            }

            return true;
        }

        //public async Task<bool> UpdateAsync(Guid id, JobDto jobDto)
        //{
        //    var existingJob = await _jobRepository.GetByIdAsync(id);
        //    if (existingJob == null)
        //    {
        //        throw new KeyNotFoundException("Job not found.");
        //    }

        //    // Debugging log to check what EmployerId is being received
        //    Console.WriteLine($"Received EmployerId: {jobDto.EmployerId}");

        //    if (jobDto.EmployerId == Guid.Empty || jobDto.EmployerId == null)
        //    {
        //        throw new ArgumentException("Invalid Employer ID.");
        //    }

        //    var employerExists = await _repository.AnyAsync(e => e.Id == jobDto.EmployerId);
        //    if (!employerExists)
        //    {
        //        throw new KeyNotFoundException($"Employer with ID {jobDto.EmployerId} does not exist.");
        //    }

        //    _mapper.Map(jobDto, existingJob);
        //    return await _jobRepository.UpdateAsync(existingJob);
        //}

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

            // 🔹 Map the updated job details
            _mapper.Map(jobDto, existingJob);
            var isUpdated = await _jobRepository.UpdateAsync(existingJob);
            await _jobRepository.SaveChangesAsync(); // Save job changes before updating JobCourses and JobSkills

            // 🔹 Update JobCourses table
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

            await _jobcourseRepository.SaveChangesAsync(); // Save JobCourse changes

            // 🔹 Update JobSkills table 
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

            await _jobskillRepository.SaveChangesAsync(); // Save JobSkill changes

            return isUpdated;
        }


        ////Delete
        //public async Task<bool> DeleteAsync(Guid id)
        //{
        //    return await _jobRepository.DeleteAsync(id);

        //}

        public async Task<bool> DeleteAsync(Guid id)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            if (job == null)
            {
                throw new KeyNotFoundException("Job not found.");
            }

            // 🔹 Step 1: Fetch and delete related JobCourse records
            var jobCourses = await _jobcourseRepository.GetListAsync(jc => jc.JobId == id);
            if (jobCourses.Any())
            {
                await _jobcourseRepository.RemoveRangeAsync(jobCourses);
            }

            // 🔹 Step 2: Fetch and delete related JobSkill records
            var jobSkills = await _jobskillRepository.GetListAsync(js => js.JobId == id);
            if (jobSkills.Any())
            {
                await _jobskillRepository.RemoveRangeAsync(jobSkills);
            }

            // 🔹 Step 3: Delete the job itself
            var isDeleted = await _jobRepository.DeleteAsync(id);

            // 🔹 Step 4: Save changes to database
            await _jobcourseRepository.SaveChangesAsync();
            await _jobskillRepository.SaveChangesAsync();
            await _jobRepository.SaveChangesAsync();

            return isDeleted;
        }


    }
}
