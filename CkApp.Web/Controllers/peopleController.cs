using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Library;
using CkApp.Web.Models.Utilities;
using CkApp.Web.Models.PeopleManagement;
using CkApp.Web.Models.CompanyManagement;

namespace CkApp.Web.Controllers
{
    public class peopleController : baseController
    {

        public Upload upload;

        public MPeople people;
        public MPeopleList list;
        public MPeopleCreate createperson;

        public MCompanyList companylist;

        public ActionResult me()
        {
            people = new MPeople();
            people.Get(Person.PersonId);
            people.GetOrder(Person.PersonId);
            people.GetCredential(Person.PersonId);
            return View("profile", people);
        }

        public ActionResult index()
        {
            list = new MPeopleList(AppId);
            list.List(Company.CompanyId);
            return View(list);
        }

        public ActionResult profile()
        {
            if (Id != 0)
            {
                people = new MPeople();

                people.Get(Id);
                people.GetOrder(Id);
                people.GetCredential(Id);

                people.PeopleRole.Load();

                companylist = new MCompanyList();
                companylist.AppId = AppId;
                companylist.GetDealers(((int)Company.CompanyId));

                ViewBag.Companies = companylist.Companies;

                return View(people);
            }
            else return Redirect("/people");
        }

        public ActionResult create()
        {
            people = new MPeople();
            createperson = new MPeopleCreate();
            if (Id != 0)
            {
                people.Get(Id);
                createperson.Person = people.Person;
                people.GetCredential(people.Person.PersonId);
                createperson.Credential = people.Credential;
            }
            return View("create", createperson);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult create(Person model)
        {
            if (ModelState.IsValid)
            {
                people = new MPeople();
                model.CreationDate = DateTime.Now;
                model.CreatedPersonId = Person.PersonId;
                people.Insert(model);
                return Redirect("/people/create/" + people.Person.PersonId.ToString());
            }
            else return Redirect("/people/create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createcredential(PersonCredential model)
        {
            if (ModelState.IsValid)
            {
                people = new MPeople();
                model.CreationDate = DateTime.Now;
                model.CreatedPersonId = Person.PersonId;
                people.InsertCredential(model);
                return Redirect("/people/profile/" + model.PersonId);
            }
            else return Redirect("/people/create/" + model.PersonId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult saveinfo(Person model, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {

                if (Id == 0) return Redirect("/people");

                model.PersonId = Id;

                if (file != null)
                {
                    upload = new Upload(file);
                    upload.Init();
                    upload.SaveMappath = Server.MapPath(upload.FolderUrl);
                    upload.Do();

                    model.Image = upload.FolderUrl;
                }

                people = new MPeople();
                people.Update(model, ((file != null) ? true : false));

                if (file != null)
                    upload.DeleteFile(people.Person.Image);

                return Redirect("/people/profile/" + Id.ToString());

            }
            else return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult savecredential(PersonCredential model)
        {

            if (ModelState.IsValid)
            {
                people = new MPeople();
                people.UpdateCredential(model);
                return Redirect("/people/profile/" + model.PersonId);
            }
            else return Redirect("/person/profile/" + model.PersonId);

        }

        public ActionResult logoff()
        {

            Session.Abandon();
            return Redirect("/login");

        }

    }
}