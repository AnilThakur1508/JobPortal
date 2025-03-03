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
    public class StateService : IStateService
    {
        private readonly IRepository<State> _stateRepository;
        private readonly IMapper _mapper;
        
        public StateService(IRepository<State> repository, IMapper mapper)
        {
            _stateRepository = repository;
            _mapper = mapper;

        }
        //Add
        public async Task<IEnumerable<StateDto>> GetAllAsync()
        {

            var state = await _stateRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StateDto>>(state);
            

        }
        public async Task<StateDto> AddAsync(StateDto stateDto)
        {
            var state = _mapper.Map<State>(stateDto);
            await _stateRepository.AddAsync(state);
            return stateDto;
        }
    }
}

