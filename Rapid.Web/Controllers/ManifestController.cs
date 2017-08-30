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

        public ActionResult Pending()
        {
            //var listManifest = Rapid.Data.ManifestProvider.GetDataProvider(Rapid.Data.DATABASE_TYPE.MONGODB).FilterStandard(
            // String.Empty,
            //  String.Empty,
            //  String.Empty,
            //  String.Empty,
            //  null,
            //  null,
            //  null,
            //  null,
            //  "null");
            return View();
        }

        public ActionResult Confirm()
        {
            return View();
        }

        public ActionResult Accept()
        {
            return View();
        }
    }
}