using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
    public class CategoryDto
    {
       
        public int? Id { get; set; }
        [Required(ErrorMessage ="Name is Required"),StringLength(30,ErrorMessage ="Max LEnth Of Name Equal 30")]
        public string Name { get; set; }
    }
}
