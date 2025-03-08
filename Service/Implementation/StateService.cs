using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using DataAccessLayer.Entity;
using DTO;
using Service.Interface;
using DataAccessLayer.Data;
using DataAccessLayer.PortalRepository;

namespace Service.Implementation
{

    public class StateService : IStateService
    {
        private readonly IRepository<State> _repository;

        public StateService(IRepository<State> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StateDto>> GetAllAsync()
        {
            var states = await _repository.GetAllAsync();
            return states.Select(s => new StateDto
            {
                CountryId = s.CountryId,
                Name = s.Name,

            }).ToList();
        }

        public async Task<StateDto> GetByIdAsync(Guid id)
        {
            var state = await _repository.GetByIdAsync(id);

            if (state == null)
                return null;

            return new StateDto
            {
                CountryId = state.CountryId,
                Name = state.Name,
            };
        }
    
    }   
}

