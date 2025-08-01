using AutoMapper;
using DataAccessLayer.Entity;
using DataAccessLayer.PortalRepository;
using DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _repository;

        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<Address> _addressService;


        public EmployeeService(
            UserManager<AppUser> userManager,
            IRepository<Employee> repository,
            IRepository<Address> addressService,

             IMapper mapper)


        {
            _repository = repository;
            _mapper = mapper;
            _addressService = addressService;
            _userManager = userManager;

        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> GetByIdAsync(Guid id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found");
            }
            return _mapper.Map<EmployeeDto>(employee);
        }
        public async Task<bool> UpsertAsync(EmployeeDto employeeDto)
        {

            var existingEmployee = await _repository.FirstOrDefaultAsync(e => e.UserId == employeeDto.UserId);
            var existingAddress = await _addressService.FirstOrDefaultAsync(e => e.UserId == employeeDto.UserId);
           



            if (existingEmployee != null)
            {

                employeeDto.Id = existingEmployee.Id;


                _mapper.Map(employeeDto, existingEmployee);


                await _repository.UpdateAsync(existingEmployee);
                if (employeeDto.Address != null && existingAddress != null)
                {
                    employeeDto.Address.UserId = existingEmployee.UserId;
                    _mapper.Map(employeeDto.Address, existingAddress);
                    await _addressService.UpdateAsync(existingAddress);
                    return true;
                }
                return false;
            }
            else
            {
                var employee = _mapper.Map<Employee>(employeeDto);




                if (await _repository.AddAsync(employee))
                {
                    if (employeeDto.Address != null)
                    {
                        employeeDto.Address.UserId = employee.UserId;
                        await _addressService.AddAsync(_mapper.Map<Address>(employeeDto.Address));
                    }
                    return true;
                }
                return false;
            }
        }

        public async Task<EmployeeDto> GetByUserIdAsync(Guid id)
        {
            var employee = await _repository.FirstOrDefaultAsync(e => e.UserId == id);
            var address = await _addressService.FirstOrDefaultAsync(x => x.UserId == id);

            if (employee == null)
            {
                throw new KeyNotFoundException("Employee not found.");
            }
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            employeeDto.Address = _mapper.Map<AddressDto>(address);
            return employeeDto;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

    }
}

