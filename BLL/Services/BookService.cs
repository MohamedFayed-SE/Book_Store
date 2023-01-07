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
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _Context;

        private readonly IMapper _Mapper;

        public BookService(ApplicationDbContext context,IMapper mapper)
        {
            _Context = context;
             _Mapper = mapper;
        }

        public async  Task<BookDTo> AddAsync(BookDTo book)
        {
            try
            {
                if (book != null)
                {
                    var Book = _Mapper.Map<Book>(book);
                    await _Context.AddAsync(Book);
                    _Context.SaveChanges();
                    return book;
                }
                else
                    throw new Exception("Cannot Add Book With Value Null");
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            
        }

        public async Task<BookDTo> DeleteAsync(int id)
        {
            try
            {
                var Book = await _Context.books.FindAsync(id);
                var Result= _Mapper.Map<BookDTo>(Book);
                if (Book != null)
                {
                    _Context.Remove(Book);
                    _Context.SaveChanges();
                    return Result;
                }
                else
                    throw new Exception($"Cannot Find Book With Id={id}");
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);

            }
            
           
        }

        public async  Task<BookDTo> GetBookByIdAsync(int id)
        {
          
            try
            {
                var Book = await _Context.books.Include(b=>b.Category).FirstOrDefaultAsync(b=>b.Id==id);
                var Result = _Mapper.Map<BookDTo>(Book);
                if (Book != null)
                    return Result;
                else
                    throw new Exception($"Cannot Find Book With Id={id}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

        public async Task<IEnumerable<BookDTo>> GetBooksAsync()
        {
            var Books = await _Context.books.Include(b=>b.Category).ToListAsync();
            var Result = _Mapper.Map<IEnumerable<BookDTo>>(Books);

            return Result;



        }

        public async Task<BookDTo> UpdateAsync(BookDTo book)
        {
            try
            {
                var Book = await _Context.books.FindAsync(book.Id);
               
                if (Book != null)
                {

                    Book.Name = book.Name;
                    Book.CategoryId=book.CategoryId;
                    Book.Rating = (float)book.Rating;

                    _Context.Update(Book);
                   await _Context.SaveChangesAsync();
                    var Result=_Mapper.Map<BookDTo>(Book);
                    return Result;


                }
                   
                else
                    throw new Exception($"Cannot Find Book With Id={book.Id}");

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
