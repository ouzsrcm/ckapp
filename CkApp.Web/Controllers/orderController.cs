using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Library;
using CkApp.Web.Models;
using CkApp.Web.Models.Ordering;

namespace CkApp.Web.Controllers
{
    public class orderController : baseController
    {
        MOrder order;
        MOrderList list;

        public ActionResult index()
        {
            list = new MOrderList(AppId);
            list.GetOrders(Company.CompanyId);
            return View("index", list);
        }

        public ActionResult details()
        {
            if (Id != 0)
            {
                order = new MOrder();
                order.GetOrder(Id);
                if (order.Order == null)
                {
                    return Redirect("/order");
                }
                else return View("details", order);
            }
            else return Redirect("/order");
        }

        public ActionResult search(FormCollection form)
        {
            if (form.Keys.Count > 0)
            {
                int orderid = ((form["OrderId"] != null) ? int.Parse(form["OrderId"].ToString()) : 0);
                if (orderid != 0)
                    return Redirect("/order/details/" + orderid.ToString());
                else return Redirect("/order");
            }
            else return Redirect("/order");
        }

        public ActionResult create()
        {
            order = new MOrder();
            order.LoadCompanies(Company.CompanyId);
            order.Country.All();
            if (Id != 0)
            {
                order.GetOrder(Id);
                return View("create", order);
            }
            else return View(order);
        }

        public ActionResult editline()
        {
            if (Id != 0)
            {
                order = new MOrder();
                order.LoadCompanies(Company.CompanyId);
                order.Country.All();
                order.Country.Selected(((int)order.OrderDetail.DistrictId));
                order.GetOrderDetail(Id);
                return View("editline", order);
            }
            else return Redirect("/order");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult create(Order model)
        {
            if (ModelState.IsValid)
            {
                order = new MOrder();
                model.CreationDate = DateTime.Now;
                model.IsActive = true;
                order.Create(model);
                return Redirect("/order/create/" + order.OrderId);
            }
            else return Redirect("/order/create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult publish(OrderDetail model)
        {
            if (ModelState.IsValid)
            {
                order = new MOrder();
                model.CreationDate = DateTime.Now;
                order.CreateDetail(model);
                return Redirect("/order/details/" + model.OrderId);
            }
            else return Redirect("/order/create/" + model.OrderId);
        }

        public ActionResult old()
        {
            list = new MOrderList(AppId);
            list.PersonId = Person.PersonId;
            list.GetOldOrders(Company.CompanyId);
            return View(list);
        }

    }
}