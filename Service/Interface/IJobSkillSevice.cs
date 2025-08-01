using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
   public interface IJobSkillSevice
    {
        Task<IEnumerable<JobSkillDto>> GetAllAsync();
        Task<JobSkillDto> AddAsync(JobSkillDto jobskillDto);
    }
}
