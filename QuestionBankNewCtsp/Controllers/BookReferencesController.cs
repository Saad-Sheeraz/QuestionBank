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
    public class BookReferencesController : Controller
    {
        private DBContext db = new DBContext();

        public ActionResult GetDegree(int division)
        {
            return Json(db.tblSubjects.Where(t => t.classId == division).Select(t => new { t.subjectID, t.subjectName }).ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: BookReferences
        public ActionResult Index()
        {
            var tblBookReferences = db.tblBookReferences.Include(t => t.tblQuestion);
            return View(tblBookReferences.ToList());
        }

        // GET: BookReferences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBookReference tblBookReference = db.tblBookReferences.Find(id);
            if (tblBookReference == null)
            {
                return HttpNotFound();
            }
            return View(tblBookReference);
        }

        // GET: BookReferences/Create
        public ActionResult Create()
        {
            ViewBag.questionId = new SelectList(db.tblQuestions, "questionID", "questionText");
            return View();
        }

        // POST: BookReferences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bookRefID,bookName,pageNum,questionNum,paragraphNum,createdBy,createdOn,updatedBy,updatedOn,status,questionId")] tblBookReference tblBookReference)
        {
            if (ModelState.IsValid)
            {
                db.tblBookReferences.Add(tblBookReference);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.questionId = new SelectList(db.tblQuestions, "questionID", "questionText", tblBookReference.questionId);
            return View(tblBookReference);
        }

        // GET: BookReferences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBookReference tblBookReference = db.tblBookReferences.Find(id);
            if (tblBookReference == null)
            {
                return HttpNotFound();
            }
            ViewBag.questionId = new SelectList(db.tblQuestions, "questionID", "questionText", tblBookReference.questionId);
            return View(tblBookReference);
        }

        // POST: BookReferences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bookRefID,bookName,pageNum,questionNum,paragraphNum,createdBy,createdOn,updatedBy,updatedOn,status,questionId")] tblBookReference tblBookReference)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblBookReference).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.questionId = new SelectList(db.tblQuestions, "questionID", "questionText", tblBookReference.questionId);
            return View(tblBookReference);
        }

        // GET: BookReferences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBookReference tblBookReference = db.tblBookReferences.Find(id);
            if (tblBookReference == null)
            {
                return HttpNotFound();
            }
            return View(tblBookReference);
        }

        // POST: BookReferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblBookReference tblBookReference = db.tblBookReferences.Find(id);
            db.tblBookReferences.Remove(tblBookReference);
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
