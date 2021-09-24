using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CkApp.Web.Models.PeopleManagement
{
    public class MPeopleCreate : MPeople
    {

        public RoleMenu RoleMenu;

        public IList<RoleModule> RoleModule;

        public IList<RoleModuleMethod> RoleModuleMethod;



    }
}