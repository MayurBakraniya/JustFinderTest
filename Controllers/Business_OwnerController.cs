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
    public class Business_OwnerController : Controller
    {
        private JustFinderTestEntities1 db = new JustFinderTestEntities1();

        // GET: Business_Owner
        public ActionResult Index()
        {
            using (JustFinderTestEntities1 db = new JustFinderTestEntities1())
            {
                return View(db.Business_Owner.ToList());
            }
        }

        [Authorize]
        public ActionResult MyProfile()
        {
            int? busiid = Convert.ToInt32(Session["busi_id"]);
            if (busiid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var businesses = db.Business_Owner.Where(b => b.busi_id == busiid).ToList();
            if (businesses == null)
            {
                return HttpNotFound();
            }
            return View(businesses);
        }

        // GET: Business_Owner/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business_Owner business_Owner = db.Business_Owner.Find(id);
            if (business_Owner == null)
            {
                return HttpNotFound();
            }
            return View(business_Owner);
        }

        // GET: Business_Owner/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Business_Owner/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "busi_id,name,email,phno,address,img,ImgFile")] Business_Owner business_Owner)
        {
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


        // GET: Business_Owner/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business_Owner business_Owner = db.Business_Owner.Find(id);
            if (business_Owner == null)
            {
                return HttpNotFound();
            }
            return View(business_Owner);
        }

        // POST: Business_Owner/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "busi_id,name,email,phno,address,img,ImgFile")] Business_Owner business_Owner)
        {
            string fileName = Path.GetFileNameWithoutExtension(business_Owner.ImgFile.FileName);
            string extension = Path.GetExtension(business_Owner.ImgFile.FileName);
            fileName += DateTime.Now.ToString("yymmssffff") + extension;
            business_Owner.img = "../../Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("../../Image/"), fileName);
            business_Owner.ImgFile.SaveAs(fileName);

            db.Entry(business_Owner).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: Business_Owner/Edit/5
        public ActionResult UserEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business_Owner business_Owner = db.Business_Owner.Find(id);
            if (business_Owner == null)
            {
                return HttpNotFound();
            }
            return View(business_Owner);
        }

        // POST: Business_Owner/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit([Bind(Include = "busi_id,name,email,phno,address,img,ImgFile")] Business_Owner business_Owner)
        {
            string fileName = Path.GetFileNameWithoutExtension(business_Owner.ImgFile.FileName);
            string extension = Path.GetExtension(business_Owner.ImgFile.FileName);
            fileName += DateTime.Now.ToString("yymmssffff") + extension;
            business_Owner.img = "../../Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("../../Image/"), fileName);
            business_Owner.ImgFile.SaveAs(fileName);

            db.Entry(business_Owner).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("MyProfile");

        }

        // GET: Business_Owner/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business_Owner business_Owner = db.Business_Owner.Find(id);
            if (business_Owner == null)
            {
                return HttpNotFound();
            }
            return View(business_Owner);
        }

        // POST: Business_Owner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Business_Owner business_Owner = db.Business_Owner.Find(id);
            db.Business_Owner.Remove(business_Owner);
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
