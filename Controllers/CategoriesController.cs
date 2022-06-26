﻿using System;
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
    public class CategoriesController : Controller
    {
        private JustFinderTestEntities1 db = new JustFinderTestEntities1();

        // GET: Categories
        public ActionResult Index()
        {
            using (JustFinderTestEntities1 db = new JustFinderTestEntities1())
            {
                return View(db.Categories.ToList());

            }
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "category_id,category_name,category_img,ImgFile")] Category category)
        {
            string fileName = Path.GetFileNameWithoutExtension(category.ImgFile.FileName);
            string extension = Path.GetExtension(category.ImgFile.FileName);
            fileName += DateTime.Now.ToString("yymmssffff") + extension;
            category.category_img = "../Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("../Image/"), fileName);
            category.ImgFile.SaveAs(fileName);
            using (JustFinderTestEntities1 db = new JustFinderTestEntities1())
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "category_id,category_name,category_img,ImgFile")] Category category)
        {
            string fileName = Path.GetFileNameWithoutExtension(category.ImgFile.FileName);
            string extension = Path.GetExtension(category.ImgFile.FileName);
            fileName += DateTime.Now.ToString("yymmssffff") + extension;
            category.category_img = "../../Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("../../Image/"), fileName);
            category.ImgFile.SaveAs(fileName);

            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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
