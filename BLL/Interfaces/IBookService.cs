using BAL.Models;
using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBookService
    {
        public  Task<IEnumerable<BookDTo>> GetBooksAsync();

        public Task<BookDTo> GetBookByIdAsync(int id);

        public Task<BookDTo> AddAsync(BookDTo book);

        public Task<BookDTo> UpdateAsync(BookDTo book);

        public Task<BookDTo> DeleteAsync(int id);

       

    }
}
