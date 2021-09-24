using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Library;
using CkApp.Web.Models.Utilities;

namespace CkApp.Web.Models.Ordering
{
    public class MOrder : MBase
    {

        public int AppId { get; set; }
        public int OrderId { get; set; }

        public Order Order;
        public OrderDetail OrderDetail;

        public IList<Company> Companies;

        public Person AuthorizedPerson;
        
        public MCountry Country = new MCountry();

        
        public void GetOrder(int orderid)
        {
            this.Order = db.Orders.Where(x => x.OrderId == orderid && x.IsActive == true).FirstOrDefault();
        }

        public void Create(Order model)
        {
            db.Orders.Add(model);
            db.SaveChanges();
            OrderId = model.OrderId;
        }

        public void CreateDetail(OrderDetail model)
        {
            db.OrderDetails.Add(model);
            db.SaveChanges();
        }

        public void GetOrderDetail(int id)
        {
            this.OrderDetail = db.OrderDetails.Where(x => x.OrderDetailId == id).FirstOrDefault();
        }

        public void LoadCompanies(int companyid) {
            var _company = db.Companies.Where(x => x.CompanyId == companyid).FirstOrDefault();
            if (_company != null)
            {
                this.Companies = db.Companies.Where(x => x.CompanyId == _company.ParentCompanyId).ToList();
                foreach (var item in Companies)
                {
                    this.AuthorizedPerson = db.People.Where(x => x.CompanyId == item.CompanyId && x.IsAuthorized == true).FirstOrDefault();
                }
            }
        }

    }
}