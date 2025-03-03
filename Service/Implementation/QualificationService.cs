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
        private readonly IRepository<Qualification> _qualifiactionRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public QualificationService(IRepository<Qualification> repository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _qualifiactionRepository = repository;  
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;

        }
        //Get
        public async Task<IEnumerable<QualificationDto>> GetAllAsync()
        {

            var qualification = await _qualifiactionRepository.GetAllAsync();
            var qualificationDto = _mapper.Map<IEnumerable<QualificationDto>>(qualification);
            return qualificationDto;
        }
        //GetById
        public async Task<QualificationDto> GetByIdAsync(Guid id)
        {
            var qualification = await _qualifiactionRepository.GetByIdAsync(id);
            if (qualification == null)
            {
                return null; // Or throw an exception if needed
            }
            return _mapper.Map<QualificationDto>(qualification);
        }
        //Add
        public async Task<bool> AddAsync(QualificationDto qualificationDto)
        {
            var qualification = _mapper.Map<Qualification>(qualificationDto);
            return await _qualifiactionRepository.AddAsync(qualification);
        }
        //Update
        public async Task<bool> UpdateAsync(Guid id, QualificationDto qualificationDto)
        {
            var qualification = _mapper.Map<Qualification>(qualificationDto);
            qualification.Id = id;
            return await _qualifiactionRepository.UpdateAsync(qualification);
        }
        //Delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _qualifiactionRepository.DeleteAsync(id);

        }
       
    }
}
