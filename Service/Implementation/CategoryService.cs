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
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _CategoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> repository, IMapper mapper)
        {
            _CategoryRepository = repository;
            _mapper = mapper;

        }
        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {

            var categories = await _CategoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
    }
}
