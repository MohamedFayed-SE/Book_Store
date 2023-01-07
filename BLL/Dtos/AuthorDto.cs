using BAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class AuthorDto
    {
        
        public int? Id { get; set; }
        [Required(ErrorMessage ="Name is Required"),StringLength(50,ErrorMessage ="Max Lenth Is 50")]
        public string Name { get; set; }

        public List<Book>? BookList { get; set; }
    }
}
