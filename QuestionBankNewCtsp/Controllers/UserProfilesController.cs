using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Microsoft.AspNet.Identity;

namespace QustionProjectCTSP.Controllers
{
    public class UserProfilesController : Controller
    {
        private DBContext db = new DBContext();

        // GET: UserProfiles
        public ActionResult Index()
        {

            using (db = new DBContext())
            {
                //var roles = db.viewForRoles.SingleOrDefault(b => b.UserName == User.Identity.Name);
                var roles = db.tblUserProfiles.SingleOrDefault(b => b.email == User.Identity.Name);
                // var rola = db.tblUserProfiles.Where(b => b.email == User.Identity.Name).Select(b => b.userType).FirstOrDefault();
                //string a = rola.ToString();
                if (roles.userType == "Creator")
                {

                    ViewBag.abc = db.tblUserProfiles.Where(u => u.email == User.Identity.Name).ToList();
                    return View();

                }
                else if (roles.userType == "Approver")
                {
                    //ViewBag.abc = db.viewForRoles.Where(u => u.UserName == User.Identity.Name).ToList();
                    ViewBag.abc = db.tblUserProfiles.Where(u => u.email == User.Identity.Name).ToList();
                    return View();
                }
                else if (roles.userType == "Verifier")
                {
                    //ViewBag.abc = db.viewForRoles.Where(u => u.UserName == User.Identity.Name).ToList();
                    ViewBag.abc = db.tblUserProfiles.Where(u => u.email == User.Identity.Name).ToList();
                    return View();
                }
                //---------------temporary commented
                else if (roles.userType == "Admin")
                {
                    //ViewBag.abc = db.viewForRoles.Where(u => u.UserName == User.Identity.Name).ToList();
                    ViewBag.abc = db.tblUserProfiles.Where(u => u.email == User.Identity.Name).ToList();
                    return View();

                }
            }


            //var tblUserProfiles = db.tblUserProfiles;
            //return View(tblUserProfiles.ToList());
            return View();
        }


        public ActionResult blockPerson(int id)
        {
            var data = db.tblUserProfiles.Where(t => t.id == id).FirstOrDefault();
            string userid = data.userid;

            var tblusernet = db.AspNetUsers.Where(t => t.Id == userid).FirstOrDefault();

            if (tblusernet != null)
            {
                tblusernet.isEnable = false;
                db.Entry(tblusernet).State = EntityState.Modified;
                db.SaveChanges();
            }

            if (data != null)
            {
                data.status = false;
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult unblockUser(int id)
        {
            var data = db.tblUserProfiles.Where(t => t.id == id).FirstOrDefault();
            string userid = data.userid;

            var tblusernet = db.AspNetUsers.Where(t => t.Id == userid).FirstOrDefault();

            if (tblusernet != null)
            {
                tblusernet.isEnable = true;
                db.Entry(tblusernet).State = EntityState.Modified;
                db.SaveChanges();
            }

            if (data != null)
            {
                data.status = true;
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Redirect(Request.UrlReferrer.ToString());
        }




        public ActionResult showBlockedUser()
        {
            ViewBag.mylist = db.tblUserProfiles.Where(c => c.status == false && c.userType == "Creator" || c.userType == "Verifier" || c.userType == "Approver").ToList();


            return View();
        }

        public ActionResult ShowPersons()
        {
            DBContext db = new DBContext();
            //  ViewBag.mylist = db.viewForRoles.Where(c => c.Name == "Creator" || c.Name == "Verifier" || c.Name == "Approver").ToList();

            ViewBag.mylist = db.tblUserProfiles.Where(c => c.status == true && c.userType == "Creator" || c.userType == "Verifier" || c.userType == "Approver").ToList();

            return View();
        }


        public ActionResult EditPersons(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            tblUserProfile tblUserProfile = db.tblUserProfiles.Find(id);
            if (tblUserProfile == null)
            {
                return HttpNotFound();
            }
            return View(tblUserProfile);

        }

        [HttpPost]
        public ActionResult EditPersons([Bind(Include = "id,userName,cnic,phone,email,address,profilePic,userType,userid,status")] tblUserProfile tblUserProfile, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                if (file != null)
                {
                    string alpha = "";
                    if (file != null && file.ContentLength > 0)
                    {
                        alpha = tblUserProfile.id + "_" + Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/img/"), alpha);
                        file.SaveAs(path);
                    }
                    tblUserProfile.profilePic = alpha;
                    db.Entry(tblUserProfile).State = EntityState.Modified;
                    db.SaveChanges();

                }

                db.Entry(tblUserProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ShowPersons");

            }
            return View(tblUserProfile);

        }
        public ActionResult showspecs()
        {
            DBContext db = new DBContext();


            ViewBag.mylist = db.viewForRoles.Where(c => c.Name == "Creator" || c.Name == "Verifier" || c.Name == "Approver").ToList();
            //if (ViewBag.mylist!=null)
            //{
            //    var result = db.tblSubjects.SingleOrDefault(b => b.subjectID == view );
            //}
            return View();
        }
        // GET: UserProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUserProfile tblUserProfile = db.tblUserProfiles.Find(id);
            if (tblUserProfile == null)
            {
                return HttpNotFound();
            }
            return View(tblUserProfile);
        }

        // GET: UserProfiles/Create
        public ActionResult Create()
        {
            //ViewBag.userType = new SelectList(db.tblUserTypes, "id", "userType");
            return View();
        }

        // POST: UserProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,userName,cnic,phone,email,address,profilePic")] tblUserProfile tblUserProfile, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {

                string alpha = "";
                if (file != null && file.ContentLength > 0)
                {
                    alpha = tblUserProfile.id + "_" + Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/img/"), alpha);
                    file.SaveAs(path);
                }
                tblUserProfile.createdBy = User.Identity.Name;
                tblUserProfile.createdOn = DateTime.Now;
                tblUserProfile.status = true;
                tblUserProfile.profilePic = alpha;
                db.tblUserProfiles.Add(tblUserProfile);
                db.SaveChanges();


                return RedirectToAction("Index");
            }
            //ViewBag.userType = new SelectList(db.tblUserTypes, "id", "userType", tblUserProfile.userType);
            return View(tblUserProfile);
        }


        // GET: UserProfiles/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tblUserProfile tblUserProfile = db.tblUserProfiles.Find(id);
            if (tblUserProfile == null)
            {
                return HttpNotFound();
            }
            //ViewBag.userType = new SelectList(db.tblUserTypes, "id", "userType", tblUserProfile.userType);
            return View(tblUserProfile);
        }

        // POST: UserProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,userName,cnic,phone,email,address,profilePic,userType,userid")] tblUserProfile tblUserProfile, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string alpha = "";
                    if (file != null && file.ContentLength > 0)
                    {
                        alpha = tblUserProfile.id + "_" + Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/img/"), alpha);
                        file.SaveAs(path);
                    }
                    tblUserProfile.profilePic = alpha;
                    db.Entry(tblUserProfile).State = EntityState.Modified;
                    db.SaveChanges();

                }

                db.Entry(tblUserProfile).State = EntityState.Modified;
                db.SaveChanges();

                // return RedirectToAction("ShowPersons");
                return RedirectToAction("Index");

            }
            //ViewBag.userType = new SelectList(db.tblUserTypes, "id", "userType", tblUserProfile.userType);
            return View();
        }

        // GET: UserProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUserProfile tblUserProfile = db.tblUserProfiles.Find(id);
            if (tblUserProfile == null)
            {
                return HttpNotFound();
            }
            return View(tblUserProfile);
        }

        // POST: UserProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblUserProfile tblUserProfile = db.tblUserProfiles.Find(id);
            db.tblUserProfiles.Remove(tblUserProfile);
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
