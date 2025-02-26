using AutoMapper;
using DataAccessLayer.Entity;
using DataAccessLayer.PortalRepository;
using DTO;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class AddressService :IAddressService
    {
        private readonly IRepository<Address> _addressRepository;
        private readonly IMapper _mapper;

        public AddressService(IRepository<Address> repository, IMapper mapper)
        {
            _addressRepository = repository;
            _mapper = mapper;

        }
        public async Task<IEnumerable<AddressDto>> GetAllAsync()
        {

            var addresses = await _addressRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AddressDto>>(addresses);

        }
        public async Task<AddressDto> AddAsync(AddressDto addressDto)
        {
            var address = _mapper.Map<Address>(addressDto);
            await _addressRepository.AddAsync(address);
            return addressDto;
        }
    }



}

