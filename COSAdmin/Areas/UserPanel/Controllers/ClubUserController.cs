using COSAdmin.Models;
using COSAdmin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace COSAdmin.Areas.UserPanel.Controllers
{
    public class ClubUserController : Controller
    {
        DBEntities db = new DBEntities();

        public ActionResult SelectClub()
        {
            List<ClubMaster> clubMasters = new List<ClubMaster>();

            try
            {
                using (db = new DBEntities())
                {
                    clubMasters = db.ClubMasters.ToList();
                }
            }
            catch
            {

            }
            return View(clubMasters);
        }
    }
}