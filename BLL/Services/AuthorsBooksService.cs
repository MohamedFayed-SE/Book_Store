using AutoMapper;
using BAL;
using BAL.Models;
using BLL.Dtos;
using BLL.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthorsBooksService : IAuthorsBooksService
    {
        private readonly ApplicationDbContext _Context;
        private readonly IMapper _Mapper;
        public AuthorsBooksService(ApplicationDbContext context,IMapper mapper)
        {
             _Context = context;
            _Mapper = mapper;
        }
        public  async Task<AuthorsBooksDto> AddAsync(AuthorsBooksDto authorBook)
        {

           var AuthorsBooks=_Mapper.Map<AuthorBook>(authorBook);
           await   _Context.AuthorBooks.AddAsync(AuthorsBooks);
            await _Context.SaveChangesAsync();
            return authorBook;
            
        }

        public Task<AuthorsBooksDto> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AuthorsBooksDto>> GetAuthorsBooksAsync()
        {
            var AuthorsBooks = await _Context.AuthorBooks.Include(ab => ab.Author).Include(ab => ab.Book)
                .ThenInclude(b=>b.Category)
                .ToListAsync();
            var Result = _Mapper.Map<IEnumerable<AuthorsBooksDto>>(AuthorsBooks);
            return Result;
        }

        public Task<AuthorsBooksDto> UpdateAsync(AuthorsBooksDto authorBook)
        {
            throw new NotImplementedException();
        }
    }
}
