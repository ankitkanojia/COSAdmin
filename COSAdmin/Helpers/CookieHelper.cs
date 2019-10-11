using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COSAdmin.Helpers
{
    public class CookieHelper
    {
        public static void SetCookie(string key, string value, int expiresHour)
        {
            try
            {
                if (HttpContext.Current.Request.Cookies[key] != null)
                {
                    var cookieOld = HttpContext.Current.Request.Cookies[key];
                    cookieOld.Expires = DateTime.Now.AddHours(expiresHour);
                    cookieOld.Value = value;
                    HttpContext.Current.Response.Cookies.Add(cookieOld);
                }
                else
                {
                    HttpCookie cookie = new HttpCookie(key);
                    cookie.Value = value;
                    cookie.Expires = DateTime.Now.AddHours(expiresHour);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static string GetCookie(string key)
        {
            string value = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    if (HttpContext.Current.Request.Cookies[key] != null)
                    {
                        HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(key);
                        return cookie.Value;
                    }
                }
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static void RemoveCookie(string key)
        {
            try
            {
                if (HttpContext.Current.Request.Cookies[key] != null)
                {
                    HttpCookie cookie = new HttpCookie(key);
                    cookie.Expires = DateTime.Now.AddDays(-1d);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void RemoveCookieAll()
        {
            try
            {
                string[] allCookie = HttpContext.Current.Request.Cookies.AllKeys;

                foreach (string cookie in allCookie)
                {
                    HttpContext.Current.Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1d);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}