using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace JustFinderTest.Controllers
{
    public class LoginController : Controller
    {

        private JustFinderTestEntities1 db = new JustFinderTestEntities1();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Loginuser()
        {
            Business_Owner owner = new Business_Owner();
            return View(owner);
        }

        [HttpPost]
        public ActionResult Loginuser(Business_Owner owner)
        {
            Business_Owner u = db.Business_Owner.Where(a => a.email == owner.email && a.phno == owner.phno).SingleOrDefault();

            if (u != null)
            {
                FormsAuthentication.SetAuthCookie(owner.email.ToString(), false);
                Session["busi_id"] = u.busi_id.ToString();
                Session["name"] = u.name.ToString();
                Session["email"] = u.email.ToString();
                Session["phno"] = u.phno.ToString();
                return RedirectToAction("UserIndex", "Home");
            }
            else
            {
                ViewBag.err = "Invalid email or Password";
            }
            return View();
        }



        public ActionResult AdminLogin()
        {
            Admin admin = new Admin();
            return View(admin);
        }

        [HttpPost]
        public ActionResult AdminLogin(Admin admin)
        {
            Admin u = db.Admins.Where(a => a.email == admin.email && a.password == admin.password).SingleOrDefault();

            if (u != null)
            {
                FormsAuthentication.SetAuthCookie(admin.email.ToString(), false);
                Session["admin_id"] = u.admin_id.ToString();
                Session["name"] = u.name.ToString();
                Session["email"] = u.email.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.err = "Invalid email or Password";
            }
            return View();
        }



        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Loginuser");
        }

        public ActionResult AdminLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("AdminLogin");
        }

        public ActionResult Register()
        {
            return View();
        }

        // POST: Login/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "busi_id,name,email,phno,address,img,ImgFile")] Business_Owner business_Owner)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Business_Owner.Add(business_Owner);
            //    db.SaveChanges();
            //    return RedirectToAction("UserIndex", "Home");
            //}

            //return View(business_Owner);


            string fileName = Path.GetFileNameWithoutExtension(business_Owner.ImgFile.FileName);
            string extension = Path.GetExtension(business_Owner.ImgFile.FileName);
            fileName += DateTime.Now.ToString("yymmssffff") + extension;
            business_Owner.img = "../Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("../Image/"), fileName);
            business_Owner.ImgFile.SaveAs(fileName);
            using (JustFinderTestEntities1 db = new JustFinderTestEntities1())
            {
                db.Business_Owner.Add(business_Owner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}