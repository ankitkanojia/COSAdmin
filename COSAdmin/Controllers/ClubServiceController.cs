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

        #region --> Club

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

        public ActionResult AddClub(long id = 0)
        {
            ClubMaster clubMaster = new ClubMaster();
            try
            {
                using (db = new DBEntities())
                {
                    if (id > 0)
                    {
                        clubMaster = db.ClubMasters.Where(s => s.ClubMasterID == id).FirstOrDefault();
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return View(clubMaster);
        }

        [HttpPost]
        public ActionResult AddClub(ClubMaster data, HttpPostedFileBase ClubImage)
        {
            ClubMaster clubMaster = new ClubMaster();

            try
            {
                using (db = new DBEntities())
                {
                    if (data.ClubMasterID > 0)
                    {
                        //Update HERE
                        clubMaster = db.ClubMasters.Where(s => s.ClubMasterID == data.ClubMasterID).FirstOrDefault();
                        if (clubMaster != null)
                        {
                            if (ClubImage != null)
                            {
                                clubMaster.ClubImage = Utilities.SaveImage(ClubImage, "~/Upload/Club/");
                            }
                            clubMaster.ClubName = data.ClubName;
                            clubMaster.UpdatedDate = DateTime.Now;
                            db.Entry(clubMaster).State = EntityState.Modified;
                            db.SaveChanges();

                        }

                    }
                    else
                    {
                        //Add HERE
                        if (ClubImage != null)
                        {
                            data.ClubImage = Utilities.SaveImage(ClubImage, "~/Upload/Club/");
                        }
                        else
                        {
                            data.ClubImage = string.Empty;
                        }
                        data.CreatedDate = DateTime.Now;
                        db.ClubMasters.Add(data);
                        db.SaveChanges();
                    }

                }
            }
            catch (Exception e)
            {
                throw;
            }
            return RedirectToAction("ViewClub");
        }

        public JsonResult DeleteClub(long ClubMasterID = 0)
        {

            ClubMaster clubMaster = new ClubMaster();
            try
            {
                using (db = new DBEntities())
                {
                    clubMaster = db.ClubMasters.Where(s => s.ClubMasterID == ClubMasterID).FirstOrDefault();

                    db.ClubMasters.Remove(clubMaster);
                    db.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region --> AddGallary

        public ActionResult ViewGallary()
        {
            List<ViewGallaryMasterVM> viewGallaryMasterVMs = new List<ViewGallaryMasterVM>();

            try
            {
                using (db = new DBEntities())
                {
                    viewGallaryMasterVMs = db.GallaryMasters.Select(s => new ViewGallaryMasterVM
                    {
                        ServiceName = db.ClubServices.Find(s.ClubServiceID).ServiceName,
                        Image = s.Image,
                        CreatedDate = s.CreatedDate,
                        IsActive = s.IsActive,
                        GallaryMasterID = s.GallaryMasterID
                    }).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }


            return View(viewGallaryMasterVMs);
        }

        #endregion
    }
}