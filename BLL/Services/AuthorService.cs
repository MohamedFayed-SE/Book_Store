using AutoMapper;
using BAL;
using BAL.Models;
using BLL.Dtos;
using BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthorService:IAutherService
    {
        private readonly ApplicationDbContext _Context;

        private readonly IMapper _Mapper;

        public AuthorService(ApplicationDbContext context, IMapper mapper)
        {
            _Context = context;
            _Mapper = mapper;
        }

        public async Task<AuthorDto> AddAsync(AuthorDto author)
        {
            var Author = _Mapper.Map<Author>(author);
            await _Context.AddAsync(Author);
            _Context.SaveChanges();
            return author;
        }

        public async Task<AuthorDto> DeleteAsync(int id)
        {
            try
            {
                var Author = await _Context.Authors.FindAsync(id);
                var Result = _Mapper.Map<AuthorDto>(Author);
                if (Author != null)
                {
                    _Context.Remove(Author);
                    _Context.SaveChanges();
                    return Result;
                }
                else
                    throw new Exception($"Cannot Find Author With Id={id}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }


        }

        

       

        public async Task<AuthorDto> GetAuthorByIdAsync(int id)
        {

            try
            {
                var Author = await _Context.Authors.SingleOrDefaultAsync(Auth=> Auth.Id==id);
                var Result = _Mapper.Map<AuthorDto>(Author);
                if (Author != null)
                    return Result;
                else
                    throw new Exception($"Cannot Find Author With Id={id}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

        public async Task<IEnumerable<AuthorDto>> GetAuthorsAsync()
        {
            var Authors = await _Context.Authors.ToListAsync();
            var Result = _Mapper.Map<IEnumerable<AuthorDto>>(Authors);

            return Result;



        }

        public async Task<AuthorDto> UpdateAsync(AuthorDto author)
        {
            try
            {
                var AuthorP = await _Context.Authors.SingleOrDefaultAsync(Auth=>Auth.Id== author.Id);

                if (AuthorP != null)
                {

                    AuthorP.Name = author.Name;
                   
                    var Result = _Mapper.Map<AuthorDto>(AuthorP);
                    _Context.Authors.Update(AuthorP);
                    await _Context.SaveChangesAsync();
                    return Result;
                }

                else
                    throw new Exception($"Cannot Find Author With Id={author.Id}");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
