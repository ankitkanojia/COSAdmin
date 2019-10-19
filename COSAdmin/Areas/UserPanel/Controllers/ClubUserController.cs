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

        [HttpGet]
        public JsonResult GetTraineeCharges(long CouchMasterID)
        {
            using (db = new DBEntities())
            {
                var coachData = db.CoachMasters.Where(s => s.CouchMasterID == CouchMasterID).FirstOrDefault();

                return Json(new { charges = coachData.TraineeCharge }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetClubServices(long ServiceCategoryID)
        {
            using (db = new DBEntities())
            {
                var city = db.ClubServices.Where(s => s.ServiceCategoryID == ServiceCategoryID).ToList();

                return Json(new SelectList(city.ToArray(), "ClubServiceID", "ServiceName"), JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult GetCoachList(long ClubServiceID)
        {
            using (db = new DBEntities())
            {
                var city = db.CoachMasters.Where(s => s.ClubMasterID == ClubServiceID).ToList();

                return Json(new SelectList(city.ToArray(), "CouchMasterID", "FirstName"), JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult ClubService(CluubServicesVM data)
        {
            try
            {
                using (db = new DBEntities())
                {

                }
            }
            catch (Exception e)
            {

            }
            return View();
        }
    }
}