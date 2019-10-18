using COSAdmin.Helpers;
using COSAdmin.Models;
using COSAdmin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace COSAdmin.Areas.UserPanel.Controllers
{
    public class HomeUserController : Controller
    {
        DBEntities db = new DBEntities();

        // GET: UserPanel/HomeUser
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserMaster data)
        {
            using (db = new DBEntities())
            {
                try
                {
                    //I am admin
                    var userData = db.UserMasters.Where(s => s.Mobile == data.Mobile && s.Password == data.Password && s.RoleID == 3).FirstOrDefault();

                    if (userData != null)
                    {
                        if (userData.IsPaid)
                        {
                            //Logn Successfull
                            CookieHelper.SetCookie("UserID", userData.UserMasterID.ToString(), 6);
                            SignInRemember(data.Mobile, true);
                            return RedirectToAction("Index", "HomeUser");
                        }
                        else
                        {
                            //Transaction Failed
                            return RedirectToAction("Payment", "HomeUser", new { id = userData.UserMasterID });
                        }

                    }
                    else
                    {
                        //Login Failed
                        return RedirectToAction("Login", "Home");
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }

        }

        [HttpGet]
        public ActionResult Logout()
        {
            try
            {
                // First we clean the authentication ticket like always
                //required NameSpace: using System.Web.Security;
                FormsAuthentication.SignOut();

                // Second we clear the principal to ensure the user does not retain any authentication
                //required NameSpace: using System.Security.Principal;
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

                Session.Clear();
                System.Web.HttpContext.Current.Session.RemoveAll();
                CookieHelper.RemoveCookie("BackEndMasterID");


                // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
                // this clears the Request.IsAuthenticated flag since this triggers a new request
                return RedirectToAction("Login");
            }
            catch
            {
                throw;
            }
        }

        private void SignInRemember(string userName, bool isPersistent = false)
        {
            // Clear any lingering authencation data
            FormsAuthentication.SignOut();

            // Write the authentication cookie
            FormsAuthentication.SetAuthCookie(userName, isPersistent);
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(UserMaster data)
        {
            try
            {
                using (db = new DBEntities())
                {
                    data.DOB = DateTime.Now;
                    data.IsActive = true;
                    data.CreatedDate = DateTime.Now;
                    data.RoleID = 3;
                    db.UserMasters.Add(data);
                    db.SaveChanges();

                    return RedirectToAction("Payment", "HomeUser", new { id = data.UserMasterID });
                }
            }
            catch
            {
                throw;
            }
        }
    }
}