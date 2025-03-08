using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;

public interface IAddressService
{
    Task<AddressDto> AddAsync(AddressDto addressDto);
    Task<IEnumerable<AddressDto>> GetAllAsync();
}
