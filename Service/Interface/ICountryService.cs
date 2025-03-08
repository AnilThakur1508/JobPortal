using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICountryService
    {
        Task<CountryDto> AddAsync(CountryDto countryDto);
        Task<IEnumerable<CountryDto>> GetAllAsync();
    }
}

