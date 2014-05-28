using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Upholsterer.Models;
using System.Configuration;
using System.Web.Configuration;

namespace Upholsterer.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }

        public JsonResult GetImg()
        {
            return Json("<img src='../../Content/Photos/cop.jpg' />");
        }
    }
}
