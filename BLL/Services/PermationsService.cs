using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public static class PermationsService
    {
        enum Modules
        {
            Books,
            Authors,
            AuthorsBooks,
            Categories,
            Roles,
            Users,
            UserRoles,

        }

        enum Actions
        {
            Create,
            Update,
            view,
        }

        public static List<string> getAllModules()
        {
          
            var  modules=   new List<string>();

            foreach (var item in Enum.GetValues(typeof(Modules)))
            {
                modules.Add(item.ToString());
            };

            return modules;

        }

        public static  List<string> getAllModulesWithActions()
        {
            var allPermations = new List<string>();

            foreach (var item in getAllModules())
            {

                allPermations.Add($"{item},{Actions.Create}");
                allPermations.Add($"{item},{Actions.view}");
                allPermations.Add($"{item},{Actions.Update}");

            }

            return allPermations;
        }
    }


    
}
