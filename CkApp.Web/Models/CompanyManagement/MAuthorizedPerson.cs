using CkApp.Web.Models.Utilities;
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CkApp.Web.Models.CompanyManagement
{
    public class MAuthorizedPerson : MBase
    {

        public int PersonId { get; set; }

        public Person AuthorizedPerson;

        public IList<Person> People;


        public void Load(int companyid)
        {
            this.People = db.People.Where(x => x.CompanyId == companyid).ToList();
            this.GetAuthorizedPerson(companyid);
        }

        public void GetAuthorizedPerson(int companyid)
        {
            if (companyid != 0)
            {
                this.AuthorizedPerson = (from x in db.People
                                         where x.CompanyId == companyid && x.IsAuthorized == true
                                         select x).FirstOrDefault();
            }

            if(this.AuthorizedPerson == null)
            {
                this.AuthorizedPerson = new Person();
            }
        }

    }
}