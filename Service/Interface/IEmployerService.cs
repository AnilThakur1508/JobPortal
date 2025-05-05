using DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IEmployerService
    {
        Task<IEnumerable<EmployerDto>> GetAllAsync();
        Task<EmployerDto> GetByIdAsync(Guid id);
        Task<EmployerDto> GetByUserIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpsertAsync(EmployerDto employerDto);
       
    }
}
