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
    public class QualificationService : IQualificationService
    {
        private readonly IRepository<Qualification> _qualifiactionRepository;
        private readonly IMapper _mapper;

        public QualificationService(IRepository<Qualification> repository, IMapper mapper)
        {
            _qualifiactionRepository = repository;  
            _mapper = mapper;

        }
        //Get
        public async Task<IEnumerable<QualificationDto>> GetAllAsync()
        {

            var qualification = await _qualifiactionRepository.GetAllAsync();
            var qualificationDto = _mapper.Map<IEnumerable<QualificationDto>>(qualification);
            return qualificationDto;
        }
        //Add
        public async Task<QualificationDto> AddAsync(QualificationDto qualificationDto)
        {
            var qualification = _mapper.Map<Qualification>(qualificationDto);
            await _qualifiactionRepository.AddAsync(qualification);
            return qualificationDto;
        }
    }
}
