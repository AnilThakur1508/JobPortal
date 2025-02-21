using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;
using DTO;

namespace Service.Interface
{
    public interface IEmployeService
    {
        Task<IEnumerable<EmployeDto>> GetAllAsync();
        Task<EmployeDto> AddAsync(EmployeDto employeDto);
    }
}
