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
    public class RighAnswersController : Controller
    {
        private DBContext db = new DBContext();

        // GET: RighAnswers
        public ActionResult Index()
        {
            return View(db.tblRighAnswers.ToList());
        }

        // GET: RighAnswers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRighAnswer tblRighAnswer = db.tblRighAnswers.Find(id);
            if (tblRighAnswer == null)
            {
                return HttpNotFound();
            }
            return View(tblRighAnswer);
        }

        // GET: RighAnswers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RighAnswers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,questionId,answerId,answerid_V,answerid_A,reasonText_C,reasonText_V,reasonText_A")] tblRighAnswer tblRighAnswer)
        {
            if (ModelState.IsValid)
            {
                db.tblRighAnswers.Add(tblRighAnswer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblRighAnswer);
        }

        // GET: RighAnswers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRighAnswer tblRighAnswer = db.tblRighAnswers.Find(id);
            if (tblRighAnswer == null)
            {
                return HttpNotFound();
            }
            return View(tblRighAnswer);
        }

        // POST: RighAnswers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,questionId,answerId,answerid_V,answerid_A,reasonText_C,reasonText_V,reasonText_A")] tblRighAnswer tblRighAnswer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblRighAnswer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblRighAnswer);
        }

        // GET: RighAnswers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRighAnswer tblRighAnswer = db.tblRighAnswers.Find(id);
            if (tblRighAnswer == null)
            {
                return HttpNotFound();
            }
            return View(tblRighAnswer);
        }

        // POST: RighAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblRighAnswer tblRighAnswer = db.tblRighAnswers.Find(id);
            db.tblRighAnswers.Remove(tblRighAnswer);
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
