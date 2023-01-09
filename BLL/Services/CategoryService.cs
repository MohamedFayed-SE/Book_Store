using AutoMapper;
using BAL;
using BAL.Models;
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
                    var Category = _Mapper.Map<Category>(category);
                    var Result = await _Context.Categories.AddAsync(Category);
                    await _Context.SaveChangesAsync();
                    
                    return category;
                }
                else
                    throw new Exception("Cannot Add Nulll Category");
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var Category =  _Context.Categories.SingleOrDefault(c => c.Id == id);
                if (Category != null)
                {
                    _Context.Remove(Category);
                    _Context.SaveChanges();
                   
                }
                else
                    throw new Exception($"Cannot Find Category With Id ={id}");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CategoryDto> GetById(int id)
        {
            try
            {
                var Category =  await _Context.Categories.SingleOrDefaultAsync(c=>c.Id==id);
                if (Category != null)
                {
                    var Result = _Mapper.Map<CategoryDto>(Category);
                    return Result;
                }
                else
                    throw new Exception($"Cannot Find Category With Id ={id}");
                
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var Categories = await _Context.Categories.ToListAsync();
            var Result = _Mapper.Map<IEnumerable<CategoryDto>>(Categories);
            return (Result);

        }

        public  async Task<CategoryDto> UpdateAsync(CategoryDto category)
        {
            try
            {
                var Category = await _Context.Categories.SingleOrDefaultAsync(c => c.Id == category.Id);
                if (Category != null)
                {
                    Category.Name = category.Name;
                    await _Context.SaveChangesAsync();
                    var Result = _Mapper.Map<CategoryDto>(Category);
                    return Result;
                }
                else
                    throw new Exception($"Cannot Find Category With Id ={category.Id}");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
