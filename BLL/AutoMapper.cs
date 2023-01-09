using AutoMapper;
using BAL.Models;
using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AutoMapper:Profile
    {

        public AutoMapper()
        {
            CreateMap<BookDTo, Book>().ReverseMap();
                
            CreateMap<Author, AuthorDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<AuthorBook, AuthorsBooksDto>().ReverseMap();
        }
    }
}
