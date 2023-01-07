using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
        public Task<CategoryDto> AddAsync(CategoryDto category);
        public Task<CategoryDto> UpdateAsync(CategoryDto category);
        public Task<CategoryDto> GetById(int id);

        public void DeleteAsync(int id);


    }
}
