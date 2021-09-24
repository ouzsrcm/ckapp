using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CkApp.Web.Models.Utilities
{
    public class Warning : MBase
    {

        public bool Status { get; set; }

        public string Body { get; set; }

        public string Css { get; set; }

        public string Url { get; set; }

        public bool IsRedirect { get; set; }


        public void Set(bool status = false, string body = "", string css = "", string url = "", bool isredirect = false)
        {
            this.Status = status;
            this.Body = body;
            this.Css = css;
            this.Url = url;
            this.IsRedirect = isredirect;
        }

    }
}