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
    public class CategoriesController : Controller
    {
        private DBContext db = new DBContext();

        public ActionResult GetDegree(int division)
        {
            return Json(db.tblSubjects.Where(t => t.classId == division).Where(t => t.status == true).Select(t => new { t.subjectID, t.subjectName }).ToList(), JsonRequestBehavior.AllowGet);

        }
        // GET: Categories
        public ActionResult Index()
        {
            var tblCategories = db.tblCategories.Include(t => t.tblDegree).Include(t => t.tblSubject);
            return View(tblCategories.Where(t => t.status == true));
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategory tblCategory = db.tblCategories.Find(id);
            if (tblCategory == null)
            {
                return HttpNotFound();
            }
            return View(tblCategory);
        }

        // GET: Categories/Create
        public ActionResult Create(string a)
        {
            var c = db.tblDegrees.Where(t => t.status == true);
            ViewBag.classId = new SelectList(c, "degreeID", "degreeName");

            var c2 = db.tblSubjects.Where(t => t.status == true);
            ViewBag.subjectId = new SelectList(c2, "subjectID", "subjectName");

            ViewBag.msg = a;
            //  ViewBag.classId = new SelectList(db.tblDegrees, "degreeID", "degreeName");
            // ViewBag.subjectId = new SelectList(db.tblSubjects, "subjectID", "subjectName");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "categoryID,classId,subjectId,categoryName,createdBy,createdOn,updatedBy,updatedOn,status")] tblCategory tblCategory)
        {
            if (ModelState.IsValid)
            {
                var x = db.tblCategories.Where(t => t.categoryName == tblCategory.categoryName && t.classId == tblCategory.classId && t.subjectId == tblCategory.subjectId && t.status == true).ToList();

                if (x.Count > 0)
                {
                    foreach (var element in x)
                    {
                        if (element.classId == tblCategory.classId && element.subjectId == tblCategory.subjectId && element.categoryName.ToUpper() == tblCategory.categoryName.ToUpper())
                        {
                            string a = "Topic already exist..!";
                            return RedirectToAction("Create", new { a });
                        }
                        else
                        {
                            tblCategory.status = true;
                            tblCategory.createdBy = User.Identity.Name;
                            tblCategory.createdOn = DateTime.Now;
                            db.tblCategories.Add(tblCategory);
                            db.SaveChanges();
                            ViewBag.msg = null;
                            return RedirectToAction("Index");
                        }
                    }

                }
                else
                {

                    tblCategory.status = true;
                    tblCategory.createdBy = User.Identity.Name;
                    tblCategory.createdOn = DateTime.Now;
                    db.tblCategories.Add(tblCategory);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }


            }

            var c = db.tblDegrees.Where(t => t.status == true);
            ViewBag.classId = new SelectList(c, "degreeID", "degreeName");

            var c2 = db.tblSubjects.Where(t => t.status == true);
            ViewBag.subjectId = new SelectList(c2, "subjectID", "subjectName");

            //ViewBag.classId = new SelectList(db.tblDegrees, "degreeID", "degreeName", tblCategory.classId);
            //ViewBag.subjectId = new SelectList(db.tblSubjects, "subjectID", "subjectName", tblCategory.subjectId);
            return View(tblCategory);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id,string a)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tblCategory tblCategory = db.tblCategories.Find(id);

            if (tblCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.msg = a;
            var alpha = db.tblCategories.Where(t => t.categoryID == id).Select(t => t.classId).SingleOrDefault();
            int newClassID = Convert.ToInt32(alpha);
            ViewBag.classId = new SelectList(db.tblDegrees.Where(t => t.status == true), "degreeID", "degreeName", tblCategory.classId);
            ViewBag.subjectId = new SelectList(db.tblSubjects.Where(t => t.classId == newClassID).Where(t => t.status == true), "subjectID", "subjectName");
            return View(tblCategory);

        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "categoryID,classId,subjectId,categoryName,createdBy,createdOn,updatedBy,updatedOn,status")] tblCategory tblCategory)
        {
            if (ModelState.IsValid)
            {
                var x = db.tblCategories.Where(t => t.categoryName == tblCategory.categoryName && t.subjectId==tblCategory.subjectId && t.classId == tblCategory.classId && t.status == true).ToList();
                if(x.Count>0)
                {
                    foreach (var element in x)
                    {

                        if (element.classId == tblCategory.classId && element.categoryName.ToUpper() == tblCategory.categoryName.ToUpper())
                        {
                            string a = "Tooic already exist..!";
                            return RedirectToAction("Edit", new { a });

                        }
                        else
                        {
                            tblCategory.updatedBy = User.Identity.Name;
                            tblCategory.updatedOn = DateTime.Now;
                            db.Entry(tblCategory).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    tblCategory.updatedBy = User.Identity.Name;
                    tblCategory.updatedOn = DateTime.Now;
                    db.Entry(tblCategory).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }              
            }

            ViewBag.classId = new SelectList(db.tblDegrees, "degreeID", "degreeName", tblCategory.classId);
            ViewBag.subjectId = new SelectList(db.tblSubjects, "subjectID", "subjectName", tblCategory.subjectId);
            return View(tblCategory);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategory tblCategory = db.tblCategories.Find(id);
            if (tblCategory == null)
            {
                return HttpNotFound();
            }
            return View(tblCategory);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "subjectId,classId,categoryID,categoryName,createdBy,createdOn,updatedBy,updatedOn,status")] tblCategory tblCategories)
        {
            tblCategories.status = false;
            db.Entry(tblCategories).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
            //tblCategory tblCategory = db.tblCategories.Find(id);
            //db.tblCategories.Remove(tblCategory);
            //db.SaveChanges();
            // return RedirectToAction("Index");
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
