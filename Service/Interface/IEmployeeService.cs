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
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> GetByIdAsync(Guid id);
        //Task<bool>AddAsync(EmployeeDto employerDto);
        Task<bool> UpsertAsync(EmployeeDto employeeDto);
        Task<bool> DeleteAsync(Guid id);

       


       // Task<string>AddFileAsync(IFormFile file, string subFolder);
    }
}
