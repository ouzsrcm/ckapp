using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CkApp.Web.Models.CompanyManagement
{
    public class MCompanyList : MCompany
    {

        public IList<Company> Companies;

        public void GetCompanies(int appid)
        {
            Companies = db.Companies.Where(x => x.AppId == appid).ToList();
        }

        public void GetDealers(int companyid)
        {
            this.Companies = db.Companies.Where(x => x.ParentCompanyId == companyid
                        && x.AppId == AppId).ToList();
        }

    }
}