using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JustFinderTest;

namespace JustFinderTest.Controllers
{
    public class Contact_UsController : Controller
    {
        private JustFinderTestEntities1 db = new JustFinderTestEntities1();

        // GET: Contact_Us
        public ActionResult Index()
        {
            var contact_Us = db.Contact_Us.Include(c => c.Business_Owner);
            return View(contact_Us.ToList());
        }

        // GET: Contact_Us/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact_Us contact_Us = db.Contact_Us.Find(id);
            if (contact_Us == null)
            {
                return HttpNotFound();
            }
            return View(contact_Us);
        }


        public ActionResult UserCreate()
        {

            ViewBag.busi_id = new SelectList(db.Business_Owner, "busi_id", "name");
            return View();
        }

        // POST: Contact_Us/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserCreate([Bind(Include = "id,busi_id,name,email,subject,msg")] Contact_Us contact_Us)
        {
            if (ModelState.IsValid)
            {
                db.Contact_Us.Add(contact_Us);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.busi_id = new SelectList(db.Business_Owner, "busi_id", "name", contact_Us.busi_id);
            return View(contact_Us);
        }


        // GET: Contact_Us/Create
        public ActionResult Create()
        {
            ViewBag.busi_id = new SelectList(db.Business_Owner, "busi_id", "name");
            return View();
        }

        // POST: Contact_Us/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,busi_id,name,email,subject,msg")] Contact_Us contact_Us)
        {
            if (ModelState.IsValid)
            {
                db.Contact_Us.Add(contact_Us);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.busi_id = new SelectList(db.Business_Owner, "busi_id", "name", contact_Us.busi_id);
            return View(contact_Us);
        }

        // GET: Contact_Us/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact_Us contact_Us = db.Contact_Us.Find(id);
            if (contact_Us == null)
            {
                return HttpNotFound();
            }
            ViewBag.busi_id = new SelectList(db.Business_Owner, "busi_id", "name", contact_Us.busi_id);
            return View(contact_Us);
        }

        // POST: Contact_Us/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,busi_id,name,email,subject,msg")] Contact_Us contact_Us)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact_Us).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.busi_id = new SelectList(db.Business_Owner, "busi_id", "name", contact_Us.busi_id);
            return View(contact_Us);
        }

        // GET: Contact_Us/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact_Us contact_Us = db.Contact_Us.Find(id);
            if (contact_Us == null)
            {
                return HttpNotFound();
            }
            return View(contact_Us);
        }

        // POST: Contact_Us/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact_Us contact_Us = db.Contact_Us.Find(id);
            db.Contact_Us.Remove(contact_Us);
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
