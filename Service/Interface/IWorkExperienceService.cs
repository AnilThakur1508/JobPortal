using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;
using DTO;

namespace Service.Interface
{
    public interface IWorkExperienceService
    {
        Task<IEnumerable<WorkExperienceDto>> GetAllAsync();
        Task<WorkExperienceDto> GetByIdAsync(Guid id);
        Task<WorkExperienceDto> AddAsync(WorkExperienceDto workExperienceDto);
    }
}
