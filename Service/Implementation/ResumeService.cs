using AutoMapper;
using DataAccessLayer.Data;
using DataAccessLayer.Entity;
using DataAccessLayer.PortalRepository;
using DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class ResumeService : IResumeService
    {
        private readonly IRepository<Resume> _resumeRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;


        public ResumeService(IRepository<Resume> resumeRepository, IMapper mapper, AppDbContext context, IWebHostEnvironment environment)
        {
            _resumeRepository = resumeRepository;
            _mapper = mapper;
            _context = context;
            _environment = environment;

        }
        //GetAll
        public async Task<IEnumerable<ResumeDto>> GetAllAsync()
        {
            var resume = await _resumeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ResumeDto>>(resume);
        }
        //GetById

        public async Task<ResumeDto?> GetByIdAsync(Guid id)
        {
            var resume = await _resumeRepository.GetByIdAsync(id);

            return resume == null ? null : _mapper.Map<ResumeDto>(resume);
        }
        //Add
        public async Task<bool> AddAsync(ResumeDto resumeDto)
        {
            var resume = _mapper.Map<Resume>(resumeDto);
            resume.ResumeFile = await AddAsync(resumeDto.ResumeFile);
            return await _resumeRepository.AddAsync(resume);
        }

       

        // Update
        public async Task<bool> UpdateAsync(Guid id, ResumeDto resumeDto)
        {
            var resume = _mapper.Map<Resume>(resumeDto);
            resume.Id = id;
            return await _resumeRepository.UpdateAsync(resume);
        }
        //Delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _resumeRepository.DeleteAsync(id);

        }

        public async Task<string> AddAsync(IFormFile file)
        {
            if (file == null) return null;

            var uploadPath = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{fileName}";
        }

        
        

    }
}
