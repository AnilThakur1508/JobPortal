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
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> _countryRepository;
        private readonly IMapper _mapper;

        public CountryService(IRepository<Country> repository, IMapper mapper)
        {
            _countryRepository = repository;
            _mapper = mapper;

        }
        public async Task<IEnumerable<CountryDto>> GetAllAsync()
        {

            var country = await _countryRepository.GetAllAsync();
             return _mapper.Map<IEnumerable<CountryDto>>(country);

        }
        public async Task<CountryDto> AddAsync(CountryDto countryDto)
        {
            var country = _mapper.Map<Country>(countryDto);
            await _countryRepository.AddAsync(country);
            return countryDto;
        }
    }
}

