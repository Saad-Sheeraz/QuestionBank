using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace QustionProjectCTSP.Controllers
{
    public class ComplexitiesController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Complexities
        public ActionResult Index()
        {
            return View(db.tblComplexities.ToList());
        }

        // GET: Complexities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblComplexity tblComplexity = db.tblComplexities.Find(id);
            if (tblComplexity == null)
            {
                return HttpNotFound();
            }
            return View(tblComplexity);
        }

        // GET: Complexities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Complexities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "complexityID,complexityLevel,createdBy,createdOn,updatedBy,updatedOn,status")] tblComplexity tblComplexity)
        {
            if (ModelState.IsValid)
            {
                db.tblComplexities.Add(tblComplexity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblComplexity);
        }

        // GET: Complexities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblComplexity tblComplexity = db.tblComplexities.Find(id);
            if (tblComplexity == null)
            {
                return HttpNotFound();
            }
            return View(tblComplexity);
        }

        // POST: Complexities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "complexityID,complexityLevel,createdBy,createdOn,updatedBy,updatedOn,status")] tblComplexity tblComplexity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblComplexity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblComplexity);
        }

        // GET: Complexities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblComplexity tblComplexity = db.tblComplexities.Find(id);
            if (tblComplexity == null)
            {
                return HttpNotFound();
            }
            return View(tblComplexity);
        }

        // POST: Complexities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblComplexity tblComplexity = db.tblComplexities.Find(id);
            db.tblComplexities.Remove(tblComplexity);
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
