using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Library;
using CkApp.Web.Models.Utilities;
using CkApp.Web.Models.CompanyManagement;

namespace CkApp.Web.Models.PeopleManagement
{
    public class MPeople : MBase
    {

        public Person Person;
        public int AppId { get; set; }

        public IList<Order> Orders;

        public PersonCredential Credential;
        
        public MCountry Country = new MCountry();
        public MPeopleRole PeopleRole = new MPeopleRole();

        public void Get(int personid)
        {
            this.Person = db.People.Where(x => x.PersonId == personid).FirstOrDefault();
        }

        public void GetOrder(int personid)
        {
            this.Orders = db.Orders.Where(x => x.PersonId == personid).ToList();
        }

        public void GetCredential(int personid)
        {
            this.Credential = db.PersonCredentials.Where(x => x.PersonId == personid).FirstOrDefault();
        }

        public void Insert(Person model)
        {
            if (model != null)
            {
                db.People.Add(model);
                db.SaveChanges();
                Person = model;
            }
        }

        public void InsertCredential(PersonCredential model)
        {
            if(model != null)
            {
                db.PersonCredentials.Add(model);
                db.SaveChanges();
            }
        }

        public void Update(Person model, bool updated_pic = false)
        {
            if (model != null)
            {
                var _p = (from x in db.People where x.PersonId == model.PersonId select x).FirstOrDefault();

                this.Person = _p;

                _p.Firstname = model.Firstname;
                _p.Lastname = model.Lastname;
                _p.Email = model.Email;
                _p.Gsm = model.Gsm;
                _p.Address = model.Address;

                _p.CompanyId = model.CompanyId;

                if (updated_pic)
                    _p.Image = model.Image;

                db.SaveChanges();
            }
        }

        public void UpdateCredential(PersonCredential model)
        {
            if (model != null)
            {
                var _c = (from x in db.PersonCredentials where x.PersonId == model.PersonId select x).FirstOrDefault();

                _c.Username = model.Username;
                _c.Password = model.Password;

                db.SaveChanges();
            }
        }

    }
}