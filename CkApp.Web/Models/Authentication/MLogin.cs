using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Library;
using CkApp.Web.Models.Utilities;

namespace CkApp.Web.Models.Authentication
{
    public class MLogin : PersonCredential
    {

        public Warning Warning { get; set; }

        public bool Status { get; set; }

        public AppEntities db = new AppEntities();

        public MLogin()
        {
            Warning = new Warning();
        }

        public void Do()
        {

            try
            {

                if (Username != "" && Password != "")
                {

                    if (Username.Trim().Length > 1 && Password.Trim().Length > 1)
                    {

                        var _credential = (from x in db.PersonCredentials
                                           where
                                                x.Username == Username &&
                                                x.Password == Password &&
                                                x.IsDeleted == false &&
                                                x.IsActive == true
                                           select x).FirstOrDefault();

                        if (_credential != null)
                        {

                            this.Person = db.People.Where(x => x.PersonId == _credential.PersonId && x.IsDeleted == false).FirstOrDefault();

                            if (this.Person != null)
                            {

                                Status = true;

                            }
                            else
                            {
                                Warning.Set(true, "Sistemde tanımlı böyle bir kullanıcı bulunamadı.", "danger", "", false);
                                Status = false;
                            }

                        }
                        else
                        {
                            Warning.Set(true, "Sistemde tanımlı böyle bir kullanıcı bulunamadı.", "danger", "", false);
                            Status = false;
                        }

                    }
                    else
                    {
                        Warning.Set(true, "Lütfen doğru bilgiler girin.", "danger", "", false);
                        Status = false;
                    }

                }
                else
                {
                    Warning.Set(true, "Kullanıcı adı veya parola alanları boş bırakılamaz.", "danger", "", false);
                    Status = false;
                }

            }
            catch (Exception ex)
            {
                Warning.Set(true, ex.Message, "danger", "", false);
                Status = false;
            }

        }

    }
}