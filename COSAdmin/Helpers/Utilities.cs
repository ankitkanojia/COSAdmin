﻿using COSAdmin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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