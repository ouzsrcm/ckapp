using CkApp.Web.Models.CompanyManagement;
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CkApp.Web.Controllers
{
    public class dealerController : baseController
    {

        public MCompany company;
        public MCompanyList list;

        public ActionResult index()
        {
            list = new MCompanyList();
            list.GetDealers(Company.CompanyId);
            return View("all", list);
        }

        public ActionResult all()
        {
            list = new MCompanyList();
            list.AppId = AppId;
            list.GetDealers(Company.CompanyId);
            return View(list);
        }

        public ActionResult details()
        {
            if (Id != 0)
            {
                company = new MCompany(AppId);
                company.GetCompany(Id);
                company.MAuthPerson.Load(company.Company.CompanyId);
                company.OrderList.GetOldOrdersByCompany(Id);
                company.PeopleList.List(Id);
                return View(company);
            }
            else return Redirect("/dealer");
        }

        public ActionResult create()
        {
            company = new MCompany(AppId);
            company.GetNewCompany(Company.CompanyId);
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult changeperson(MAuthorizedPerson model)
        {
            company = new MCompany(AppId);
            if (ModelState.IsValid)
            {
                company.ChangePerson(model);
            }
            return Redirect("/dealer/details/" + company.PersonCompany.CompanyId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult saveinfo(Company model)
        {
            if (ModelState.IsValid) {
                company = new MCompany(AppId);
                if (Id != 0)
                {
                    model.CompanyId = Id;
                    company.Update(model);
                } else
                {
                    model.AppId = AppId;
                    model.ParentCompanyId = Company.CompanyId;
                    model.CompanyTypeId = 2;
                    model.IsDeleted = false;
                    company.Insert(model);
                }
            }
            return Redirect("/dealer/details/" + company.Company.CompanyId);
        }

    }
}