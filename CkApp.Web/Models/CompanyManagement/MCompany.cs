using CkApp.Web.Models.Ordering;
using CkApp.Web.Models.PeopleManagement;
using CkApp.Web.Models.Utilities;
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CkApp.Web.Models.CompanyManagement
{
    public class MCompany : MBase
    {

        public int AppId;

        public MPeople People;
        public Company Company;
        public Company ParentCompany;


        public Person PersonCompany;
        public Person PersonParentCompany;
        public IList<Person> PeopleCompany;
        public MAuthorizedPerson MAuthPerson;
        public MOrderList OrderList;
        public MPeopleList PeopleList;

        public MCompany(int AppId = 0)
        {
            People = new MPeople();
            MAuthPerson = new MAuthorizedPerson();
            OrderList = new MOrderList(AppId);
            PeopleList = new MPeopleList(AppId);
        }

        public void GetCompany(int companyid)
        {

            this.Company = db.Companies.Where(x => x.CompanyId == companyid).FirstOrDefault();

            if (this.Company != null)
            {
                this.PersonCompany = db.People
                    .Where(x => x.CompanyId == this.Company.CompanyId
                    && x.IsAuthorized == true).FirstOrDefault();
            }

            this.GetParentCompany();

        }

        public void GetNewCompany(int companyid)
        {
            this.GetParentCompany(companyid);
            this.Company = new Company();
        }

        public void GetParentCompany(int companyid = 0)
        {

            if (this.Company != null)
            {
                this.ParentCompany = db.Companies
                    .Where(x => x.CompanyId == this.Company.ParentCompanyId).FirstOrDefault();
            }
            else
            {
                this.ParentCompany = db.Companies.Where(x => x.CompanyId == companyid).FirstOrDefault();
            }

            if (this.ParentCompany != null)
            {
                this.PersonParentCompany = db.People
                    .Where(x => x.CompanyId == this.ParentCompany.CompanyId
                    && x.IsAuthorized == true).FirstOrDefault();
            }

        }

        public void Update(Company model)
        {
            if (model != null)
            {
                var _company = (from x in db.Companies where x.CompanyId == model.CompanyId select x).FirstOrDefault();

                if (_company != null)
                {
                    _company.Title = model.Title;
                    _company.Email = model.Email;
                    _company.Gsm = model.Gsm;
                    _company.Address = model.Address;
                    _company.TaxInformation = model.TaxInformation;
                    _company.TaxNumber = model.TaxNumber;

                    db.SaveChanges();

                    this.Company = _company;
                }
            }
        }

        public void Insert(Company model)
        {
            if (model != null)
            {
                db.Companies.Add(model);
                db.SaveChanges();
                this.Company = model;
            }
        }

        public void ChangePerson(MAuthorizedPerson model)
        {
            if (model != null)
            {

                this.PersonCompany = db.People.Where(x => x.PersonId == model.PersonId).FirstOrDefault();

                var _people = (from x in db.People
                               where x.CompanyId == PersonCompany.CompanyId
                               select x).ToList();
                
                if (_people != null && _people.Count > 0)
                {
                    foreach (Person item in _people)
                    {
                        item.IsAuthorized = false;
                    }
                    
                }

                var _changePerson = (from x in db.People where x.PersonId == model.PersonId select x).FirstOrDefault();

                _changePerson.IsAuthorized = true;

                db.SaveChanges();

            }
        }

    }
}