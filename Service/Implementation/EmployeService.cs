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
    public class EmployeService : IEmployeService
    {
        private readonly IRepository<Employe> _employeRepository;
        private readonly IMapper _mapper;

        public EmployeService(IRepository<Employe> repository, IMapper mapper)
        {
            _employeRepository = repository;
            _mapper = mapper;

        }
        public async Task<IEnumerable<EmployeDto>> GetAllAsync()
        {

            var employes = await _employeRepository.GetAllAsync();
           var employesDto = _mapper.Map<IEnumerable<EmployeDto>>(employes);
            return employesDto;

        }
        public async Task<EmployeDto> AddAsync(EmployeDto employeDto)
        {
            var employes = _mapper.Map<Employe>(employeDto);
            await _employeRepository.AddAsync(employes);
            return employeDto;
        }
    }
}
