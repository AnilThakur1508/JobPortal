using AutoMapper;
using DataAccessLayer.Entity;
using DataAccessLayer.PortalRepository;
using DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
     public class EmployerService : IEmployerService
     {
            private readonly IRepository<Employer> _repository;
            private readonly IMapper _mapper;
            private readonly UserManager<AppUser> _userManager;
            private readonly IWebHostEnvironment _webHostEnvironment;
            private readonly ILogger<EmployerService> _logger;
            private readonly IAddressService _addressService;



        public EmployerService(UserManager<AppUser> userManager,IRepository<Employer> repository, IMapper mapper, IWebHostEnvironment webHostEnvironment, ILogger<EmployerService> logger , IAddressService addressService)
          
            {
                _repository = repository;
                _mapper = mapper;
                _webHostEnvironment = webHostEnvironment;
                 _logger = logger;
                _userManager = userManager;
               _addressService = addressService;


        }
        //GetAll
        public async Task<IEnumerable<EmployerDto>> GetAllAsync()
            {
                var employers = await _repository.GetAllAsync();
                return _mapper.Map<IEnumerable<EmployerDto>>(employers);
            }
         //GetById

            public async Task<EmployerDto?> GetByIdAsync(Guid id)
            {
            var employer = await _repository.GetByIdAsync(id);
    
                return employer == null ? null : _mapper.Map<EmployerDto>(employer);
            }
        public async Task<bool> UpsertAsync(EmployerDto employerDto)
        {
            //var address = _mapper.Map<Address>(employerDto.Address);
            await _addressService.AddAsync(employerDto.Address);


            // 🔹 Map DTO to Employee entity
            var employer = _mapper.Map<Employer>(employerDto);

            // 🔹 Handle logo upload
            if (employerDto.Logo != null)
            {
                employer.Logo = await UploadFileAsync(employerDto.Logo);
            }

            // 🔹 Check if employee already exists
            var existingEmployer = await _repository.GetByIdAsync(employer.Id);

            if (existingEmployer != null)
            {
                // 🔹 Update employee
                return await _repository.UpdateAsync(employer);
            }
            else
            {
                // 🔹 Insert new employee
                return await _repository.AddAsync(employer);
            }
        }

        //// Update
        //public async Task<bool> UpdateAsync(Guid id, EmployerDto employerDto)
        //    {
        //        var employer = _mapper.Map<Employer>(employerDto);
        //        employer.Id = id;
        //        return await _repository.UpdateAsync(employer);
        //    }
        //  //Delete
            public async Task<bool> DeleteAsync(Guid id)
            {
              return await _repository.DeleteAsync(id);
    
            }
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No file uploaded.");
            }

            try
            {
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadPath);

                string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                string filePath = Path.Combine(uploadPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                string fileUrl = $"/uploads/{uniqueFileName}";
                _logger.LogInformation($"File uploaded successfully: {fileUrl}");

                return fileUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError($"File upload failed: {ex.Message}");
                throw new Exception("File upload failed.", ex);
            }
        }
        public async Task<string> AddFileAsync(IFormFile file, string subFolder = "uploads")
        {
            if (file == null) return null;

            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", subFolder);
            if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{subFolder}/{fileName}";
        }

    }



}






