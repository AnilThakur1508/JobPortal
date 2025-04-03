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
    public class JobCourseService:IJobCourseService
    {
        private readonly IRepository<JobCourse> _JobCourseRepository;
        private readonly IMapper _mapper;

        public JobCourseService(IRepository<JobCourse> repository, IMapper mapper)
        {
            _JobCourseRepository = repository;
            _mapper = mapper;

        }
        public async Task<IEnumerable<JobCourseDto>> GetAllAsync()
        {

            var jobcourses = await _JobCourseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<JobCourseDto>>(jobcourses);
        }
        public async Task<JobCourseDto> AddAsync(JobCourseDto jobcourseDto)
        {
            var jobcourse = _mapper.Map<JobCourse>(jobcourseDto);
            await _JobCourseRepository.AddAsync(jobcourse);
            return jobcourseDto;
        }
    }

}

