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
    public class DegreesController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Degrees
        public ActionResult Index()
        {
            // tblDegree tblDegree = new tblDegree();
            // tblDegree = db.tblDegrees.Where(t => t.status == true).ToList();
            //return View(db.tblDegrees.ToList());
            return View(db.tblDegrees.Where(t => t.status == true).ToList());
        }

        // GET: Degrees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDegree tblDegree = db.tblDegrees.Find(id);
            if (tblDegree == null)
            {
                return HttpNotFound();
            }
            return View(tblDegree);
        }

        // GET: Degrees/Create
        public ActionResult Create(string a)
        {
            ViewBag.msg = a;
            return View();
        }

        // POST: Degrees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "degreeID,degreeName,createdBy,createdOn,updatedBy,updatedOn,status")] tblDegree tblDegree)
        {
            if (ModelState.IsValid)
            {
                var x = db.tblDegrees.Where(t=>t.degreeName==tblDegree.degreeName && t.status==true).ToList();
                if(x.Count>0)
                {
                    foreach (var element in x)
                    {
                        if (element.degreeName.ToUpper() == tblDegree.degreeName.ToUpper())
                        {
                            string a = "Class Already Exist..!";
                            return RedirectToAction("Create", new { a });
                        }
                        else
                        {
                            tblDegree.status = true;
                            tblDegree.createdBy = User.Identity.Name;
                            tblDegree.createdOn = DateTime.Now;
                            db.tblDegrees.Add(tblDegree);
                            db.SaveChanges();
                            ViewBag.msg = null;
                            return RedirectToAction("Index");

                        }
                    }
                }
                else
                {
                    tblDegree.status = true;
                    tblDegree.createdBy = User.Identity.Name;
                    tblDegree.createdOn = DateTime.Now;
                    db.tblDegrees.Add(tblDegree);
                    db.SaveChanges();
                    ViewBag.msg = null;
                    return RedirectToAction("Index");
                }
              

            }

            return View(tblDegree);
        }

        // GET: Degrees/Edit/5
        public ActionResult Edit(int? id,string a)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDegree tblDegree = db.tblDegrees.Find(id);
            if (tblDegree == null)
            {
                return HttpNotFound();
            }
            ViewBag.msg = a;
            return View(tblDegree);
        }

        // POST: Degrees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "degreeID,degreeName,createdBy,createdOn,updatedBy,updatedOn,status")] tblDegree tblDegree)
        {
            if (ModelState.IsValid)
            {
                //DBContext dbnew = new DBContext();
                //var x = dbnew.tblDegrees;

                var x = db.tblDegrees.Where(t => t.degreeName == tblDegree.degreeName && t.status == true).ToList();
                if (x.Count > 0)
                {
                    foreach (var element in x)
                    {
                        if (element.degreeName.ToUpper() == tblDegree.degreeName.ToUpper())
                        {
                            string a = "Class Already Exist..!";
                            return RedirectToAction("Edit", new { a });
                        }
                        else
                        {
                            tblDegree.status = true;
                            tblDegree.createdBy = User.Identity.Name;
                            tblDegree.createdOn = DateTime.Now;
                            db.tblDegrees.Add(tblDegree);
                            db.SaveChanges();
                            ViewBag.msg = null;
                            return RedirectToAction("Index");

                        }
                    }
                }
                else
                {
                    tblDegree.status = true;
                    tblDegree.createdBy = User.Identity.Name;
                    tblDegree.createdOn = DateTime.Now;
                    db.tblDegrees.Add(tblDegree);
                    db.SaveChanges();
                    ViewBag.msg = null;
                    return RedirectToAction("Index");
                }


                //foreach (var element in x)
                //{
                //    if (element.degreeName != tblDegree.degreeName)
                //    {
                //        tblDegree.createdOn = DateTime.Now;
                //        tblDegree.createdBy = User.Identity.Name;
                //       // db.Entry(x).State = EntityState.Deleted;
                //        db.Entry(tblDegree).State = EntityState.Modified;
                //        db.SaveChanges();
                //        return RedirectToAction("Index");
                       
                //    }
                //    else if(element.degreeName==tblDegree.degreeName)
                //    {
                //        string a = "Class Already Exist..!";
                //      //  db.Entry(tblDegree).State = EntityState.Unchanged;
                //        return RedirectToAction("Edit", new {id=tblDegree.degreeID,a });
                //    }
                //    else
                //    {
                //        return RedirectToAction("Index");
                //    }

                //}
              
            }
            return View(tblDegree);
        }

        // GET: Degrees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDegree tblDegree = db.tblDegrees.Find(id);
            if (tblDegree == null)
            {
                return HttpNotFound();
            }
            return View(tblDegree);
        }

        // POST: Degrees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "degreeID,degreeName,createdBy,createdOn,updatedBy,updatedOn,status")] tblDegree tblDegree)
        {

            tblDegree.status = false;
            db.Entry(tblDegree).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
            //tblDegree tblDegree = db.tblDegrees.Find(id);
            //db.tblDegrees.Remove(tblDegree);
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
