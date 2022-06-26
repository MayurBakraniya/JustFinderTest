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
    public class SubcategoriesController : Controller
    {
        private JustFinderTestEntities1 db = new JustFinderTestEntities1();

        // GET: Subcategories
        public ActionResult Index()
        {
            using (JustFinderTestEntities1 db = new JustFinderTestEntities1())
            {

                var subcategories = db.Subcategories.Include(s => s.Category);
                return View(subcategories.ToList());
            }
        }

        public ActionResult SubcatList(int categoryid)
        {

            using (JustFinderTestEntities1 db = new JustFinderTestEntities1())
            {
                var subcategories = db.Subcategories.Include(s => s.Category).Where(c => c.category_id == categoryid);
                return View(subcategories.ToList());
            }
        }

        // GET: Subcategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategory subcategory = db.Subcategories.Find(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }

        // GET: Subcategories/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name");
            return View();
        }

        // POST: Subcategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "subcat_id,category_id,subcat_name,subcat_img,ImgFile")] Subcategory subcategory)
        {
            string fileName = Path.GetFileNameWithoutExtension(subcategory.ImgFile.FileName);
            string extension = Path.GetExtension(subcategory.ImgFile.FileName);
            fileName += DateTime.Now.ToString("yymmssffff") + extension;
            subcategory.subcat_img = "../Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("../Image/"), fileName);
            subcategory.ImgFile.SaveAs(fileName);
            using (JustFinderTestEntities1 db = new JustFinderTestEntities1())
            {
                db.Subcategories.Add(subcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name", subcategory.category_id);
            return View(subcategory);
        }

        // GET: Subcategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategory subcategory = db.Subcategories.Find(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name", subcategory.category_id);
            return View(subcategory);
        }

        // POST: Subcategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "subcat_id,category_id,subcat_name,subcat_img,ImgFile")] Subcategory subcategory)
        {
            string fileName = Path.GetFileNameWithoutExtension(subcategory.ImgFile.FileName);
            string extension = Path.GetExtension(subcategory.ImgFile.FileName);
            fileName += DateTime.Now.ToString("yymmssffff") + extension;
            subcategory.subcat_img = "../../Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("../../Image/"), fileName);
            subcategory.ImgFile.SaveAs(fileName);

            db.Entry(subcategory).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category_name", subcategory.category_id);
            return View(subcategory);
        }

        // GET: Subcategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategory subcategory = db.Subcategories.Find(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }

        // POST: Subcategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subcategory subcategory = db.Subcategories.Find(id);
            db.Subcategories.Remove(subcategory);
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
