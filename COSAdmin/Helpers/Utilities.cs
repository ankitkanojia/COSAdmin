using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace COSAdmin.Helpers
{
    public class Utilities
    {
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
    }
}