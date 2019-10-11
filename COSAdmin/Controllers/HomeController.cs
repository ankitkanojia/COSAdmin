using COSAdmin.Helpers;
using COSAdmin.Models;
using COSAdmin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace COSAdmin.Controllers
{
    public class HomeController : Controller
    {
        DBEntities db = new DBEntities();

        #region --> Coach Login

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM data)
        {
            using (db = new DBEntities())
            {
                try
                {
                    if (data.IsAdmin)
                    {
                        //I am admin
                        var userData = db.UserMasters.Where(s => s.Mobile == data.Mobile && s.Password == data.Password).FirstOrDefault();
                        if (userData != null)
                        {
                            //Logn Successfull

                            CookieHelper.SetCookie("UserID", userData.UserMasterID.ToString(), 6);
                            SignInRemember(data.Mobile, true);

                            return RedirectToAction("AdminDashboard", "Dashboard");
                        }
                        else
                        {
                            //Login Failed
                            return RedirectToAction("Login", "Home");
                        }
                    }
                    else
                    {
                        //I am coach
                        var coachData = db.CoachMasters.Where(s => s.Mobile == data.Mobile && s.Password == data.Password).FirstOrDefault();

                        if (coachData != null)
                        {
                            //Logn Successfull

                            CookieHelper.SetCookie("UserID", coachData.CouchMasterID.ToString(), 6);
                            SignInRemember(data.Mobile, true);
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else
                        {
                            //Login Failed
                            return RedirectToAction("Login", "Home");
                        }
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }

        }

        #endregion

        #region --> Coach Registration

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        #endregion

        private void SignInRemember(string userName, bool isPersistent = false)
        {
            // Clear any lingering authencation data
            FormsAuthentication.SignOut();

            // Write the authentication cookie
            FormsAuthentication.SetAuthCookie(userName, isPersistent);
        }
    }
}