using COSAdmin.Helpers;
using COSAdmin.Models;
using COSAdmin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace COSAdmin.Controllers
{
    public class ClubServiceController : Controller
    {
        DBEntities db = new DBEntities();

        public ActionResult ViewClub()
        {
            List<ClubMaster> clubMasters = new List<ClubMaster>();

            try
            {
                using (db = new DBEntities())
                {
                    clubMasters = db.ClubMasters.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }


            return View(clubMasters);
        }
    }
}