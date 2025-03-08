using DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IResumeService
    {
        Task<IEnumerable<ResumeDto>> GetAllAsync();
        Task<ResumeDto> GetByIdAsync(Guid id);
        Task<bool> AddAsync(ResumeDto resumeDto);
        Task<bool> UpdateAsync(Guid id, ResumeDto resumeDto);
        Task<bool> DeleteAsync(Guid id);
        Task<string> AddAsync(IFormFile file);
    }
}
