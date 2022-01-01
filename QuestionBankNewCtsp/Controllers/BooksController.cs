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
    public class BooksController : Controller
    {
        public ActionResult GetDegree(int division)
        {
            return Json(db.tblSubjects.Where(t => t.classId == division && t.status == true).Select(t => new { t.subjectID, t.subjectName }).ToList(), JsonRequestBehavior.AllowGet);
        }

        private DBContext db = new DBContext();

        // GET: Books
        public ActionResult Index()
        {
            var tblBooks = db.tblBooks.Include(t => t.tblDegree).Include(t => t.tblSubject);
            return View(tblBooks.Where(t => t.status == true));
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBook tblBook = db.tblBooks.Find(id);
            if (tblBook == null)
            {
                return HttpNotFound();
            }
            return View(tblBook);
        }

        // GET: Books/Create
        public ActionResult Create(string a)
        {
            var c = db.tblDegrees.Where(t => t.status == true);
            ViewBag.degreeId = new SelectList(c, "degreeID", "degreeName");

            var c1 = db.tblSubjects.Where(t => t.status == true);
            ViewBag.subjectId = new SelectList(c1, "subjectID", "subjectName");

            ViewBag.msg = a;
            // ViewBag.degreeId = new SelectList(db.tblDegrees, "degreeID", "degreeName");
            //ViewBag.subjectId = new SelectList(db.tblSubjects, "subjectID", "subjectName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bookID,bookName,degreeId,subjectId,bookAuthor,createdBy,createdOn,updatedBy,updatedOn,status")] tblBook tblBook)
        {
            if (ModelState.IsValid)
            {
                var x = db.tblBooks.Where(t => t.bookName == tblBook.bookName &&t.bookAuthor==tblBook.bookAuthor && t.degreeId == tblBook.degreeId && t.subjectId == tblBook.subjectId && t.status == true).ToList();
                if (x.Count > 0)
                {
                    foreach (var element in x)
                    {
                        if (element.subjectId == tblBook.subjectId && element.degreeId == tblBook.degreeId && element.bookAuthor == tblBook.bookAuthor && element.bookName.ToUpper() == tblBook.bookName.ToUpper())
                        {
                            string a = "Book already exist..!";
                            return RedirectToAction("Create", new { a });
                        }
                        else
                        {

                            tblBook.status = true;
                            tblBook.createdBy = User.Identity.Name;
                            tblBook.createdOn = DateTime.Now;
                            db.tblBooks.Add(tblBook);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }

                }
                else
                {

                    tblBook.status = true;
                    tblBook.createdBy = User.Identity.Name;
                    tblBook.createdOn = DateTime.Now;
                    db.tblBooks.Add(tblBook);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            var c = db.tblDegrees.Where(t => t.status == true);
            ViewBag.degreeId = new SelectList(c, "degreeID", "degreeName", tblBook.degreeId);

            var c1 = db.tblSubjects.Where(t => t.status == true);
            ViewBag.subjectId = new SelectList(c1, "subjectID", "subjectName", tblBook.subjectId);


            //ViewBag.degreeId = new SelectList(db.tblDegrees, "degreeID", "degreeName", tblBook.degreeId);
            //ViewBag.subjectId = new SelectList(db.tblSubjects, "subjectID", "subjectName", tblBook.subjectId);
            return View(tblBook);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id,string a)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBook tblBook = db.tblBooks.Find(id);
            ViewBag.msg = a;
            if (tblBook == null)
            {
                return HttpNotFound();
            }


            var alpha = db.tblBooks.Where(t => t.bookID == id).Select(t => t.degreeId).SingleOrDefault();
            int newClassID = Convert.ToInt32(alpha);
            ViewBag.degreeId = new SelectList(db.tblDegrees.Where(t => t.status == true), "degreeID", "degreeName", tblBook.degreeId);
            ViewBag.subjectId = new SelectList(db.tblSubjects.Where(t => t.classId == newClassID).Where(t => t.status == true), "subjectID", "subjectName");

            //var c = db.tblDegrees.Where(t => t.status == true);
            //ViewBag.degreeId = new SelectList(c, "degreeID", "degreeName", tblBook.degreeId);

            //var c1 = db.tblSubjects.Where(t => t.status == true);
            //ViewBag.subjectId = new SelectList(c1, "subjectID", "subjectName", tblBook.subjectId);

            //ViewBag.degreeId = new SelectList(db.tblDegrees, "degreeID", "degreeName", tblBook.degreeId);
            //ViewBag.subjectId = new SelectList(db.tblSubjects, "subjectID", "subjectName", tblBook.subjectId);
            return View(tblBook);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bookID,bookName,degreeId,subjectId,bookAuthor,createdBy,createdOn,updatedBy,updatedOn,status")] tblBook tblBook)
        {
            if (ModelState.IsValid)
            {
                var x = db.tblBooks.Where(t => t.bookName.ToUpper() == tblBook.bookName.ToUpper() && t.bookAuthor.ToUpper()==tblBook.bookAuthor.ToUpper() && t.degreeId == tblBook.degreeId && t.subjectId == tblBook.subjectId && t.status == true).ToList();
                if (x.Count > 0)
                {
                    foreach (var element in x)
                    {
                        if (element.subjectId == tblBook.subjectId && element.degreeId == tblBook.degreeId && element.bookAuthor == tblBook.bookAuthor && element.bookName.ToUpper() == tblBook.bookName.ToUpper())
                        {
                            string a = "Book already exist..!";
                            return RedirectToAction("Edit", new { a });
                        }
                        else
                        {

                            tblBook.status = true;
                            tblBook.createdBy = User.Identity.Name;
                            tblBook.createdOn = DateTime.Now;
                            db.tblBooks.Add(tblBook);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }

                }
                else
                {
                    tblBook.updatedBy = User.Identity.Name;
                    tblBook.updatedOn = DateTime.Now;
                    db.Entry(tblBook).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            var c = db.tblDegrees.Where(t => t.status == true);
            ViewBag.degreeId = new SelectList(c, "degreeID", "degreeName", tblBook.degreeId);

            var c1 = db.tblSubjects.Where(t => t.status == true);
            ViewBag.subjectId = new SelectList(c1, "subjectID", "subjectName", tblBook.subjectId);

            //ViewBag.degreeId = new SelectList(db.tblDegrees, "degreeID", "degreeName", tblBook.degreeId);
            //ViewBag.subjectId = new SelectList(db.tblSubjects, "subjectID", "subjectName", tblBook.subjectId);
            return View(tblBook);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBook tblBook = db.tblBooks.Find(id);
            if (tblBook == null)
            {
                return HttpNotFound();
            }
            return View(tblBook);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "bookID,bookName,degreeId,subjectId,bookAuthor,createdBy,createdOn,updatedBy,updatedOn,status")] tblBook tblBook)
        {
            tblBook.status = false;
            db.Entry(tblBook).State = EntityState.Modified;
            db.SaveChanges();


            //tblBook tblBook = db.tblBooks.Find(id);
            //db.tblBooks.Remove(tblBook);
            //db.SaveChanges();
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
