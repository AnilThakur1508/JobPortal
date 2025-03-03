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
        Task<IEnumerable<JobDto>> GetAllAsync();
        Task<JobDto> GetByIdAsync(Guid id);
        Task<bool> AddAsync(JobDto jobDto);
        Task<bool> UpdateAsync(Guid id, JobDto jobDto);
        Task<bool> DeleteAsync(Guid id);

    }
}
