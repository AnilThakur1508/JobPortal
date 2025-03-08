using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DTO;
using DataAccessLayer.Entity;
using DataAccessLayer.Data;
using AutoMapper;
using DataAccessLayer.PortalRepository;

namespace Service.Implementation
{
    public class AddressService : IAddressService
    {
        private readonly IRepository<Address> _addressRepository;
        private readonly IMapper _mapper;

        public AddressService(IRepository<Address> addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }
        public async Task<AddressDto> AddAsync(AddressDto addressDto)
        {
            var address = _mapper.Map<Address>(addressDto);
            await _addressRepository.AddAsync(address);
            return addressDto;
        }
        public async Task<IEnumerable<AddressDto>> GetAllAsync()
        {

            var addresses = await _addressRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AddressDto>>(addresses);
        }

    }
}