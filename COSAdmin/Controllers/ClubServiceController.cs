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

        [HttpGet]
        public ActionResult AddGallery(long id = 0)
        {
            GallaryMaster gallaryMaster = new GallaryMaster();
            try
            {
                if (id > 0)
                {
                    gallaryMaster = db.GallaryMasters.Where(s => s.GallaryMasterID == id).FirstOrDefault();
                }

            }
            catch (Exception e)
            {
                throw;
            }
            return View(gallaryMaster);
        }

        [HttpPost]
        public ActionResult AddGallery(GallaryMaster data, HttpPostedFileBase GallaryImg)
        {
            try
            {
                using (db = new DBEntities())
                {
                    if (data.GallaryMasterID > 0)
                    {
                        //Update
                        GallaryMaster gallaryMaster = new GallaryMaster();
                        gallaryMaster = db.GallaryMasters.Where(s => s.GallaryMasterID == data.GallaryMasterID).FirstOrDefault();
                        if (gallaryMaster != null)
                        {
                            if (Profile != null)
                            {
                                gallaryMaster.Image = Utilities.SaveImage(GallaryImg, "~/Upload/Gallery/");

                            }
                            gallaryMaster.Image = data.Image;
                            gallaryMaster.IsActive = data.IsActive;
                            gallaryMaster.UpdatedDate = data.UpdatedDate;
                            db.Entry(gallaryMaster).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        //Add
                        GallaryMaster gallaryMaster = new GallaryMaster();
                        gallaryMaster.Image = Utilities.SaveImage(GallaryImg, "~/Upload/Gallery/");
                        gallaryMaster.GallaryMasterID = data.GallaryMasterID;
                        gallaryMaster.ClubServiceID = data.ClubServiceID;
                        gallaryMaster.CreatedDate = DateTime.Now;
                        gallaryMaster.IsActive = data.IsActive;
                        db.GallaryMasters.Add(gallaryMaster);
                        db.SaveChanges();
                    }
                }

            }
            catch (Exception e)
            {
                throw;
            }

            return View();
        }

        public JsonResult deleteGallary(long GallaryMasterID = 0)
        {
            GallaryMaster gallaryMaster = new GallaryMaster();
            try
            {
                using (db = new DBEntities())
                {
                    gallaryMaster = db.GallaryMasters.Where(s => s.GallaryMasterID == GallaryMasterID).FirstOrDefault();
                    db.GallaryMasters.Remove(gallaryMaster);
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

        #region --> Club Service Category

        public ActionResult ViewServiceCategory()
        {
            List<ServiceCategory> serviceCategories = new List<ServiceCategory>();
            try
            {
                using (db = new DBEntities())
                {
                    serviceCategories = db.ServiceCategories.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return View(serviceCategories);
        }

        public ActionResult AddServiceCategory(long id = 0)
        {
            ServiceCategory serviceCategory = new ServiceCategory();
            try
            {
                using (db = new DBEntities())
                {
                    if (id > 0)
                    {
                        serviceCategory = db.ServiceCategories.Where(s => s.ServiceCategoryID == id).FirstOrDefault();
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return View(serviceCategory);
        }

        [HttpPost]
        public ActionResult AddServiceCategory(ServiceCategory data)
        {
            ServiceCategory serviceCategory = new ServiceCategory();

            try
            {
                using (db = new DBEntities())
                {
                    if (data.ServiceCategoryID > 0)
                    {
                        //Update HERE
                        serviceCategory = db.ServiceCategories.Where(s => s.ServiceCategoryID == data.ServiceCategoryID).FirstOrDefault();
                        serviceCategory.CategoryType = data.CategoryType;
                        serviceCategory.IsActive = data.IsActive;
                        db.Entry(serviceCategory).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                    else
                    {
                        //Add HERE
                        serviceCategory.CategoryType = data.CategoryType;
                        serviceCategory.CreatedDate = DateTime.Now;
                        serviceCategory.IsActive = data.IsActive;
                        db.ServiceCategories.Add(serviceCategory);
                        db.SaveChanges();
                    }

                }
            }
            catch (Exception e)
            {
                throw;
            }
            return View();
        }

        public JsonResult DeleteServiceCategory(long ServiceCategoryID = 0)
        {

            ServiceCategory serviceCategory = new ServiceCategory();
            try
            {
                using (db = new DBEntities())
                {
                    serviceCategory = db.ServiceCategories.Where(s => s.ServiceCategoryID == ServiceCategoryID).FirstOrDefault();

                    db.ServiceCategories.Remove(serviceCategory);
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

        #region --> Club Services

        public ActionResult ViewClubService()
        {
            List<ViewClubServiceVM> viewClubServiceVMs = new List<ViewClubServiceVM>();

            try
            {
                using (db = new DBEntities())
                {
                    viewClubServiceVMs = db.ClubServices.Select(s => new ViewClubServiceVM
                    {
                        ClubServiceID = s.ClubServiceID,
                        CategoryType = db.ServiceCategories.Find(s.ClubServiceID).CategoryType,
                        ServiceName = s.ServiceName,
                        CreatedDate = s.CreatedDate,
                        IsActive = s.IsActive,
                        IsPaid = s.IsPaid

                    }).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }


            return View(viewClubServiceVMs);
        }

        public ActionResult AddClubService()
        {

            return View((new ClubService()));
        }

        [HttpPost]
        public ActionResult AddClubService(ClubService data)
        {
            try
            {
                using (db = new DBEntities())
                {
                    ClubService clubService = new ClubService();
                    clubService.ServiceName = data.ServiceName;
                    clubService.ServiceCategoryID = data.ServiceCategoryID;
                    clubService.CreatedDate = DateTime.Now;
                    clubService.ClubMasterID = data.ClubMasterID;
                    clubService.IsActive = data.IsActive;
                    clubService.IsPaid = data.IsPaid;
                    db.ClubServices.Add(clubService);
                    db.SaveChanges();
                }

            }
            catch (Exception e)
            {
                throw;
            }

            return View();
        }

        public JsonResult deleteClubService(long ClubServiceID = 0)
        {
            ClubService clubService = new ClubService();
            try
            {
                using (db = new DBEntities())
                {
                    clubService = db.ClubServices.Where(s => s.ClubServiceID == ClubServiceID).FirstOrDefault();
                    db.ClubServices.Remove(clubService);
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

        #region --> Coach Schedule

        public ActionResult ViewCoachSchedule()
        {
            List<ViewCoachScheduleVM> viewCoachScheduleVMs = new List<ViewCoachScheduleVM>();

            try
            {
                using (db = new DBEntities())
                {
                    viewCoachScheduleVMs = db.CoachSchedules.Select(s => new ViewCoachScheduleVM
                    {
                        CreatedDate = s.CreatedDate,
                        EndTime = s.EndTime,
                        FirstName = db.CoachMasters.Find(s.CoachMasterID).FirstName,
                        IsActive = s.IsActive,
                        LastName = db.CoachMasters.Find(s.CoachMasterID).LastName,
                        StartTime = s.StartTime,
                        CoachScheduleID = s.CoachScheduleID
                    }).ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }


            return View(viewCoachScheduleVMs);
        }

        [HttpGet]
        public ActionResult AddCoachSchedule(long id = 0)
        {
            CoachSchedule coachSchedule = new CoachSchedule();
            try
            {
                using (db = new DBEntities())
                {


                    if (id > 0)
                    {
                        coachSchedule = db.CoachSchedules.Where(s => s.CoachScheduleID == id).FirstOrDefault();
                    }
                    else
                    {
                        long UserID = Convert.ToInt64(CookieHelper.GetCookie("UserID"));

                        coachSchedule = db.CoachSchedules.Where(s => s.CoachMasterID == UserID).FirstOrDefault();
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return View(coachSchedule);
        }

        [HttpPost]
        public ActionResult AddCoachSchedule(CoachSchedule data)
        {
            try
            {
                using (db = new DBEntities())
                {
                    if (data.CoachScheduleID > 0)
                    {
                        //Update
                        CoachSchedule coachSchedule = new CoachSchedule();
                        coachSchedule = db.CoachSchedules.Where(s => s.CoachScheduleID == data.CoachScheduleID).FirstOrDefault();
                        if (coachSchedule != null)
                        {
                            coachSchedule.StartTime = data.StartTime;
                            coachSchedule.EndTime = data.EndTime;
                            coachSchedule.IsActive = data.IsActive;
                            coachSchedule.UpdatedDate = data.UpdatedDate;
                            coachSchedule.CoachMasterID = Convert.ToInt64(CookieHelper.GetCookie("UserID"));
                            db.Entry(coachSchedule).State = EntityState.Modified;
                            db.SaveChanges();
                        }

                    }
                    else
                    {
                        //Add
                        CoachSchedule coachSchedule = new CoachSchedule();
                        coachSchedule.StartTime = data.StartTime;
                        coachSchedule.EndTime = data.EndTime;
                        coachSchedule.IsActive = data.IsActive;
                        coachSchedule.CoachMasterID = Convert.ToInt64(CookieHelper.GetCookie("UserID"));
                        coachSchedule.CreatedDate = DateTime.Now;
                        db.CoachSchedules.Add(coachSchedule);
                        db.SaveChanges();
                    }

                }

            }
            catch (Exception e)
            {
                throw;
            }
            return View();
        }

        public JsonResult deleteCoachSchedule(long CoachScheduleID = 0)
        {
            CoachSchedule coachSchedule = new CoachSchedule();
            try
            {
                using (db = new DBEntities())
                {
                    coachSchedule = db.CoachSchedules.Where(s => s.CoachScheduleID == CoachScheduleID).FirstOrDefault();
                    db.CoachSchedules.Remove(coachSchedule);
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
    }
}