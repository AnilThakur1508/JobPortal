using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;
using DTO;

namespace Service.Interface
{
    public interface IQualificationService
    {
        Task<IEnumerable<QualificationDto>> GetAllAsync();
        Task<QualificationDto> GetByIdAsync(Guid id);
        Task<QualificationDto> AddAsync(QualificationDto QualificationDto); 
    }
}
