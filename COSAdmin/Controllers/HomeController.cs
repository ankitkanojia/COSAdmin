using COSAdmin.Helpers;
using COSAdmin.Models;
using COSAdmin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
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

        [HttpPost]
        public ActionResult Registration(CoachMaster data, HttpPostedFileBase CoachCertificate)
        {
            try
            {
                using (db = new DBEntities())
                {
                    CoachMaster reg = new CoachMaster();
                    if (CoachCertificate != null)
                    {
                        reg.CoachCertificate = Utilities.SaveImage(CoachCertificate, "~/Upload/CoachCertificate/");

                    }
                    reg.Mobile = data.Mobile;
                    reg.Password = data.Password;
                    reg.FirstName = data.FirstName;
                    reg.LastName = data.LastName;
                    reg.CityMasterID = data.CityMasterID;
                    reg.ClubMasterID = data.ClubMasterID;
                    reg.Address = data.Address;
                    reg.CreatedDate = DateTime.Now;
                    reg.TraineeCharge = data.TraineeCharge;
                    reg.IsActive = true;
                    db.CoachMasters.Add(reg);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region --> Forgot Password

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string Mobile)
        {
            try
            {
                using (db = new DBEntities())
                {
                    //Validate Mobile Number
                    var data = db.UserMasters.Where(s => s.Mobile == Mobile).FirstOrDefault();

                    if (data == null)
                    {
                        //Mobile Number does not exists in our system
                        return View();
                    }

                    //Generate OTP
                    string OTPCode = Utilities.GenerateRandomString(6);
                    string UniqueCode = Utilities.GenerateRandomString(26);

                    //Save OTP
                    OTP oTP = new OTP();
                    oTP.CreatedDate = DateTime.Now;
                    oTP.OTPCode = OTPCode;
                    oTP.UniqueCode = UniqueCode;
                    db.OTPs.Add(oTP);
                    db.SaveChanges();

                    //Send OTP
                    EmailAndSMS.SendOTPSMS(Mobile, OTPCode);

                    TempData["UniqueCode"] = UniqueCode;
                    TempData["Mobile"] = Mobile;
                    return RedirectToAction("ResetPassword", "Home");
                }
            }
            catch
            {
                throw;
            }

        }

        #endregion

        #region --> Reset Password 

        public ActionResult ResetPassword()
        {
            ResetPWDVM resetPWDVM = new ResetPWDVM();

            try
            {

                if (TempData["UniqueCode"] != null && TempData["Mobile"] != null)
                {
                    resetPWDVM.UniqueCode = TempData["UniqueCode"].ToString();
                    resetPWDVM.Mobile = TempData["Mobile"].ToString();
                }
                else
                {
                    return RedirectToAction("ForgotPassword");
                }

                return View(resetPWDVM);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPWDVM data)
        {

            UserMaster userMaster = new UserMaster();


            try
            {
                //Mobile Check
                userMaster = db.UserMasters.Where(s => s.Mobile == data.Mobile).FirstOrDefault();

                if (userMaster == null)
                {
                    //Mobile Number does not exists in our system
                    return View();
                }

                //OTP Check
                var OTPData = db.OTPs.Where(s => s.OTPCode == data.OTP && s.UniqueCode == data.UniqueCode).FirstOrDefault();

                if (OTPData == null)
                {
                    //Invalid OTP / Wrong OTP
                    return View();
                }

                if (OTPData.CreatedDate.AddMinutes(15) < DateTime.Now)
                {
                    //OTP Expired
                    return View();
                }

                //Reset Password
                userMaster.Password = data.Password;
                db.Entry(userMaster).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Login", "Home");
            }
            catch
            {
                throw;
            }


        }

        #endregion

        #region --> Enquiry

        public ActionResult ViewEnquiry()
        {
            List<Enquiry> enquiries = new List<Enquiry>();

            try
            {
                using (db = new DBEntities())
                {

                    enquiries = db.Enquiries.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }


            return View(enquiries);
        }

        #endregion

        #region --> Sign out 

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