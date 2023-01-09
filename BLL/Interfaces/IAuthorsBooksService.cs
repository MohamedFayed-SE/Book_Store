using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.interfaces
{
    public interface IAuthorsBooksService
    {
        public Task<IEnumerable<AuthorsBooksDto>> GetAuthorsBooksAsync();
        public Task<AuthorsBooksDto> AddAsync(AuthorsBooksDto authorBook);
        public Task<AuthorsBooksDto> UpdateAsync(AuthorsBooksDto authorBook);
        public Task<AuthorsBooksDto> DeleteAsync(int id);



    }
}
