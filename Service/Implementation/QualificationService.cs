using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Entity;
using DataAccessLayer.PortalRepository;
using DataAccessLayer.Repository;
using DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Service.Interface;

namespace Service.Implementation
{
    public class QualificationService : IQualificationService
    {
        private readonly IRepository<EmployeeQualification> _qualifiactionRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public QualificationService(IRepository<EmployeeQualification> repository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _qualifiactionRepository = repository;  
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;

        }
        
        public async Task<IEnumerable<QualificationDto>> GetAllAsync()
        {

            var qualification = await _qualifiactionRepository.GetAllAsync();
            var qualificationDto = _mapper.Map<IEnumerable<QualificationDto>>(qualification);
            return qualificationDto;
        }
        
        public async Task<QualificationDto> GetByIdAsync(Guid id)
        {
            var qualification = await _qualifiactionRepository.GetByIdAsync(id);
            if (qualification == null)
            {
                return null; 
            }
            return _mapper.Map<QualificationDto>(qualification);
        }
        
        public async Task<bool> AddAsync(QualificationDto qualificationDto)
        {
            var qualification = _mapper.Map<EmployeeQualification>(qualificationDto);
            return await _qualifiactionRepository.AddAsync(qualification);
        }
        
        public async Task<bool> UpdateAsync(Guid id, QualificationDto qualificationDto)
        {
            var qualification = _mapper.Map<EmployeeQualification>(qualificationDto);
            qualification.Id = id;
            return await _qualifiactionRepository.UpdateAsync(qualification);
        }
       
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _qualifiactionRepository.DeleteAsync(id);

        }
       
    }
}
