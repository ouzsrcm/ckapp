using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Library;
using CkApp.Web.Models.Utilities;

namespace CkApp.Web.Controllers
{
    public class baseController : Controller
    {

        public int Id { get; set; }
        public int AppId { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public Person Person;
        public Company Company;

        public AppEntities db = new AppEntities();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            __querystring();

            if (Session["Person"] == null)
            {
                filterContext.Result = new RedirectResult("/login");
                return;
            }

            Person = (Person)Session["Person"];

            if (Person == null)
            {
                Session.Abandon();
                filterContext.Result = new RedirectResult("/login");
                return;
            }

            Company = db.Companies.Where(x => x.CompanyId == Person.CompanyId).FirstOrDefault();

            ViewBag.CompanyTitle = Company.Title;

            App app = db.Apps.Where(x => x.AppId == AppId).FirstOrDefault();

            if (app == null)
            {
                Session.Abandon();
                filterContext.Result = new RedirectResult("/error");
                return;
            }

            ViewBag.AppId = app.AppId;
            ViewBag.CompanyId = Company.CompanyId;

            ViewBag.PersonId = Person.PersonId;

            Module module = db.Modules.Where(x => x.Name == ControllerName && x.AppId == app.AppId).FirstOrDefault();

            if (module != null)
            {

                ModuleMethod method = db.ModuleMethods.Where(x => x.Name == ActionName && x.ModuleId == module.ModuleId).FirstOrDefault();

                if (method != null)
                {

                    ModuleMethodHeader head = db.ModuleMethodHeaders.Where(x => x.ModuleMethodId == method.ModuleMethodId).FirstOrDefault();

                    if (head != null)
                    {

                        ViewBag.PersonFullname = Person.Firstname + " " + Person.Lastname;

                        ViewBag.Js = head.Js;
                        ViewBag.Css = head.Css;
                        ViewBag.HeadTitle = head.Title;
                        ViewBag.ModuleTitle = module.Title;
                        ViewBag.Keyword = head.Keyword;
                        ViewBag.Description = head.Description;
                        ViewBag.Charset = head.Charset;

                        MNavigation nav = new MNavigation(Person.PersonId, app.AppId);
                        nav.Prepare();

                        ViewBag.Navigation = nav.Html;

                    }
                    else
                    {
                        filterContext.Result = new RedirectResult("/error");
                        return;
                    }

                }
                else
                {
                    filterContext.Result = new RedirectResult("/error");
                    return;
                }

            }
            else
            {
                filterContext.Result = new RedirectResult("/error");
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        private void __querystring()
        {

            ControllerName = ControllerContext.RouteData.Values["controller"].ToString();
            ActionName = ControllerContext.RouteData.Values["action"].ToString();

            try
            {
                Id = int.Parse(ControllerContext.RouteData.Values["id"].ToString());
                AppId = int.Parse(System.Configuration.ConfigurationManager.AppSettings["AppId"].ToString());
            }
            catch
            {
                Id = 0;
                AppId = 1;
            }
        }
    }
}