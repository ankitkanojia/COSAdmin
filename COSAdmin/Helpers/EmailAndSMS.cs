using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;


namespace COSAdmin.Helpers
{
    public class EmailAndSMS
    {
        public static bool SendOTPSMS(string mobile, string OTP)
        {
            try
            {
                string username = "u450";
                string msg_token = "4EwoLK";
                string sender_id = "ANKIT";


                string Message = "http://message.yukontechnologies.com/api/send_transactional_sms.php?username=" + username + "&msg_token=" + msg_token + "&sender_id=" + sender_id + "&message=Your One Time Password is " + OTP + "Team WebCayon" + "&mobile=" + mobile;

                HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(Message);

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

        public static bool SendEmail(string EmailId, string subject, string Body, string from)
        {
            try
            {
                if (string.IsNullOrEmpty(from))
                {
                    from = WebConfigurationManager.AppSettings["EmailAddress"].ToString();
                }
                MailMessage mail = new MailMessage(from, EmailId, "", "");
                System.Net.Mime.ContentType mimeType = new System.Net.Mime.ContentType("text/html");

                mail.Subject = subject;
                mail.Body = Body;
                mail.Bcc.Add(WebConfigurationManager.AppSettings["BCCAddress"].ToString());
                mail.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                mail.IsBodyHtml = true;
                SmtpClient oSmtpClient = new SmtpClient();
                oSmtpClient.Host = WebConfigurationManager.AppSettings["SMPTHost"].ToString();
                oSmtpClient.Port = Convert.ToInt32(WebConfigurationManager.AppSettings["SMPTPort"]);
                oSmtpClient.Credentials = new System.Net.NetworkCredential(WebConfigurationManager.AppSettings["SMPTUserName"].ToString(), WebConfigurationManager.AppSettings["EmailPassword"].ToString());
                oSmtpClient.Send(mail);
                return true;



            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}