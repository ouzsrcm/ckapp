using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CkApp.Web.Controllers
{
    public class homeController : baseController
    {
        public ActionResult index()
        {
            return View();
        }
    }
}