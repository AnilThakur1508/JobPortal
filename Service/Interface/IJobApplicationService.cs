using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IJobApplicationService
    {
        Task<IEnumerable<JobApplicationDto>> GetAllAsync();
        Task<JobApplicationDto> GetByIdAsync(Guid id);
        Task<bool> AddAsync(JobApplicationDto jobapplicationDto);
        Task<bool> UpdateAsync(Guid id, JobApplicationDto jobapplicationDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
