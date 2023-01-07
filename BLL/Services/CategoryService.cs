using AutoMapper;
using BAL;
using BLL.Dtos;
using BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _Context;
        private readonly IMapper _Mapper;

        public CategoryService(ApplicationDbContext contex,IMapper mapper)
        {
            _Context = contex;  
            _Mapper= mapper;
        }

        public async Task<CategoryDto> AddAsync(CategoryDto category)
        {
            try
            {
                if (category != null)
                {
                    var Category = await _Context.AddAsync(category);
                    await _Context.SaveChangesAsync();
                    var Result = _Mapper.Map<CategoryDto>(Category);
                    return Result;
                }
                else
                    throw new Exception("Cannot Add Nulll Category");
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var Categories = await _Context.Categories.ToListAsync();
            var Result = _Mapper.Map<IEnumerable<CategoryDto>>(Categories);
            return (Result);

        }

        public Task<CategoryDto> UpdateAsync(CategoryDto category)
        {
            throw new NotImplementedException();
        }
    }
}
