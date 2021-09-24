using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Library;

namespace CkApp.Web.Models.Utilities
{
    public class MNavigation : MBase
    {

        public int AppId { get; set; }
        public int PersonId { get; set; }

        public string Html { get; set; }

        public Menu Menu;
        public IList<MenuItem> Items;


        public MNavigation(int personid, int appid)
        {
            this.PersonId = personid;
            this.AppId = appid;
        }

        private void __load()
        {

            var _rolemenu = db.RoleMenus.Where(x => x.PersonId == PersonId).FirstOrDefault();
            if (_rolemenu != null)
            {
                Menu = db.Menus.Where(x => x.MenuId == _rolemenu.MenuId && x.AppId == AppId).FirstOrDefault();
                if (Menu != null)
                {
                    Items = db.MenuItems.Where(x => x.MenuId == Menu.MenuId).OrderBy(y => y.Sorting).ToList();
                }
            }

        }

        public void Prepare()
        {
            __load();

            if (Items != null)
            {
                foreach (MenuItem item in Items)
                {
                    var _temp = (from x in Items where x.ParentMenuItemId == item.MenuItemId select x).OrderBy(y => y.Sorting).ToList();
                    if (_temp.Count > 0)
                    {
                        __add("<li class='nav-parent'><a href='#'><i class='" + item.Icon
                            + "'></i> <span>" + item.Title + "</span></a><ul class='children'>");
                        foreach (var subitem in _temp)
                        {
                            __add("<li><a href='" + subitem.Url + "'><i class='" + subitem.Icon + "'></i> " + subitem.Title + "</a></li>");
                        }
                        __add("</ul></li>");
                    }
                    else
                    {
                        if (item.ParentMenuItemId == 0)
                            __add("<li><a href='" + item.Url + "'><i class='" + item.Icon
                                + "'></i> <span>" + item.Title + "</span></a></li>");
                    }
                }
            }
        }

        private void __add(string str)
        {
            Html += str;
        }

    }
}