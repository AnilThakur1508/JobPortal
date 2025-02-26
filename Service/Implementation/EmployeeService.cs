using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Entity;
using DataAccessLayer.PortalRepository;
using DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Service.Interface;

namespace Service.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IRepository<Employee> repository, IMapper mapper)
        {
            _employeRepository = repository;
            _mapper = mapper;

        }
        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {

            var employes = await _employeRepository.GetAllAsync();
           var employesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employes);
            return employesDto;

        }
        public async Task<EmployeeDto> AddAsync(EmployeeDto employeDto)
        {
            var employes = _mapper.Map<Employee>(employeDto);
            await _employeRepository.AddAsync(employes);
            return employeDto;
        }
    }
}
