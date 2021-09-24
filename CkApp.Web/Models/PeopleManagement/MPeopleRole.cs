using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CkApp.Web.Models.PeopleManagement
{
    public class MPeopleRole : MPeople
    {

        public IList<Menu> AllMenuList;
        public IList<Module> AllModuleList;

        public RoleMenu PersonRoleMenu;
        public IList<RoleModule> PersonRoleModule;


        public void Load(int personid = 0)
        {

            this.PersonRoleMenu = db.RoleMenus.Where(x => x.PersonId == personid).FirstOrDefault();
            this.PersonRoleModule = db.RoleModules.Where(x => x.PersonId == personid).ToList();

            this.AllMenuList = db.Menus.ToList();
            this.AllModuleList = db.Modules.ToList();

        }

    }
}