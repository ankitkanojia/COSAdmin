using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace COSAdmin.Areas.UserPanel.Controllers
{
    public class HomeUserController : Controller
    {
        // GET: UserPanel/HomeUser
        public ActionResult Index()
        {
            return View();
        }
    }
}