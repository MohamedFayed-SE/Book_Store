using BAL.Models;
using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAutherService
    {
        public Task<IEnumerable<AuthorDto>> GetAuthorsAsync();

        public Task<AuthorDto> GetAuthorByIdAsync(int id);

        public Task<AuthorDto> AddAsync(AuthorDto Auther);

        public Task<AuthorDto> UpdateAsync(AuthorDto Auther);

        public Task<AuthorDto> DeleteAsync(int id);
    }
}
