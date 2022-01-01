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
    public class SubjectsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Subjects
        public ActionResult Index()
        {
            var tblSubjects = db.tblSubjects.Include(t => t.tblDegree);
            return View(tblSubjects.Where(t => t.status == true));
        }

        // GET: Subjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSubject tblSubject = db.tblSubjects.Find(id);
            if (tblSubject == null)
            {
                return HttpNotFound();
            }
            return View(tblSubject);
        }

        // GET: Subjects/Create
        public ActionResult Create(string a)
        {

            var c = db.tblDegrees.Where(t => t.status == true);
            ViewBag.classId = new SelectList(c, "degreeID", "degreeName");
            ViewBag.msg= a;
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "subjectID,classId,subjectName,createdBy,createdOn,updatedBy,updatedOn,status")] tblSubject tblSubject)
        {
            if (ModelState.IsValid)
            {
               
               // var x = db.tblSubjects.ToList();
                var x = db.tblSubjects.Where(t => t.subjectName == tblSubject.subjectName && t.classId==tblSubject.classId && t.status == true).ToList();
                if(x.Count>0)
                {
                    foreach (var element in x)
                    {
                        if (element.classId == tblSubject.classId && element.subjectName.ToUpper() == tblSubject.subjectName.ToUpper())
                        {
                            string a = "Subject already exist..!";
                            return RedirectToAction("Create", new { a });
                        }
                        else
                        {
                            tblSubject.status = true;
                            tblSubject.createdBy = User.Identity.Name;
                            tblSubject.createdOn = System.DateTime.Now;
                            db.tblSubjects.Add(tblSubject);
                            db.SaveChanges();
                            ViewBag.msg = null;
                            return RedirectToAction("Index");
                        }
                    }

                }
                else
                {

                    tblSubject.status = true;
                    tblSubject.createdBy = User.Identity.Name;
                    tblSubject.createdOn = System.DateTime.Now;
                    db.tblSubjects.Add(tblSubject);
                    db.SaveChanges();
                    ViewBag.msg = null;
                    return RedirectToAction("Index");
                }
               

            }
            var c = db.tblDegrees.Where(t => t.status == true);
            ViewBag.classId = new SelectList(c, "degreeID", "degreeName");

            //ViewBag.classId = new SelectList(db.tblDegrees, "degreeID", "degreeName", tblSubject.classId);
            return View(tblSubject);
        }

        // GET: Subjects/Edit/5
        public ActionResult Edit(int? id, string a)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSubject tblSubject = db.tblSubjects.Find(id);
            if (tblSubject == null)
            {
                return HttpNotFound();
            }

            var c = db.tblDegrees.Where(t => t.status == true);
            ViewBag.classId = new SelectList(c, "degreeID", "degreeName", tblSubject.classId);
            ViewBag.msg = a;


            //ViewBag.classId = new SelectList(db.tblDegrees, "degreeID", "degreeName", tblSubject.classId);
            return View(tblSubject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "subjectID,classId,subjectName,createdBy,createdOn,updatedBy,updatedOn,status")] tblSubject tblSubject)
        {
            if (ModelState.IsValid)
            {
                // DBContext dbnew = new DBContext();        
                //var x = dbnew.tblSubjects.ToList();
                var x = db.tblSubjects.Where(t => t.subjectName == tblSubject.subjectName && t.classId == tblSubject.classId && t.status == true).ToList();
                if(x.Count>0)
                {
                    foreach (var element in x)
                    {

                        if (element.classId == tblSubject.classId && element.subjectName.ToUpper() == tblSubject.subjectName.ToUpper())
                        {
                            string a = "Subject already exist..!";
                            return RedirectToAction("Edit", new { a });
                            
                        }
                        else
                        {
                            tblSubject.updatedBy = User.Identity.Name;
                            tblSubject.updatedOn = DateTime.Now;
                            db.Entry(tblSubject).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    tblSubject.updatedBy = User.Identity.Name;
                    tblSubject.updatedOn = DateTime.Now;
                    db.Entry(tblSubject).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
               

            }
            var c = db.tblDegrees.Where(t => t.status == true);
            ViewBag.classId = new SelectList(c, "degreeID", "degreeName", tblSubject.classId);

            // ViewBag.classId = new SelectList(db.tblDegrees, "degreeID", "degreeName", tblSubject.classId);
            return View(tblSubject);
        }

        // GET: Subjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSubject tblSubject = db.tblSubjects.Find(id);
            if (tblSubject == null)
            {
                return HttpNotFound();
            }
            return View(tblSubject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "subjectID,classId,subjectName,createdBy,createdOn,updatedBy,updatedOn,status")] tblSubject tblSubject)
        {
            tblSubject.status = false;
            db.Entry(tblSubject).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
            //tblSubject tblSubject = db.tblSubjects.Find(id);
            //db.tblSubjects.Remove(tblSubject);         
            //db.SaveChanges();
            //return RedirectToAction("Index");
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
