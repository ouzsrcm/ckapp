using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Library;
using CkApp.Web.Models.Authentication;

namespace CkApp.Web.Controllers
{
    public class loginController : Controller
    {

        public MLogin login;

        public ActionResult index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult index(MLogin model)
        {
            login = new MLogin();
            if (ModelState.IsValid)
            {

                login.Username = model.Username;
                login.Password = model.Password;

                login.Do();

                if (login.Status)
                {
                    Session["Person"] = login.Person;
                    return Redirect("/home");
                }

            }
            else
            {
                login.Warning.Set(true, "Lütfen verileri doğru girin.", "danger", "", false);
            }

            return View("index", login);

        }

    }
}