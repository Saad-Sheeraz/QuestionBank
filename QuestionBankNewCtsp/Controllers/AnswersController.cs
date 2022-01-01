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
    public class AnswersController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Answers
        public ActionResult Index()
        {
            return View(db.tblAnwers.ToList());
        }

        // GET: Answers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAnwer tblAnwer = db.tblAnwers.Find(id);
            if (tblAnwer == null)
            {
                return HttpNotFound();
            }
            return View(tblAnwer);
        }

        // GET: Answers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "answerID,questionId,answerText1,answerText2,answerText3,answerText4,answerRight,createdBy,createdOn,updatedBy,updatedOn,status")] tblAnwer tblAnwer)
        {
            if (ModelState.IsValid)
            {
                db.tblAnwers.Add(tblAnwer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblAnwer);
        }

        // GET: Answers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAnwer tblAnwer = db.tblAnwers.Find(id);
            if (tblAnwer == null)
            {
                return HttpNotFound();
            }
            return View(tblAnwer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "answerID,questionId,answerText1,answerText2,answerText3,answerText4,answerRight,createdBy,createdOn,updatedBy,updatedOn,status")] tblAnwer tblAnwer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblAnwer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblAnwer);
        }

        // GET: Answers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAnwer tblAnwer = db.tblAnwers.Find(id);
            if (tblAnwer == null)
            {
                return HttpNotFound();
            }
            return View(tblAnwer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblAnwer tblAnwer = db.tblAnwers.Find(id);
            db.tblAnwers.Remove(tblAnwer);
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
