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
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Service.Interface;

namespace Service.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Address> _addressService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<EmployeeService> _logger;
        

        public EmployeeService(IRepository<Employee> repository, IRepository<Address> addressService, UserManager<AppUser> userManager, IMapper mapper,IWebHostEnvironment webHostEnvironment, ILogger<EmployeeService> logger)
        {
            _employeeRepository = repository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _logger = logger;
            _addressService = addressService;


        }
        //GetAll
        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {

            var employees = await _employeeRepository.GetAllAsync();
           var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return employeesDto;

        }
        //GetById
        public async Task<EmployeeDto?> GetByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            return employee == null ? null : _mapper.Map<EmployeeDto>(employee);
        }

       // Add
        public async Task<bool> UpsertAsync(EmployeeDto employeeDto)
        {

            //var address = _mapper.Map<Address>(employeeDto.Address);
            // 🔹 Map DTO to Employee entity
            var existingEmployee = await _employeeRepository.FirstOrDefaultAsync(e => e.UserId == employeeDto.UserId);

            if (existingEmployee != null)
            {
                // 🔹 Update existing entity instead of creating a new instance
                _mapper.Map(employeeDto, existingEmployee); // This updates existingEmployee in-place

                return await _employeeRepository.UpdateAsync(existingEmployee);

            }

            else
            {
                var employee = _mapper.Map<Employee>(employeeDto);
                // 🔹 Insert new employee
                if (await _employeeRepository.AddAsync(employee))
                {
                    if (employeeDto.Address != null)
                    {
                        employeeDto.Address.UserId = employee.UserId; // Ensure correct UserId is set
                        await _addressService.AddAsync(_mapper.Map<Address>(employeeDto.Address));
                    }
                    return true;
                }

                return false;
            }
        }






        //Delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _employeeRepository.DeleteAsync(id);

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

        public async Task<EmployeeDto> GetByUserIdAsync(Guid id)
        {
            var employee = await _employeeRepository.FirstOrDefaultAsync(e => e.UserId == id);
            if (employee == null)
            {
                throw new KeyNotFoundException("Employee not found.");
            }
            return _mapper.Map<EmployeeDto>(employee);
        }
       
    }
}



       

       
        

    

    

