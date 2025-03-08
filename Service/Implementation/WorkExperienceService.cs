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
using Service.Interface;

namespace Service.Implementation
{
    public class WorkExperienceService : IWorkExperienceService
    {
        private readonly IRepository<WorkExperience> _workExperienceRepository;
        private readonly IMapper _mapper;
        

        public WorkExperienceService(IRepository<WorkExperience> workExperienceRepository, IMapper mapper)
        {
            _workExperienceRepository = workExperienceRepository;
            _mapper = mapper;
            
        }
        //GetAll
        public async Task<IEnumerable<WorkExperienceDto>> GetAllAsync()
        {
            var experiences = await _workExperienceRepository.GetAllAsync();
             var experiencesDto =_mapper.Map<IEnumerable<WorkExperienceDto>>(experiences);
            return experiencesDto;
        }
        //Add
        public async Task<WorkExperienceDto> AddAsync(WorkExperienceDto workExperienceDto)
        {
            var experience =  _mapper. Map<WorkExperience>(workExperienceDto);
            await _workExperienceRepository.AddAsync(experience);
            return workExperienceDto;
        }
        //GetbyId
        public async Task<WorkExperienceDto> GetByIdAsync(Guid id)
        {
            var experience = await _workExperienceRepository.GetByIdAsync(id);
            if (experience == null)
            {
                return null; // Or throw an exception if needed
            }
            return _mapper.Map<WorkExperienceDto>(experience);
        }
        
        //Update
        public async Task<bool> UpdateAsync(Guid id, WorkExperienceDto workExperienceDto)
        {
            var workExperience = _mapper.Map<WorkExperience>(workExperienceDto);
            workExperience.Id = id;
            return await _workExperienceRepository.UpdateAsync(workExperience);
        }


        //Delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _workExperienceRepository.DeleteAsync(id);

        }
      




    }

}
