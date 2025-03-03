using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;
using DTO;
using Microsoft.AspNetCore.Http;

namespace Service.Interface
{
    public interface IQualificationService
    {
        Task<IEnumerable<QualificationDto>> GetAllAsync();
        Task<QualificationDto> GetByIdAsync(Guid id);
        Task<bool> AddAsync(QualificationDto QualificationDto);
        Task<bool> UpdateAsync(Guid id, QualificationDto qualificationDto);

        Task<bool> DeleteAsync(Guid id);

        




    }
}
