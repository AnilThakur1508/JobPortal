using DataAccessLayer.Entity;
using DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IEmployerService
    {
            Task<IEnumerable<EmployerDto>> GetAllAsync();
            Task<EmployerDto> GetByIdAsync(Guid id);
           
            Task<bool> AddAsync(EmployerDto employerDto);
            Task<bool> UpdateAsync(Guid id, EmployerDto employerDto);
            
            Task<bool> DeleteAsync(Guid id);
         
           //Task<string> AddAsync(IFormFile file, string subFolder);
           

    }
}


    