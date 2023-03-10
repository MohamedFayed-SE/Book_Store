using BAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class AuthorsBooksDto
    {
        [Required(ErrorMessage ="This Field Is Required")]
        public int BookId { get; set; }
  
        public Book? Book { get; set; }
        [Required(ErrorMessage = "This Field Is Required")]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
  

    
       
    }
}
