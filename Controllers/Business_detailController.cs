using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JustFinderTest;

namespace JustFinderTest.Controllers
{
    public class Business_detailController : Controller
    {
        private JustFinderTestEntities1 db = new JustFinderTestEntities1();

        // GET: Business_detail
        public ActionResult Index()
        {
            using (JustFinderTestEntities1 db = new JustFinderTestEntities1())
            {
                var business_detail = db.Business_detail.Include(b => b.Area).Include(b => b.Business_Owner).Include(b => b.Subcategory);
                return View(business_detail.ToList());
            }
        }

        public ActionResult MyBusinesses(int businessid)
        {
            using (JustFinderTestEntities1 db = new JustFinderTestEntities1())
            {
                var business_detail = db.Business_detail.Include(b => b.Area).Include(b => b.Business_Owner).Include(b => b.Subcategory).Where(b => b.busi_id == businessid);
                return View(business_detail.ToList());
            }
        }

        public ActionResult ListBySubCategory(int Subcategoryid)
        {
            using (JustFinderTestEntities1 db = new JustFinderTestEntities1())
            {
                var business_detail = db.Business_detail.Include(b => b.Area).Include(b => b.Business_Owner).Include(b => b.Subcategory).Where(b => b.subcat_id == Subcategoryid);
                return View(business_detail.ToList());
            }
        }

        public ActionResult AreaListing()
        {
            var areas = db.Areas.Include(a => a.City);
            return View(areas.ToList());
        }

        public ActionResult DetailByAreaListing(int areaid)
        {
            using (JustFinderTestEntities1 db = new JustFinderTestEntities1())
            {
                var business_detail = db.Business_detail.Include(b => b.Area).Include(b => b.Business_Owner).Include(b => b.Subcategory).Where(b => b.area_id == areaid);
                return View(business_detail.ToList());
            }
        }

        public ActionResult DetailListing()
        {
            using (JustFinderTestEntities1 db = new JustFinderTestEntities1())
            {
                var business_detail = db.Business_detail.Include(b => b.Area).Include(b => b.Business_Owner).Include(b => b.Subcategory);
                return View(business_detail.ToList());
            }
        }

        public ActionResult Listing(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business_detail business_detail = db.Business_detail.Find(id);
            if (business_detail == null)
            {
                return HttpNotFound();
            }
            return View(business_detail);
        }


        // GET: Business_detail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business_detail business_detail = db.Business_detail.Find(id);
            if (business_detail == null)
            {
                return HttpNotFound();
            }
            return View(business_detail);
        }

        // GET: Business_detail/Create
        public ActionResult Create()
        {
            ViewBag.area_id = new SelectList(db.Areas, "area_id", "area_name");
            ViewBag.busi_id = new SelectList(db.Business_Owner, "busi_id", "name");
            ViewBag.subcat_id = new SelectList(db.Subcategories, "subcat_id", "subcat_name");
            return View();
        }

        // POST: Business_detail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bid,bname,busi_id,subcat_id,area_id,address,pincode,busi_email,phno,img,ImgFile,time,description")] Business_detail business_detail)
        {
            string fileName = Path.GetFileNameWithoutExtension(business_detail.ImgFile.FileName);
            string extension = Path.GetExtension(business_detail.ImgFile.FileName);
            fileName += DateTime.Now.ToString("yymmssffff") + extension;
            business_detail.img = "../Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("../Image/"), fileName);
            business_detail.ImgFile.SaveAs(fileName);
            using (JustFinderTestEntities1 db = new JustFinderTestEntities1())
            {
                db.Business_detail.Add(business_detail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.area_id = new SelectList(db.Areas, "area_id", "area_name", business_detail.area_id);
            ViewBag.busi_id = new SelectList(db.Business_Owner, "busi_id", "name", business_detail.busi_id);
            ViewBag.subcat_id = new SelectList(db.Subcategories, "subcat_id", "subcat_name", business_detail.subcat_id);
            return View(business_detail);
        }



        public ActionResult UserCreate()
        {
            ViewBag.area_id = new SelectList(db.Areas, "area_id", "area_name");
            ViewBag.busi_id = Convert.ToString(Session["name"]);
            ViewBag.subcat_id = new SelectList(db.Subcategories, "subcat_id", "subcat_name");
            return View();
        }

        // POST: Business_detail/UserCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserCreate([Bind(Include = "bid,bname,busi_id,subcat_id,area_id,address,pincode,busi_email,phno,img,ImgFile,time,description")] Business_detail business_detail)
        {
            business_detail.busi_id = Convert.ToInt32(Session["busi_id"]);

            string fileName = Path.GetFileNameWithoutExtension(business_detail.ImgFile.FileName);
            string extension = Path.GetExtension(business_detail.ImgFile.FileName);
            fileName += DateTime.Now.ToString("yymmssffff") + extension;
            business_detail.img = "../Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("../Image/"), fileName);
            business_detail.ImgFile.SaveAs(fileName);
            using (JustFinderTestEntities1 db = new JustFinderTestEntities1())
            {
                db.Business_detail.Add(business_detail);
                db.SaveChanges();
                return RedirectToAction("DetailListing", "Business_detail");
            }

            ViewBag.area_id = new SelectList(db.Areas, "area_id", "area_name", business_detail.area_id);
            ViewBag.busi_id = new SelectList(db.Business_Owner, "busi_id", "name", business_detail.busi_id);
            ViewBag.subcat_id = new SelectList(db.Subcategories, "subcat_id", "subcat_name", business_detail.subcat_id);

        }





        // GET: Business_detail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business_detail business_detail = db.Business_detail.Find(id);
            if (business_detail == null)
            {
                return HttpNotFound();
            }
            ViewBag.area_id = new SelectList(db.Areas, "area_id", "area_name", business_detail.area_id);
            ViewBag.busi_id = new SelectList(db.Business_Owner, "busi_id", "name", business_detail.busi_id);
            ViewBag.subcat_id = new SelectList(db.Subcategories, "subcat_id", "subcat_name", business_detail.subcat_id);
            return View(business_detail);
        }

        // POST: Business_detail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bid,bname,busi_id,subcat_id,area_id,address,pincode,busi_email,phno,img,ImgFile,time,description")] Business_detail business_detail)
        {
            string fileName = Path.GetFileNameWithoutExtension(business_detail.ImgFile.FileName);
            string extension = Path.GetExtension(business_detail.ImgFile.FileName);
            fileName += DateTime.Now.ToString("yymmssffff") + extension;
            business_detail.img = "../../Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("../../Image/"), fileName);
            business_detail.ImgFile.SaveAs(fileName);

            db.Entry(business_detail).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

            ViewBag.area_id = new SelectList(db.Areas, "area_id", "area_name", business_detail.area_id);
            ViewBag.busi_id = new SelectList(db.Business_Owner, "busi_id", "name", business_detail.busi_id);
            ViewBag.subcat_id = new SelectList(db.Subcategories, "subcat_id", "subcat_name", business_detail.subcat_id);
           
        }





        // GET: Business_detail/Edit/5
        public ActionResult EditBusiness(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business_detail business_detail = db.Business_detail.Find(id);
            if (business_detail == null)
            {
                return HttpNotFound();
            }
            ViewBag.area_id = new SelectList(db.Areas, "area_id", "area_name", business_detail.area_id);
            ViewBag.busi_id = new SelectList(db.Business_Owner, "busi_id", "name", business_detail.busi_id);
            ViewBag.subcat_id = new SelectList(db.Subcategories, "subcat_id", "subcat_name", business_detail.subcat_id);
            return View(business_detail);
        }

        // POST: Business_detail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBusiness([Bind(Include = "bid,bname,busi_id,subcat_id,area_id,address,pincode,busi_email,phno,img,ImgFile,time,description")] Business_detail business_detail)
        {
            string fileName = Path.GetFileNameWithoutExtension(business_detail.ImgFile.FileName);
            string extension = Path.GetExtension(business_detail.ImgFile.FileName);
            fileName += DateTime.Now.ToString("yymmssffff") + extension;
            business_detail.img = "../../Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("../../Image/"), fileName);
            business_detail.ImgFile.SaveAs(fileName);

            db.Entry(business_detail).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

            ViewBag.area_id = new SelectList(db.Areas, "area_id", "area_name", business_detail.area_id);
            ViewBag.busi_id = new SelectList(db.Business_Owner, "busi_id", "name", business_detail.busi_id);
            ViewBag.subcat_id = new SelectList(db.Subcategories, "subcat_id", "subcat_name", business_detail.subcat_id);

        }







        // GET: Business_detail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business_detail business_detail = db.Business_detail.Find(id);
            if (business_detail == null)
            {
                return HttpNotFound();
            }
            return View(business_detail);
        }

        // POST: Business_detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Business_detail business_detail = db.Business_detail.Find(id);
            db.Business_detail.Remove(business_detail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
