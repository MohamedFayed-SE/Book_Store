using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Identity.Dtos
{
    public class UserRoleVm
    {
        
        public string UserId { get; set; }
        public string UserName { get; set; }

       public List<CheckBox> UserRoles { get; set; }

      

    }
}
