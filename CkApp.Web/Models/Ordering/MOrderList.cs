using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Library;

namespace CkApp.Web.Models.Ordering
{
    public class MOrderList : MOrder
    {

        public int PersonId { get; set; }

        public IList<Order> AllOrders;

        public MOrderList(int appid)
        {
            this.AppId = appid;
        }

        public void GetOrders()
        {
            this.AllOrders = db.Orders.Where(x => x.AppId == AppId).OrderByDescending(x => x.CreationDate).ToList();
        }

        public void GetOrders(int companyid)
        {
            this.AllOrders = (from x in db.Orders
                              join y in db.OrderDetails on x.OrderId equals y.OrderId
                              where
                                x.AppId == AppId &&
                                y.CompanyId == companyid
                              select x).ToList();
        }

        public void GetOldOrders(int companyid)
        {
            this.AllOrders = (from x in db.Orders
                              where x.CompanyId == companyid && x.PersonId == PersonId
                              select x).OrderByDescending(x => x.CreationDate).ToList();
        }

        public void GetOldOrdersByCompany(int companyid)
        {
            this.AllOrders = (from x in db.Orders
                              where x.CompanyId == companyid
                              select x).OrderByDescending(x => x.CreationDate).ToList();
        }

    }
}