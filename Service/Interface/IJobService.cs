using DataAccessLayer.Entity;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IJobService
    {
        Task<IEnumerable<JobResponseDto>> GetAllAsync();
        Task<JobDto> GetByIdAsync(Guid id);
        Task<bool> AddAsync(JobDto jobDto);
        Task<bool> UpdateAsync(Guid id, JobDto jobDto);
        Task<bool> DeleteAsync(Guid id);
        Task<List<CategoryCountDto>> GetJobCountsByCategoryAsync();
        Task<IEnumerable<JobDto>> GetFeaturedJobsAsync();
        Task<(List<JobDto> Jobs, int TotalCount)> GetJobsAsync(JobFilterDto filters, int pageNumber, int pageSize);
        Task<JobDetailDto> GetJobDetailByIdAsync(Guid id);
        Task<List<Job>> SearchJobsAsync(string keyword);

    }
}
