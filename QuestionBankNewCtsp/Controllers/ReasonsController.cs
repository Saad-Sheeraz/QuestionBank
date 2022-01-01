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
    public class ReasonsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Reasons
        public ActionResult Index()
        {
            var tblReasons = db.tblReasons.Include(t => t.tblAnwer);
            return View(tblReasons.ToList());
        }

        // GET: Reasons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblReason tblReason = db.tblReasons.Find(id);
            if (tblReason == null)
            {
                return HttpNotFound();
            }
            return View(tblReason);
        }

        // GET: Reasons/Create
        public ActionResult Create()
        {
            ViewBag.answerId = new SelectList(db.tblAnwers, "answerID", "answerText1");
            return View();
        }

        // POST: Reasons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "reasonID,resaonName,createdBy,createdOn,updatedBy,updatedOn,status,answerId")] tblReason tblReason)
        {
            if (ModelState.IsValid)
            {
                db.tblReasons.Add(tblReason);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.answerId = new SelectList(db.tblAnwers, "answerID", "answerText1", tblReason.answerId);
            return View(tblReason);
        }

        // GET: Reasons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblReason tblReason = db.tblReasons.Find(id);
            if (tblReason == null)
            {
                return HttpNotFound();
            }
            ViewBag.answerId = new SelectList(db.tblAnwers, "answerID", "answerText1", tblReason.answerId);
            return View(tblReason);
        }

        // POST: Reasons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "reasonID,resaonName,createdBy,createdOn,updatedBy,updatedOn,status,answerId")] tblReason tblReason)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblReason).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.answerId = new SelectList(db.tblAnwers, "answerID", "answerText1", tblReason.answerId);
            return View(tblReason);
        }

        // GET: Reasons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblReason tblReason = db.tblReasons.Find(id);
            if (tblReason == null)
            {
                return HttpNotFound();
            }
            return View(tblReason);
        }

        // POST: Reasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblReason tblReason = db.tblReasons.Find(id);
            db.tblReasons.Remove(tblReason);
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
