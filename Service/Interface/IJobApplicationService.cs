using DataAccessLayer.Enum;
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
        Task<IEnumerable<JobApplicationResponseDto>> GetAllAsync();
        Task ApplyForJobAsync(JobApplicationDto applicationDto);

        Task<bool> UpdateStatusAsync(Guid Id, AppStatus appStatus);
        Task<JobApplicationResponseDto> GetByIdAsync(Guid Id);
        Task<bool> UpdateAsync(Guid Id, JobApplicationDto jobapplicationDto);
    }

}
