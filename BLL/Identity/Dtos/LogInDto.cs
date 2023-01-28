using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Identity.Dtos
{
    public class LogInDto
    {
        [Required,EmailAddress(ErrorMessage ="Your Email Is not Valid")]
        public string Email { get; set; }
        [Required(ErrorMessage ="This Field Is Required")]
        public string Password { get; set; }
    }
}
