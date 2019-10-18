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

        #region --> PayUMoney Payment Integration

        public ActionResult Payment(long id = 0)
        {
            PayuResponse payuResponse = new PayuResponse();
            UserMaster usermaster = new UserMaster();

            try
            {
                using (db = new DBEntities())
                {
                    usermaster = db.UserMasters.Where(s => s.UserMasterID == id).FirstOrDefault();

                    if (usermaster != null)
                    {
                        usermaster.IsPaid = true;
                        db.Entry(usermaster).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        payuResponse.tblPKID = usermaster.UserMasterID;
                        payuResponse.amount = "350";
                    }

                }
            }
            catch
            {
                throw;
            }

            return View(payuResponse);
        }

        [HttpPost]
        public void Payment(PayuResponse data)
        {

            try
            {
                RemotePost myremotepost = new RemotePost();
                string key = ConfigurationManager.AppSettings["MERCHANT_KEY"];
                string salt = ConfigurationManager.AppSettings["SALT"];
                string surl = ConfigurationManager.AppSettings["SUCC_URL"];
                string furl = ConfigurationManager.AppSettings["FAIL_URL"];
                string zipcode = "390025";
                string productInfo = "test";

                //posting all the parameters required for integration.
                myremotepost.Url = ConfigurationManager.AppSettings["PAYU_BASE_URL"];
                myremotepost.Add("key", key);
                string txnid = Guid.NewGuid().ToString().Substring(12);
                myremotepost.Add("txnid", txnid);
                myremotepost.Add("amount", data.amount);
                myremotepost.Add("productinfo", productInfo);
                myremotepost.Add("firstName", data.firstName);
                myremotepost.Add("lastname", data.lastname);
                myremotepost.Add("email", data.email);
                myremotepost.Add("phone", data.phone);
                myremotepost.Add("address1", data.address1);
                myremotepost.Add("address2", data.address2);
                myremotepost.Add("city", data.city);
                myremotepost.Add("state", data.state);
                myremotepost.Add("zipcode", zipcode);
                myremotepost.Add("surl", surl);
                myremotepost.Add("furl", furl);
                string hashString = key + "|" + txnid + "|" + data.amount + "|" + productInfo + "|" + data.firstName + "|" + data.email + "|||||||||||" + salt;
                string hash = Hashing.Generatehash512(hashString);
                myremotepost.Add("hash", hash);
                myremotepost.Post();
            }
            catch
            {
                throw;
            }
        }

        public ActionResult Success(PayuResponse data)
        {
            return View();
        }

        public ActionResult Fail(PayuResponse data)
        {
            return View();
        }

        #endregion
    }
}