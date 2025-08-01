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
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> _CourseRepository;
        private readonly IMapper _mapper;

        public CourseService(IRepository<Course> repository, IMapper mapper)
        {
            _CourseRepository = repository;
            _mapper = mapper;

        }
        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {

            var course = await _CourseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CourseDto>>(course);
        }
    }
}
