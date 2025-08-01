using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public  interface IJobCourseService
    {
        Task<IEnumerable<JobCourseDto>> GetAllAsync();
        Task<JobCourseDto> AddAsync(JobCourseDto jobcourseDto);
    }
}
