using COSAdmin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace COSAdmin.Helpers
{
    public class Utilities
    {
        public static List<SelectListItem> GetClubServices()
        {
            List<SelectListItem> lstCategory = new List<SelectListItem>();

            try
            {
                using (DBEntities db = new DBEntities())
                {
                    var data = db.ClubServices.Where(s => s.IsActive).ToList();

                    lstCategory = data.Select(s => new SelectListItem
                    {
                        Text = s.ServiceName,
                        Value = s.ClubServiceID.ToString()

                    }).ToList();
                }
            }
            catch
            {
                throw;
            }

            return lstCategory;
        }

        public static List<SelectListItem> GetClubs()
        {
            List<SelectListItem> lstCategory = new List<SelectListItem>();

            try
            {
                using (DBEntities db = new DBEntities())
                {
                    var data = db.ClubMasters.Where(s => s.IsActive).ToList();

                    lstCategory = data.Select(s => new SelectListItem
                    {
                        Text = s.ClubName,
                        Value = s.ClubMasterID.ToString()

                    }).ToList();
                }
            }
            catch
            {
                throw;
            }

            return lstCategory;
        }

        public static List<SelectListItem> GetCouchList()
        {
            List<SelectListItem> lstCategory = new List<SelectListItem>();

            try
            {
                using (DBEntities db = new DBEntities())
                {
                    SelectListItem single = new SelectListItem();
                    single.Value = "";
                    single.Text = "Select service coach";

                    lstCategory.Add(single);

                    var data = db.CoachMasters.Where(s => s.IsActive).ToList();

                    lstCategory.AddRange(data.Select(s => new SelectListItem
                    {
                        Text = s.FirstName + " " + s.LastName,
                        Value = s.CouchMasterID.ToString()

                    }).ToList());
                }
            }
            catch
            {
                throw;
            }

            return lstCategory;
        }

        public static List<SelectListItem> GetServiceCategory()
        {
            List<SelectListItem> lstCategory = new List<SelectListItem>();

            try
            {
                using (DBEntities db = new DBEntities())
                {
                    var data = db.ServiceCategories.Where(s => s.IsActive).ToList();

                    lstCategory = data.Select(s => new SelectListItem
                    {
                        Text = s.CategoryType,
                        Value = s.ServiceCategoryID.ToString()

                    }).ToList();
                }
            }
            catch
            {
                throw;
            }

            return lstCategory;
        }

        public static List<SelectListItem> GetCity()
        {
            List<SelectListItem> lstCity = new List<SelectListItem>();

            try
            {
                using (DBEntities db = new DBEntities())
                {
                    var data = db.CityMasters.ToList();

                    lstCity = data.Select(s => new SelectListItem
                    {
                        Text = s.CityName,
                        Value = s.CityMasterID.ToString()

                    }).ToList();
                }
            }
            catch
            {
                throw;
            }

            return lstCity;
        }

        public static List<SelectListItem> GetClubName()
        {
            List<SelectListItem> lstClubName = new List<SelectListItem>();

            try
            {
                using (DBEntities db = new DBEntities())
                {
                    var data = db.ClubMasters.Where(s => s.IsActive).ToList();

                    lstClubName = data.Select(s => new SelectListItem
                    {
                        Text = s.ClubName,
                        Value = s.ClubMasterID.ToString()

                    }).ToList();
                }
            }
            catch
            {
                throw;
            }

            return lstClubName;
        }

        public static List<SelectListItem> GetServiceCategoryList()
        {
            List<SelectListItem> lstCategory = new List<SelectListItem>();

            try
            {
                using (DBEntities db = new DBEntities())
                {
                    var data = db.ServiceCategories.Where(s => s.IsActive).ToList();
                    SelectListItem single = new SelectListItem();
                    single.Value = "";
                    single.Text = "Select service category";

                    lstCategory.Add(single);


                    lstCategory.AddRange(data.Select(s => new SelectListItem
                    {
                        Text = s.CategoryType,
                        Value = s.ServiceCategoryID.ToString()

                    }).ToList());
                }
            }
            catch
            {
                throw;
            }

            return lstCategory;
        }

        public static string SaveImage(HttpPostedFileBase file, string path)
        {
            try
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                file.SaveAs(HttpContext.Current.Server.MapPath(path + filename));
                return filename;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static bool SendOTPViaSMS(string mobile, string otp)
        {
            try
            {
                string username = "";
                string msg_token = "";
                string sender_id = "";

                string URL = "http://message.yukontechnologies.com/api/send_transactional_sms.php?username=" + username + "&msg_token=" + msg_token + "&sender_id=" + sender_id + "&message=Your One Time Password is " + otp + ". From Team WebCayon" + "&mobile=" + mobile;

                HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(URL);

                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                StreamReader sr = new StreamReader(httpres.GetResponseStream());
                string results = sr.ReadToEnd();
                //string finalresult = new string(results.Take(4).ToArray());
                sr.Close();

                return true;

            }
            catch
            {
                return false;
            }
        }

        public static string GenerateRandomString(int Length)
        {
            try
            {
                const string chars = "0123456789";

                var random = new Random();

                return new string(Enumerable.Repeat(chars, Length)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static bool SendEmail(string EmailId, string subject, string Body, string from)
        {
            try
            {
                bool fSSL = true;

                System.Web.Mail.MailMessage message = new System.Web.Mail.MailMessage();
                message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "smtp.gmail.com");
                message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2");
                message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "465");
                if (fSSL)
                    message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true");
                message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "*******@gmail.com"); //add your email
                message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "*******"); //add your key

                //Preparing the message object....

                if (string.IsNullOrWhiteSpace(from))
                {

                    message.From = "*******@gmail.com";
                }
                else
                {
                    message.From = from;
                }


                message.To = EmailId;
                message.Subject = subject;
                message.BodyFormat = System.Web.Mail.MailFormat.Html;


                message.Body = Body;
                System.Web.Mail.SmtpMail.SmtpServer = "smtp.gmail.com";
                System.Web.Mail.SmtpMail.Send(message);
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}