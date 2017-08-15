using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rapid.Web.Controllers
{
    public class ManifestController : Controller
    {
        // GET: Manifest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Confirm()
        {
            return View();
        }
    }
}