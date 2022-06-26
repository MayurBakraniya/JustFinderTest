using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JustFinderTest.Controllers
{
    public class HomeController : Controller
    {

        private JustFinderTestEntities1 db = new JustFinderTestEntities1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserIndex()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult UserAbout()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}