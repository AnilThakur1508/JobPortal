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
    public class EmployerService : IEmployerService
    {
        private readonly IRepository<Employer> _repository;

        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<Address> _addressService;


        public EmployerService(
            UserManager<AppUser> userManager,
            IRepository<Employer> repository,
            IRepository<Address> addressService,

             IMapper mapper)


        {
            _repository = repository;
            _mapper = mapper;
            _addressService = addressService;
            _userManager = userManager;

        }

        public async Task<IEnumerable<EmployerDto>> GetAllAsync()
        {
            var employers = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployerDto>>(employers);
        }

        public async Task<EmployerDto> GetByIdAsync(Guid id)
        {
            var employer = await _repository.GetByIdAsync(id);
            if (employer == null)
            {
                throw new KeyNotFoundException($"Employer with ID {id} not found");
            }
            return _mapper.Map<EmployerDto>(employer);
        }
        public async Task<bool> UpsertAsync(EmployerDto employerDto)
        {

            var existingEmployer = await _repository.FirstOrDefaultAsync(e => e.UserId == employerDto.UserId);
            var existingAddress = await _addressService.FirstOrDefaultAsync(e => e.UserId == employerDto.UserId);




            if (existingEmployer != null)
            {

                employerDto.Id = existingEmployer.Id;


                _mapper.Map(employerDto, existingEmployer);


                await _repository.UpdateAsync(existingEmployer);
                if (employerDto.Address != null && existingAddress != null)
                {
                    employerDto.Address.UserId = existingEmployer.UserId;
                    _mapper.Map(employerDto.Address, existingAddress);
                    await _addressService.UpdateAsync(existingAddress);
                    return true;
                }
                return false;
            }
            else
            {
                var employer = _mapper.Map<Employer>(employerDto);




                if (await _repository.AddAsync(employer))
                {
                    if (employerDto.Address != null)
                    {
                        employerDto.Address.UserId = employer.UserId;
                        await _addressService.AddAsync(_mapper.Map<Address>(employerDto.Address));
                    }
                    return true;
                }
                return false;
            }
        }

        public async Task<EmployerDto> GetByUserIdAsync(Guid id)
        {
            var employer = await _repository.FirstOrDefaultAsync(e => e.UserId == id);
            var address = await _addressService.FirstOrDefaultAsync(x => x.UserId == id);

            if (employer == null)
            {
                throw new KeyNotFoundException("Employer not found.");
            }
            var employerDto = _mapper.Map<EmployerDto>(employer);
            employerDto.Address = _mapper.Map<AddressDto>(address);
            return employerDto;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

    }
}

