using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Library;

namespace CkApp.Web.Models.PeopleManagement
{
    public class MPeopleList : MPeople
    {

        public IList<Person> People;

        public MPeopleList(int appid)
        {
            this.AppId = appid;
        }

        public void List()
        {
            this.People = db.People.Where(x => x.AppId == AppId).ToList();
        }

        public void List(int companyid)
        {
            this.People = db.People.Where(x => x.CompanyId == companyid && x.AppId == AppId).ToList();
        }

    }
}