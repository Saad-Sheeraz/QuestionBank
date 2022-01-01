using System;
using System.Collections;
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

    public class QuestionsController : Controller
    {

        private DBContext db = new DBContext();

        public ActionResult GetDegree(int division)
        {
            return Json(db.tblSubjects.Where(t => t.classId == division).Where(t => t.status == true).Select(t => new { t.subjectID, t.subjectName }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubject(int division)
        {
            return Json(db.tblCategories.Where(t => t.subjectId == division).Where(t => t.status == true).Select(t => new { t.categoryID, t.categoryName }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBooksname(int division)
        {
            return Json(db.tblBooks.Where(t => t.subjectId == division).Where(t => t.status == true).Select(t => new { t.bookID, t.bookName }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBooksauthor(int division)
        {
            return Json(db.tblBooks.Where(t => t.bookID == division).Where(t => t.status == true).Select(t => new { t.bookID, t.bookAuthor }).ToList(), JsonRequestBehavior.AllowGet);
        }

        //--------------------------------------------- till here scripts for dropdown---------------------------------------------------//


        //---------------------------------------------Dashboard Functions------------ Section---------------------------------------------------
        public ActionResult admin_DashQuestions()
        {
            string userid = User.Identity.GetUserId();
            ViewBag.mydata = db.viewForVerfiers.Where(c => c.statusInfo != null).ToList();
            ViewBag.userProfiledata = db.tblUserProfiles.ToList();
            ViewBag.type = db.AspNetUsers.Where(c => c.Id == userid).Select(c => c.RollId).SingleOrDefault();
            ViewBag.check = "allquestions";
            return View();
        }

        //For show all answer to admin
        public ActionResult admin_Gridanswer(int? id, string type, string check)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.specificdata = db.viewForVerfiers.Where(q => q.questionID == id).ToList();
            if (ViewBag.specificdata == null)
            {
                return HttpNotFound();
            }
            tblQuestion tbl;
            tbl = db.tblQuestions.Where(q => q.questionID == id).FirstOrDefault();

            ViewBag.type = type;
            ViewBag.check = check;
            return View(tbl);

        }

        public ActionResult admin_ViewDetails(int? id, int? id2, string type, string check)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.detail = db.viewForVerfiers.Where(c => c.rightID == id).ToList();
            ViewBag.deg = db.viewForVerfiers.Where(t => t.rightID == id).Select(t => t.degreeName).FirstOrDefault();
            ViewBag.sub = db.viewForVerfiers.Where(t => t.rightID == id).Select(t => t.subjectName).FirstOrDefault();
            ViewBag.topic = db.viewForVerfiers.Where(t => t.rightID == id).Select(t => t.categoryName).FirstOrDefault();

            var bookid = db.tblQuestions.Where(t => t.questionID == id2).Select(t => t.bookId).FirstOrDefault();
            int i = Convert.ToInt32(bookid);
            ViewBag.bookdetail = db.tblBooks.Where(t => t.bookID == i).ToList();

            var complexid = db.tblQuestions.Where(t => t.questionID == id2).Select(t => t.complexityId).FirstOrDefault();
            int j = Convert.ToInt32(complexid);
            ViewBag.complexid = db.tblComplexities.Where(t => t.complexityID == j).ToList();

            ViewBag.type = type;
            ViewBag.check = check;
            return View();
        }


        public ActionResult creator_ViewPendingQuestion()
        {
            string userid = User.Identity.GetUserId();
            // var list = db.tblQuestions.Where(c => c.UserID == userid && c.statusInfo == "pending").ToList();
            var list = db.viewForVerfiers.Where(c => c.UserID == userid && c.statusInfo == "pending").ToList();
            ViewBag.userProfiledata = db.tblUserProfiles.ToList();
            ViewBag.c_pendingQ = list;

            //special ones
            ViewBag.type = db.AspNetUsers.Where(c => c.Id == userid).Select(c => c.RollId).SingleOrDefault();
            ViewBag.check = "created";
            return View();
        }


        public ActionResult creator_ViewVerifiedQuestion()
        {
            string userid = User.Identity.GetUserId();

            var list = db.viewForVerfiers.Where(c => c.UserID == userid && c.statusInfo == "verified"|| c.UserID==userid && c.verifiedBy!=null).ToList();
            ViewBag.userProfiledata = db.tblUserProfiles.ToList();
            ViewBag.c_verifiedQ = list;

            //special ones
            ViewBag.type = db.AspNetUsers.Where(c => c.Id == userid).Select(c => c.RollId).SingleOrDefault();

            ViewBag.check = "verified";

            return View();
        }


        public ActionResult creator_ViewApprovedQuestion()
        {
            string userid = User.Identity.GetUserId();

            var list = db.viewForVerfiers.Where(c => c.UserID == userid && c.statusInfo == "approved").ToList();
            ViewBag.userProfiledata = db.tblUserProfiles.ToList();
            ViewBag.c_approvedQ = list;
            ViewBag.type = db.AspNetUsers.Where(c => c.Id == userid).Select(c => c.RollId).SingleOrDefault();

            ViewBag.check = "approved";

            return View();
        }


        public ActionResult creator_ViewDiscardedQuestion()
        {
            string userid = User.Identity.GetUserId();

            var list = db.viewForVerfiers.Where(c => c.UserID == userid && c.statusInfo == "deleted").ToList();
            ViewBag.userProfiledata = db.tblUserProfiles.ToList();
            ViewBag.c_discardQ = list;
            ViewBag.type = db.AspNetUsers.Where(c => c.Id == userid).Select(c => c.RollId).SingleOrDefault();

            ViewBag.check = "discard";

            return View();
        }

        public ActionResult creator_ViewAllQuestion()
        {
            string userid = User.Identity.GetUserId();
            var list = db.viewForVerfiers.Where(c => c.UserID == userid && c.statusInfo != null).ToList();
            ViewBag.userProfiledata = db.tblUserProfiles.ToList();
            ViewBag.allqList = list;
            ViewBag.type = db.AspNetUsers.Where(c => c.Id == userid).Select(c => c.RollId).SingleOrDefault();

            ViewBag.check = "creator_All";

            return View();

        }


        //-----------------------------verifier

        public ActionResult verifier_ViewVerifiedQuestion()
        {
            string uname = User.Identity.GetUserName();
            var list = db.viewForVerfiers.Where(c => c.verifiedBy == uname && c.statusInfo == "verified").ToList();
            ViewBag.userProfiledata = db.tblUserProfiles.ToList();
            ViewBag.v_verif = list;
            ViewBag.type = db.AspNetUsers.Where(c => c.UserName == uname).Select(c => c.RollId).SingleOrDefault();
            ViewBag.check = "verified";
            return View();
        }

        public ActionResult verifier_ViewApprovedQuestion()
        {
            string uname = User.Identity.GetUserName();
            var list = db.viewForVerfiers.Where(c => c.verifiedBy == uname && c.statusInfo == "approved").ToList();
            ViewBag.userProfiledata = db.tblUserProfiles.ToList();
            ViewBag.v_approve = list;
            ViewBag.type = db.AspNetUsers.Where(c => c.UserName == uname).Select(c => c.RollId).SingleOrDefault();
            ViewBag.check = "approved";
            return View();

        }

        public ActionResult verifier_ViewDiscardedQuestion()
        {
            string uname = User.Identity.GetUserName();
            var list = db.viewForVerfiers.Where(c => c.verifiedBy == uname && c.statusInfo == "deleted").ToList();
            ViewBag.userProfiledata = db.tblUserProfiles.ToList();
            ViewBag.v_deleted = list;
            ViewBag.type = db.AspNetUsers.Where(c => c.UserName == uname).Select(c => c.RollId).SingleOrDefault();
            ViewBag.check = "deleted";
            return View();
        }

        public ActionResult verifier_ViewAllQuestions()
        {
            string uname = User.Identity.GetUserName();
            var list = db.viewForVerfiers.Where(c => c.verifiedBy == uname).ToList();
            ViewBag.userProfiledata = db.tblUserProfiles.ToList();
            ViewBag.v_allQ = list;
            ViewBag.type = db.AspNetUsers.Where(c => c.UserName == uname).Select(c => c.RollId).SingleOrDefault();
            ViewBag.check = "all";
            return View();
        }


        //-----------------------------approver

        public ActionResult approver_viewApprovedQuestion()
        {
            string uname = User.Identity.GetUserName();
            var list = db.viewForVerfiers.Where(c => c.approvedBy == uname && c.statusInfo == "approved").ToList();
            ViewBag.userProfiledata = db.tblUserProfiles.ToList();
            ViewBag.a_allQ = list;
            ViewBag.type = db.AspNetUsers.Where(c => c.UserName == uname).Select(c => c.RollId).SingleOrDefault();
            ViewBag.check = "approved";
            return View();
        }
        //----------------------------------------------Dashboard Functions----------- Section  Ends---------------------------------------------


        //---------------------------------------------Creator------------ Section---------------------------------------------------


        public ActionResult c_dashboard()
        {

            var admin = db.AspNetUsers.Where(c => c.Email == User.Identity.Name && c.RollId == "Admin").FirstOrDefault();
            if (admin != null)
            {
                ViewBag.usertype = admin.RollId;
                ViewBag.qcount = db.tblQuestions.Where(c => c.statusInfo != null).Count();
                ViewBag.deg_count = db.tblDegrees.Where(c => c.status == true).Count();
                ViewBag.sub_count = db.tblSubjects.Where(c => c.status == true).Count();
                ViewBag.cat_count = db.tblCategories.Where(c => c.status == true).Count();
                ViewBag.usercount = db.tblUserProfiles.Where(t => t.userType != "Admin" && t.status == true).Count();

            }
            var creator = db.AspNetUsers.Where(c => c.Email == User.Identity.Name && c.RollId == "Creator").FirstOrDefault();
            if (creator != null)
            {
                ViewBag.usertype = creator.RollId;
                //ViewBag.qcount = db.tblQuestions.Where(c => c.UserID == creator.Id).Count();
                //ViewBag.deg_count = db.tblDegrees.Where(c => c.status == true).Count();
                //ViewBag.sub_count = db.tblSubjects.Where(c => c.status == true).Count();
                //ViewBag.cat_count = db.tblCategories.Where(c => c.status == true).Count();

                ViewBag.pend_q = db.tblQuestions.Where(c => c.UserID == creator.Id && c.statusInfo == "pending").Count();
                ViewBag.ver_q = db.tblQuestions.Where(c => c.UserID == creator.Id && c.statusInfo == "verified" || c.UserID == creator.Id && c.verifiedBy != null).Count();
                ViewBag.app_q = db.tblQuestions.Where(c => c.UserID == creator.Id && c.statusInfo == "approved").Count();
                ViewBag.discarded_q = db.tblQuestions.Where(c => c.UserID == creator.Id && c.statusInfo == "deleted").Count();
                ViewBag.totalQ = db.tblQuestions.Where(c => c.UserID == creator.Id).Count();
            }

            var verifier = db.AspNetUsers.Where(c => c.Email == User.Identity.Name && c.RollId == "Verifier").FirstOrDefault();
            if (verifier != null)
            {
                ViewBag.usertype = verifier.RollId;
                ViewBag.ver_q = db.tblQuestions.Where(c => c.verifiedBy == verifier.Email && c.statusInfo == "verified").Count();
                ViewBag.app_q = db.tblQuestions.Where(c => c.verifiedBy == verifier.Email && c.statusInfo == "approved").Count();
                ViewBag.discarded_q = db.tblQuestions.Where(c => c.verifiedBy == verifier.Email && c.statusInfo == "deleted").Count();

                ViewBag.totalQ = db.tblQuestions.Where(c => c.verifiedBy == verifier.Email).Count();

            }

            var approver = db.AspNetUsers.Where(c => c.Email == User.Identity.Name && c.RollId == "Approver").FirstOrDefault();
            if (approver != null)
            {
                ViewBag.usertype = approver.RollId;
               ViewBag.ver_q = db.tblQuestions.Where(c => c.statusInfo == "verified").Count();
                ViewBag.app_q = db.tblQuestions.Where(c => c.approvedBy == approver.Email && c.statusInfo == "approved").Count();
                //ViewBag.discarded_q = db.tblQuestions.Where(c => c. == verifier.Email && c.statusInfo == "deleted").Count();

                //ViewBag.totalQ = db.tblQuestions.Where(c => c.verifiedBy == verifier.Email).Count();

            }

            return View();
        }

        public ActionResult c_showQmainview()
        {
            ViewBag.categoryId = new SelectList(db.tblCategories.Where(t => t.status == true), "categoryID", "categoryName");
            ViewBag.degreeId = new SelectList(db.tblDegrees.Where(t => t.status == true), "degreeID", "degreeName");
            ViewBag.subjectId = new SelectList(db.tblSubjects.Where(t => t.status == true), "subjectID", "subjectName");
            return View();
        }
        [HttpPost]
        public ActionResult c_showQmainview([Bind(Include = "degreeId, subjectId, categoryId")] tblQuestion tblq)
        {
            if (Request.Form["this"] != null)
            {
                int degid = tblq.degreeId;
                int subid = tblq.subjectId;
                int catid = tblq.categoryId;
                return RedirectToAction("c_table", "Questions", new { degid, subid, catid });
            }
            ViewBag.degreeId = new SelectList(db.tblDegrees, "degreeID", "degreeName", tblq.degreeId);
            ViewBag.categoryId = new SelectList(db.tblCategories, "categoryID", "categoryName", tblq.categoryId);
            ViewBag.subjectId = new SelectList(db.tblSubjects, "subjectID", "subjectName", tblq.subjectId);

            return View();
        }


        public ActionResult c_table(int degid, int subid, int catid)
        {


            string current_userID = User.Identity.GetUserId();
            ViewBag.mydata = db.viewForVerfiers.Where(t => t.categoryID == catid && t.subjectID == subid && t.degreeID == degid && t.UserID == current_userID).ToList();
            ViewBag.questiontbl = db.tblQuestions.Where(t => t.UserID == current_userID).ToList();
            ViewBag.cid = catid;
            ViewBag.subid = subid;
            ViewBag.degid = degid;
            return View();
        }


        public ActionResult c_GridRadio(int? id, int? p_catid, int? p_subid, int? p_degid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.specificdata = db.viewForVerfiers.Where(q => q.questionID == id).ToList();

            tblRighAnswer tblRighAnswer = db.tblRighAnswers.Where(t => t.questionId == id).SingleOrDefault();

            if (ViewBag.specificdata == null)
            {
                return HttpNotFound();
            }
            return View(tblRighAnswer);
        }
        [HttpPost]
        public ActionResult c_GridRadio([Bind(Include = "reasonText_C,questionId")] tblRighAnswer tbl)
        {
            int qid = Convert.ToInt32(tbl.questionId);
            if (qid != 0)
            {
                string YourRadioButton = Request.Form["chkOption"];
                int x = Convert.ToInt32(YourRadioButton);
                tblRighAnswer tra = new tblRighAnswer();
                //  int qid = Convert.ToInt32(Session["qid"]);
                var data = db.tblRighAnswers.Where(t => t.questionId == qid).FirstOrDefault();
                data.answerId = x;
                data.reasonText_C = tbl.reasonText_C;
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

            }
            int catid = 0;
            int subid = 0;
            int degid = 0;

            using (db = new DBContext())
            {
                var result = db.tblQuestions.SingleOrDefault(b => b.questionID == qid);
                if (result != null)
                {
                    catid = result.categoryId;
                    subid = result.subjectId;
                    degid = result.degreeId;
                }
            }
            return RedirectToAction("c_table", "Questions", new { catid, subid, degid });

        }


        public ActionResult c_GridRadioEditNot(int? id, int? p_catid, int? p_subid, int? p_degid)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.specificdata = db.viewForVerfiers.Where(q => q.questionID == id).ToList();
            if (ViewBag.specificdata == null)
            {
                return HttpNotFound();
            }
            tblQuestion tbl = db.tblQuestions.Where(q => q.questionID == id).FirstOrDefault();
            return View(tbl);
        }
        [HttpPost]
        public ActionResult c_GridRadioEditNot([Bind(Include = "questionID,categoryId,subjectId,degreeId")] tblQuestion tbl)
        {
            int check = tbl.questionID;
            int catid = tbl.categoryId;
            int subid = tbl.subjectId;
            int degid = tbl.degreeId;

            return RedirectToAction("c_table", "Questions", new { catid, subid, degid });
        }


        public ActionResult c_editQuestionBox(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //ViewBag.question = db.tblQuestions.Where(t => t.questionID == id).ToList().SingleOrDefault();
            tblQuestion tblQuestion = db.tblQuestions.Find(id);
            if (tblQuestion == null)
            {
                return HttpNotFound();
            }
            return View(tblQuestion);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult c_editQuestionBox([Bind(Include = "questionID,UserID,statusInfo,degreeId,questionText,categoryId,subjectId,complexityId,bookId,bookPageNum,createdBy,createdOn,picBool,questionPic")] tblQuestion tblQuestion)
        {
            if (ModelState.IsValid)
            {

                db.Entry(tblQuestion).State = EntityState.Modified;
                db.SaveChanges();

                int id = tblQuestion.questionID;
                int p_catid = tblQuestion.categoryId;
                int p_subid = tblQuestion.subjectId;
                int p_degid = tblQuestion.degreeId;
                //int id = Convert.ToInt32(Session["qid"]);
                //int p_catid = Convert.ToInt32(Session["P_catid"]);
                //int p_subid = Convert.ToInt32(Session["P_subid"]);
                //int p_degid = Convert.ToInt32(Session["P_degid"]);

                return RedirectToAction("c_GridRadio", new { id, p_catid, p_subid, p_degid });
            }
            return View();
        }

        public ActionResult c_editPictureBox(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ViewBag.question = db.tblQuestions.Where(t => t.questionID == id).ToList().SingleOrDefault();
            tblQuestion tblQuestion = db.tblQuestions.Find(id);
            return View(tblQuestion);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult c_editPictureBox([Bind(Include = "questionID,questionText,bookPageNum,createdBy,createdOn,questionPic")] tblQuestion tblQuestion, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                // int id1 = tblQuestion.questionID
                //------------------------for picture-------------------
                string alpha = "";
                if (file != null && file.ContentLength > 0)
                {
                    alpha = tblQuestion.questionID + "_" + Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/questionIMG/"), alpha);
                    file.SaveAs(path);
                }
                //tblQuestion.questionPic = alpha;
                //tblQuestion.picBool = true;
                using (db = new DBContext())
                {
                    var result = db.tblQuestions.SingleOrDefault(b => b.questionID == tblQuestion.questionID);
                    if (result != null)
                    {
                        result.questionPic = alpha;
                        result.picBool = true;
                        db.SaveChanges();
                    }
                }

                //------------------------for picture-------------------


                int id = tblQuestion.questionID;
                int p_catid = tblQuestion.categoryId;
                int p_subid = tblQuestion.subjectId;
                int p_degid = tblQuestion.degreeId;
                return RedirectToAction("c_GridRadio", new { id, p_catid, p_subid, p_degid });
            }
            return View();
        }


        public ActionResult c_editPictureBoxAns(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tblAnwer tblAnwer = db.tblAnwers.Find(id);
            return View(tblAnwer);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult c_editPictureBoxAns([Bind(Include = "answerID,questionId,answerText1,createdOn,answerPic")] tblAnwer tblAnwer, HttpPostedFileBase file1)
        {

            if (ModelState.IsValid)
            {

                //------------------------for picture-------------------
                string alpha1 = "";
                if (file1 != null && file1.ContentLength > 0)
                {
                    alpha1 = tblAnwer.answerID + "_" + Path.GetFileName(file1.FileName);
                    var path = Path.Combine(Server.MapPath("~/questionIMG/"), alpha1);
                    file1.SaveAs(path);
                }
                //tblQuestion.questionPic = alpha;
                //tblQuestion.picBool = true;
                using (db = new DBContext())
                {
                    var result = db.tblAnwers.SingleOrDefault(b => b.answerID == tblAnwer.answerID);
                    if (result != null)
                    {
                        result.answerPic = alpha1;
                        result.ansPicBool = true;
                        db.SaveChanges();
                    }
                }

                //------------------------for picture-------------------
                //var Forqid = db.tblQuestions.Where(q => q.questionID == tblAnwer.questionId).FirstOrDefault();
                int p_catid = 0;
                int p_subid = 0;
                int p_degid = 0;
                int id = 0;
                using (db = new DBContext())
                {
                    var result = db.tblQuestions.SingleOrDefault(b => b.questionID == tblAnwer.questionId);
                    if (result != null)
                    {
                        p_catid = result.categoryId;
                        p_subid = result.subjectId;
                        p_degid = result.degreeId;
                        id = result.questionID;
                        //result.answerPic = alpha1;
                        //result.ansPicBool = true;
                        //db.SaveChanges();
                    }
                }

                //int p_catid = Convert.ToInt32(Session["P_catid"]);
                //int p_subid = Convert.ToInt32(Session["P_subid"]);
                //int p_degid = Convert.ToInt32(Session["P_degid"]);

                return RedirectToAction("c_GridRadio", new { id, p_catid, p_subid, p_degid });
            }
            return View();
        }



        public ActionResult c_editAnswerBox(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAnwer tblAnwer = db.tblAnwers.Find(id);
            return View(tblAnwer);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult c_editAnswerBox([Bind(Include = "answerID,questionId,answerText1,createdOn,answerPic,ansPicBool")] tblAnwer tblAnwer)
        {
            if (ModelState.IsValid)
            {

                db.Entry(tblAnwer).State = EntityState.Modified;
                db.SaveChanges();
                int p_catid = 0;
                int p_subid = 0;
                int p_degid = 0;
                int id = 0;
                using (db = new DBContext())
                {
                    var result = db.tblQuestions.SingleOrDefault(b => b.questionID == tblAnwer.questionId);
                    if (result != null)
                    {
                        p_catid = result.categoryId;
                        p_subid = result.subjectId;
                        p_degid = result.degreeId;
                        id = result.questionID;

                    }
                }

                return RedirectToAction("c_GridRadio", new { id, p_catid, p_subid, p_degid });
            }
            return View(tblAnwer);
        }


        //working here
        public ActionResult c_IndexRadio(int questionID)
        {

            var tblQuestions = db.tblQuestions.Where(x => x.questionID == questionID).Include(t => t.tblCategory).Include(t => t.tblComplexity).Include(t => t.tblDegree).Include(t => t.tblSubject);
            //ViewBag.alldata = db.tblViewDatas.ToList();
            return PartialView(tblQuestions.ToList());

        }

        public ActionResult c_AddQmainview()
        {
            ViewBag.degreeId = new SelectList(db.tblDegrees.Where(t => t.status == true), "degreeID", "degreeName");
            ViewBag.subjectId = new SelectList(db.tblSubjects.Where(t => t.status == true), "subjectID", "subjectName");
            ViewBag.categoryId = new SelectList(db.tblCategories.Where(t => t.status == true), "categoryID", "categoryName");

            ViewBag.complexityId = new SelectList(db.tblComplexities, "complexityID", "complexityLevel");
            ViewBag.bookId = new SelectList(db.tblBooks, "bookID", "bookName");
            ViewBag.bookIDauthor = new SelectList(db.tblBooks, "bookID", "bookAuthor");
            Session["qid"] = 0;
            Session["counter"] = 0;
            Session.Remove("myid");
            ViewBag.count = Convert.ToInt32(Session["counter"]).ToString();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult c_AddQmainview([Bind(Include = "questionID,reasonCreator,degreeId,subjectId,categoryId,complexityId,bookId,bookPageNum,questionText,answerText,createdBy,createdOn,updatedBy,updatedOn,status,verifiedBy,verifiedDate,approvedBy,approvedDate,statusInfo,UserID,questionPic,picBool,answerPic,iteratequestions,scheckit,iterateval")] tblQuestion tblQuestion, HttpPostedFileBase file, HttpPostedFileBase file1)
        {
            if (ModelState.IsValid)
            {
                if (tblQuestion.questionText != null && tblQuestion.answerText != null && Convert.ToInt32(Session["qid"]) == 0)
                //if (tblQuestion.questionText != null && tblQuestion.answerText != null)
                {
                    string userName = User.Identity.Name;
                    tblQuestion.UserID = User.Identity.GetUserId();
                    tblQuestion.createdBy = userName;
                    tblQuestion.createdOn = DateTime.Now;
                    tblQuestion.statusInfo = "pending";
                    //tblQuestion.iteratequestions = true;
                    //tblQuestion.scheckit = tblQuestion.scheckit + 1;
                    tblQuestion.scheckit = "val2";
                   

                    //------------------------for Question picture-------------------
                    string alpha = "";
                    if (file != null && file.ContentLength > 0)
                    {
                        alpha = tblQuestion.questionID + "_" + Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/questionIMG/"), alpha);
                        file.SaveAs(path);
                    }
                    tblQuestion.questionPic = alpha;
                    if (file != null)
                    {
                        tblQuestion.picBool = true;
                    }
                    else
                    {
                        tblQuestion.picBool = false;
                    }
                    //------------------------end Question picture-------------------
                    db.tblQuestions.Add(tblQuestion);
                    db.SaveChanges();
                    // tblQuestion.q_id = tblQuestion.questionID;
                    Session["qid"] = tblQuestion.questionID;
                    int x = tblQuestion.questionID;
                    //ViewBag.qid = tblQuestion.questionID;
                    Session["myid"] = tblQuestion.questionID;
                    tblAnwer tblAnwer = new tblAnwer();
                    tblAnwer.questionId = tblQuestion.questionID;
                    tblAnwer.answerText1 = tblQuestion.answerText;
                    tblAnwer.createdBy = userName;
                    tblAnwer.createdOn = DateTime.Now;

                    //db.tblAnwers.Add(tblAnwer);
                    //db.SaveChanges();
                    int ansid = tblAnwer.answerID;
                    //------------------------for ans picture-------------------
                    string alpha1 = "";
                    if (file1 != null && file1.ContentLength > 0)
                    {
                        alpha1 = tblAnwer.answerID + "_" + Path.GetFileName(file1.FileName);
                        var path = Path.Combine(Server.MapPath("~/questionIMG/"), alpha1);
                        file1.SaveAs(path);
                    }
                    tblAnwer.answerPic = alpha1;
                    if (file1 != null)
                    {
                        tblAnwer.ansPicBool = true;
                    }
                    else
                    {
                        tblAnwer.ansPicBool = false;
                    }
                    //------------------------end ans picture-------------------
                    db.tblAnwers.Add(tblAnwer);
                    db.SaveChanges();

                }
                else if (tblQuestion.questionText != null && tblQuestion.answerText != null && Convert.ToInt32(Session["qid"]) != 0)
                {
                   
                    string userName = User.Identity.Name;
                    tblAnwer tblAnwer = new tblAnwer();
                    tblAnwer.questionId = Convert.ToInt32(Session["qid"]);
                    tblAnwer.answerText1 = tblQuestion.answerText;
                    tblAnwer.createdOn = DateTime.Now;
                    tblAnwer.createdBy = userName;
                    string alpha1 = "";
                    if (file1 != null && file1.ContentLength > 0)
                    {
                        alpha1 = tblAnwer.answerID + "_" + Path.GetFileName(file1.FileName);
                        var path = Path.Combine(Server.MapPath("~/questionIMG/"), alpha1);
                        file1.SaveAs(path);
                    }
                    tblAnwer.answerPic = alpha1;
                    if (file1 != null)
                    {
                        tblAnwer.ansPicBool = true;
                    }
                    else
                    {
                        tblAnwer.ansPicBool = false;
                    }
                    //------------------------end ans picture-------------------

                    db.tblAnwers.Add(tblAnwer);
                    db.SaveChanges();

                }


                return RedirectToAction("c_IndexRadio", new { questionID = Convert.ToInt32(Session["qid"]) });
                //return RedirectToAction("c_IndexRadio", new { questionID = tblQuestion.questionID });
            }

            if (Request.Form["submitbutton1"] != null)
            {
                string YourRadioButton = Request.Form["chkOption"];
                int x = Convert.ToInt32(YourRadioButton);
                tblRighAnswer tra = new tblRighAnswer();
                tra.questionId = Convert.ToInt32(Session["qid"]);
                //tra.questionId = tblQuestion.questionID;
                tra.answerid_C = x;
                tra.answerId = x;
                string reason = Request.Form["reasonText"];
                tra.reasonText_C = reason;
                db.tblRighAnswers.Add(tra);
                db.SaveChanges();
                //tblQuestion.statusInfo = "Pending";
                //db.tblQuestions.Add(tblQuestion);
                //db.SaveChanges();
                //Session["myid"] = 0;
                //Session.Remove("myid");
                //return RedirectToAction("Index");
                Session["counter"] = Convert.ToInt32(Session["counter"]) + 1;
                ViewBag.count = Convert.ToInt32(Session["counter"]).ToString();
                Session.Remove("qid");

            }

            // Session.Remove("myid");

            ViewBag.categoryId = new SelectList(db.tblCategories.Where(t => t.status == true), "categoryID", "categoryName", tblQuestion.categoryId);
            ViewBag.degreeId = new SelectList(db.tblDegrees.Where(t => t.status == true), "degreeID", "degreeName", tblQuestion.degreeId);
            ViewBag.subjectId = new SelectList(db.tblSubjects.Where(t => t.status == true), "subjectID", "subjectName", tblQuestion.subjectId);


            ViewBag.complexityId = new SelectList(db.tblComplexities, "complexityID", "complexityLevel", tblQuestion.complexityId);
            return Json(tblQuestion);
        }


        //---------------------------------------------Creator------------ Section--------- Ends------------------------------------------


        //---------------------------------------------admin------------ Section--------- Starts------------------------------------------

        public ActionResult admin_showDropdowns()
        {
            ViewBag.categoryId = new SelectList(db.tblCategories.Where(t => t.status == true), "categoryID", "categoryName");
            ViewBag.degreeId = new SelectList(db.tblDegrees.Where(t => t.status == true), "degreeID", "degreeName");
            ViewBag.subjectId = new SelectList(db.tblSubjects.Where(t => t.status == true), "subjectID", "subjectName");
            return View();

        }
        [HttpPost]
        public ActionResult admin_showDropdowns([Bind(Include = "degreeId, subjectId, categoryId")] tblQuestion tblq)
        {
            var abc = db.viewForVerfiers.Where(t => t.categoryID == tblq.categoryId && t.subjectID == tblq.subjectId && t.degreeID == tblq.degreeId).FirstOrDefault();
            if (Request.Form["this"] != null)
            {

                var catId = tblq.categoryId;
                var subId = tblq.subjectId;
                var degId = tblq.degreeId;

                return RedirectToAction("admin_QuestionTable", "Questions", new { catid = catId, subid = subId, degid = degId });
            }
            ViewBag.degreeId = new SelectList(db.tblDegrees.Where(t => t.status == true), "degreeID", "degreeName", tblq.degreeId);
            ViewBag.categoryId = new SelectList(db.tblCategories.Where(t => t.status == true), "categoryID", "categoryName", tblq.categoryId);
            ViewBag.subjectId = new SelectList(db.tblSubjects.Where(t => t.status == true), "subjectID", "subjectName", tblq.subjectId);

            return View();
        }


        public ActionResult admin_QuestionTable(int degid, int subid, int catid)
        {
            if (degid != 0 && subid == 0 && catid == 0)
            {
                ViewBag.mydata = db.viewForVerfiers.Where(t => t.degreeID == degid).ToList();
                var degname = db.tblDegrees.Where(c => c.degreeID == degid).SingleOrDefault();
                ViewBag.Heading = degname.degreeName;

            }
            else if (degid != 0 && subid != 0 && catid == 0)
            {
                ViewBag.mydata = db.viewForVerfiers.Where(t => t.degreeID == degid && t.subjectID == subid).ToList();

                var degname = db.tblDegrees.Where(c => c.degreeID == degid).SingleOrDefault();
                ViewBag.Heading = degname.degreeName;


                var subname = db.tblSubjects.Where(c => c.subjectID == subid).SingleOrDefault();
                ViewBag.degHeading = subname.subjectName;

            }
            else if (degid != 0 && subid != 0 && catid != 0)
            {
                ViewBag.mydata = db.viewForVerfiers.Where(t => t.degreeID == degid && t.subjectID == subid && t.categoryID == catid).ToList();

            }

            ViewBag.questiontbl = db.tblQuestions.Where(t => t.status == true).ToList();
            ViewBag.cid = catid;
            ViewBag.subid = subid;
            ViewBag.degid = degid;
            return View();

        }


        public ActionResult admin_ViewtotalQuestion()
        {
            ViewBag.mylist = db.tblUserProfiles.Where(c => c.status == true && c.userType == "Creator" || c.userType == "Verifier" || c.userType == "Approver").ToList();
            ViewBag.userQ_count = db.tblQuestions.Where(c => c.statusInfo != null).ToList();
            return View();

        }
        //---------------------------------------------admin------------ Section--------- Ends------------------------------------------


        //---------------------------------------------Verifier------------ Section--------- Starts------------------------------------------




        public ActionResult v_editPictureBox(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblQuestion tblQuestion = db.tblQuestions.Find(id);
            return View(tblQuestion);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult v_editPictureBox([Bind(Include = "questionID,categoryId,subjectId,degreeId,complexityId,questionText,bookPageNum,createdBy,createdOn,questionPic")] tblQuestion tblQuestion, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                // int id1 = tblQuestion.questionID
                //------------------------for picture-------------------
                string alpha = "";
                if (file != null && file.ContentLength > 0)
                {
                    alpha = tblQuestion.questionID + "_" + Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/questionIMG/"), alpha);
                    file.SaveAs(path);
                }

                using (db = new DBContext())
                {
                    var result = db.tblQuestions.SingleOrDefault(b => b.questionID == tblQuestion.questionID);
                    if (result != null)
                    {
                        result.updatedOn = DateTime.Now;
                        result.updatedBy = User.Identity.Name;
                        result.questionPic = alpha;
                        result.picBool = true;
                        db.SaveChanges();
                    }
                }

                //------------------------for picture-------------------


                int id = tblQuestion.questionID;
                int p_catid = tblQuestion.categoryId;
                int p_subid = tblQuestion.subjectId;
                int p_degid = tblQuestion.degreeId;
                return RedirectToAction("v_GridRadio", new { id, p_catid, p_subid, p_degid });
            }
            return View();

        }


        public ActionResult v_editPictureBoxAns(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tblAnwer tblAnwer = db.tblAnwers.Find(id);
            return View(tblAnwer);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult v_editPictureBoxAns([Bind(Include = "answerID,questionId,answerText1,createdOn,answerPic")] tblAnwer tblAnwer, HttpPostedFileBase file1)
        {

            if (ModelState.IsValid)
            {

                //------------------------for picture-------------------
                string alpha1 = "";
                if (file1 != null && file1.ContentLength > 0)
                {
                    alpha1 = tblAnwer.answerID + "_" + Path.GetFileName(file1.FileName);
                    var path = Path.Combine(Server.MapPath("~/questionIMG/"), alpha1);
                    file1.SaveAs(path);
                }

                using (db = new DBContext())
                {
                    var result = db.tblAnwers.SingleOrDefault(b => b.answerID == tblAnwer.answerID);
                    if (result != null)
                    {
                        result.updatedBy = User.Identity.Name;
                        result.updatedOn = DateTime.Now;
                        result.answerPic = alpha1;
                        result.ansPicBool = true;
                        db.SaveChanges();
                    }
                }

                //------------------------for picture-------------------

                int id = 0;
                int p_catid = 0;
                int p_subid = 0;
                int p_degid = 0;

                using (db = new DBContext())
                {
                    var result = db.tblQuestions.SingleOrDefault(b => b.questionID == tblAnwer.questionId);
                    if (result != null)
                    {
                        id = result.questionID;
                        p_catid = result.categoryId;
                        p_subid = result.subjectId;
                        p_degid = result.degreeId;

                    }
                }

                return RedirectToAction("v_GridRadio", new { id, p_catid, p_subid, p_degid });
            }
            return View();
        }


        public ActionResult v_mainview()
        {
            ViewBag.categoryId = new SelectList(db.tblCategories.Where(c => c.status == true), "categoryID", "categoryName");
            ViewBag.degreeId = new SelectList(db.tblDegrees.Where(c => c.status == true), "degreeID", "degreeName");
            ViewBag.subjectId = new SelectList(db.tblSubjects.Where(c => c.status == true), "subjectID", "subjectName");
            return View();
        }

        [HttpPost]
        public ActionResult v_mainview([Bind(Include = "degreeId, subjectId, categoryId")] tblQuestion tblq)
        {
            var abc = db.viewForVerfiers.Where(t => t.categoryID == tblq.categoryId && t.subjectID == tblq.subjectId && t.degreeID == tblq.degreeId).FirstOrDefault();
            if (Request.Form["this"] != null)
            {
                var catId = abc.categoryID;
                var subId = abc.subjectID;
                var degId = abc.degreeID;

                return RedirectToAction("v_table", "Questions", new { catid = catId, subid = subId, degid = degId });
            }
            ViewBag.degreeId = new SelectList(db.tblDegrees.Where(c => c.status == true), "degreeID", "degreeName", tblq.degreeId);
            ViewBag.categoryId = new SelectList(db.tblCategories.Where(c => c.status == true), "categoryID", "categoryName", tblq.categoryId);
            ViewBag.subjectId = new SelectList(db.tblSubjects.Where(c => c.status == true), "subjectID", "subjectName", tblq.subjectId);

            return View();
        }

        public ActionResult v_table(int catid, int subid, int degid)
        {
            ViewBag.mydata = db.viewForVerfiers.Where(t => t.categoryID == catid && t.subjectID == subid && t.degreeID == degid).ToList();
            ViewBag.questiontbl = db.tblQuestions.ToList();
            ViewBag.cid = catid;
            ViewBag.subid = subid;
            ViewBag.degid = degid;
            return View();
        }

        public ActionResult v_Details(int idq, int ida, int c, int s, int d)
        {
            // int x1 = id;
            ViewBag.detail = db.viewForVerfiers.Where(t => t.rightID == ida).ToList();
            var bookid = db.tblQuestions.Where(t => t.questionID == idq).Select(t => t.bookId).FirstOrDefault();
            int i = Convert.ToInt32(bookid);
            ViewBag.bookdetail = db.tblBooks.Where(t => t.bookID == i).ToList();

            var complexid = db.tblQuestions.Where(t => t.questionID == idq).Select(t => t.complexityId).FirstOrDefault();
            int j = Convert.ToInt32(complexid);
            ViewBag.complexid = db.tblComplexities.Where(t => t.complexityID == j).ToList();

            ViewBag.catid = c;
            ViewBag.subid = s;
            ViewBag.degid = d;
            return View();
        }
        [HttpPost]
        public ActionResult v_Details()
        {
            return View();
        }

        public ActionResult v_verifyClick(int id, int id2)
        {
            //id2 is answerid can be needed further
            tblQuestion tbq = new tblQuestion();
            var data = db.tblQuestions.Where(t => t.questionID == id).FirstOrDefault();
            if (data != null)
            {
                data.verifiedBy = User.Identity.Name;
                data.verifiedDate = DateTime.Now;
                data.statusInfo = "verified";
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
            }


            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult v_discardClick(int id, int id2)
        {
            tblQuestion tbq = new tblQuestion();
            var data = db.tblQuestions.Where(t => t.questionID == id).FirstOrDefault();
            if (data != null)
            {
                data.verifiedBy = User.Identity.Name;
                data.verifiedDate = DateTime.Now;
                data.statusInfo = "deleted";
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());

            //return View();
        }


        public ActionResult v_GridRadio(int? id, int? p_catid, int? p_subid, int? p_degid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.specificdata = db.viewForVerfiers.Where(q => q.questionID == id).ToList();
            tblQuestion tblQuestion = db.tblQuestions.Where(q => q.questionID == id).FirstOrDefault();

            if (ViewBag.specificdata == null)
            {
                return HttpNotFound();
            }
            return View(tblQuestion);
        }

        [HttpPost]
        public ActionResult v_GridRadio([Bind(Include = "questionID,categoryId,subjectId,degreeId")] tblQuestion tbl)
        {
            //if (Session["qid"] != null)
            if (tbl.questionID != 0)
            {
                string YourRadioButton = Request.Form["chkOption"];
                int x = Convert.ToInt32(YourRadioButton);
                // int qid = Convert.ToInt32(Session["qid"]);
                var data = db.tblRighAnswers.Where(t => t.questionId == tbl.questionID).FirstOrDefault();
                data.answerId = x;
                data.answerid_V = x;
                data.reasonText_V = Request.Form["reasonTextv"].ToString();
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                var data1 = db.tblQuestions.Where(t => t.questionID == tbl.questionID).FirstOrDefault();
                if (data1 != null)
                {
                    data1.verifiedDate = null;
                    data1.statusInfo = "pending";
                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }

            int catid = tbl.categoryId;

            int subid = tbl.subjectId;
            int degid = tbl.degreeId;

            return RedirectToAction("v_table", "Questions", new { catid, subid, degid });
            //return Redirect(Request.UrlReferrer.ToString());

        }


        public ActionResult v_GridRadioEditNot(int? id, int? p_catid, int? p_subid, int? p_degid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.specificdata = db.viewForVerfiers.Where(q => q.questionID == id).ToList();
            tblQuestion tblQuestion = db.tblQuestions.Where(q => q.questionID == id).FirstOrDefault();

            if (ViewBag.specificdata == null)
            {
                return HttpNotFound();
            }
            return View(tblQuestion);
        }
        [HttpPost]
        public ActionResult v_GridRadioEditNot([Bind(Include = "questionID,categoryId,subjectId,degreeId")] tblQuestion tbl)
        {
            int catid = tbl.categoryId;
            int subid = tbl.subjectId;
            int degid = tbl.degreeId;
            return RedirectToAction("v_table", "Questions", new { catid, subid, degid });


            // return View();
        }



        public ActionResult v_editQuestionBox(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tblQuestion tblQuestion = db.tblQuestions.Find(id);
            return View(tblQuestion);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult v_editQuestionBox([Bind(Include = "questionID,statusInfo,UserID,questionText,degreeId,categoryId,subjectId,complexityId,bookId,bookPageNum,createdBy,createdOn,picBool,questionPic")] tblQuestion tblQuestion)
        {
            if (ModelState.IsValid)
            {
                //  tblQuestion.statusInfo = "pending";
                tblQuestion.updatedOn = DateTime.Now;
                tblQuestion.updatedBy = User.Identity.Name;
                db.Entry(tblQuestion).State = EntityState.Modified;
                db.SaveChanges();
                int id = tblQuestion.questionID;
                int p_catid = tblQuestion.categoryId;
                int p_subid = tblQuestion.subjectId;
                int p_degid = tblQuestion.degreeId;
                return RedirectToAction("v_GridRadio", new { id, p_catid, p_subid, p_degid });
            }
            return View();
        }

        public ActionResult v_editAnswerBox(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAnwer tblAnwer = db.tblAnwers.Find(id);
            return View(tblAnwer);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult v_editAnswerBox([Bind(Include = "answerID,questionId,createdOn,answerPic,ansPicBool,answerText1")] tblAnwer tblAnwer)
        {
            if (ModelState.IsValid)
            {
                tblAnwer.updatedBy = User.Identity.Name;
                tblAnwer.updatedOn = DateTime.Now;
                db.Entry(tblAnwer).State = EntityState.Modified;
                db.SaveChanges();
                int id = 0;
                int p_catid = 0;
                int p_subid = 0;
                int p_degid = 0;

                using (db = new DBContext())
                {
                    var result = db.tblQuestions.SingleOrDefault(b => b.questionID == tblAnwer.questionId);
                    if (result != null)
                    {
                        id = result.questionID;
                        p_catid = result.categoryId;
                        p_subid = result.subjectId;
                        p_degid = result.degreeId;
                    }
                }

                return RedirectToAction("v_GridRadio", new { id, p_catid, p_subid, p_degid });
            }
            return View(tblAnwer);
        }


        //---------------------------------------------Verifier------------ Section--------- Ends------------------------------------------


        //---------------------------------------------Approver------------ Section--------- Starts------------------------------------------



        public ActionResult a_editPictureBox(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tblQuestion tblQuestion = db.tblQuestions.Find(id);
            return View(tblQuestion);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult a_editPictureBox([Bind(Include = "questionID,categoryId,subjectId,complexityId,degreeId,bookPageNum,createdBy,createdOn,verifiedBy,verifiedDate,questionText,questionPic")] tblQuestion tblQuestion, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                //------------------------for picture-------------------
                string alpha = "";
                if (file != null && file.ContentLength > 0)
                {
                    alpha = tblQuestion.questionID + "_" + Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/questionIMG/"), alpha);
                    file.SaveAs(path);
                }

                using (db = new DBContext())
                {
                    var result = db.tblQuestions.SingleOrDefault(b => b.questionID == tblQuestion.questionID);
                    if (result != null)
                    {
                        result.updatedOn = DateTime.Now;
                        result.updatedBy = User.Identity.Name;
                        result.questionPic = alpha;
                        result.picBool = true;
                        db.SaveChanges();
                    }
                }

                //------------------------for picture-------------------

                int id = tblQuestion.questionID;
                int p_catid = tblQuestion.categoryId;
                int p_subid = tblQuestion.subjectId;
                int p_degid = tblQuestion.degreeId;

                return RedirectToAction("a_GridRadioEditable", new { id, p_catid, p_subid, p_degid });
            }
            return View();

        }



        public ActionResult a_editPictureBoxAns(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAnwer tblAnwer = db.tblAnwers.Find(id);
            return View(tblAnwer);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult a_editPictureBoxAns([Bind(Include = "answerID,questionId,answerText1,createdOn,answerPic")] tblAnwer tblAnwer, HttpPostedFileBase file1)
        {
            if (ModelState.IsValid)
            {

                //------------------------for picture-------------------
                string alpha1 = "";
                if (file1 != null && file1.ContentLength > 0)
                {
                    alpha1 = tblAnwer.answerID + "_" + Path.GetFileName(file1.FileName);
                    var path = Path.Combine(Server.MapPath("~/questionIMG/"), alpha1);
                    file1.SaveAs(path);
                }

                using (db = new DBContext())
                {
                    var result = db.tblAnwers.SingleOrDefault(b => b.answerID == tblAnwer.answerID);
                    if (result != null)
                    {
                        result.updatedBy = User.Identity.Name;
                        result.updatedOn = DateTime.Now;
                        result.answerPic = alpha1;
                        result.ansPicBool = true;
                        db.SaveChanges();
                    }
                }

                //------------------------for picture-------------------
                int id = 0;
                int p_catid = 0;
                int p_subid = 0;
                int p_degid = 0;

                using (db = new DBContext())
                {
                    var result = db.tblQuestions.Where(q => q.questionID == tblAnwer.questionId).SingleOrDefault();
                    if (result != null)
                    {
                        id = result.questionID;
                        p_catid = result.categoryId;
                        p_subid = result.subjectId;
                        p_degid = result.degreeId;
                    }
                }
                //int id = Convert.ToInt32(Session["qid"]);
                //    int id = Convert.ToInt32(tblAnwer.questionId);
                //int p_catid = Convert.ToInt32(Session["P_catid"]);
                //int p_subid = Convert.ToInt32(Session["P_subid"]);
                //int p_degid = Convert.ToInt32(Session["P_degid"]);

                return RedirectToAction("a_GridRadioEditable", new { id, p_catid, p_subid, p_degid });
            }
            return View();
        }

        public ActionResult a_mainview()
        {
            ViewBag.categoryId = new SelectList(db.tblCategories.Where(c => c.status == true), "categoryID", "categoryName");
            ViewBag.degreeId = new SelectList(db.tblDegrees.Where(c => c.status == true), "degreeID", "degreeName");
            ViewBag.subjectId = new SelectList(db.tblSubjects.Where(c => c.status == true), "subjectID", "subjectName");
            return View();

        }
        [HttpPost]
        public ActionResult a_mainview([Bind(Include = "degreeId, subjectId, categoryId")] tblQuestion tblq)
        {
            var aprove = db.viewForVerfiers.Where(t => t.categoryID == tblq.categoryId && t.subjectID == tblq.subjectId && t.degreeID == tblq.degreeId).FirstOrDefault();
            if (Request.Form["this"] != null)
            {
                var catId = aprove.categoryID;
                var subId = aprove.subjectID;
                var degId = aprove.degreeID;

                return RedirectToAction("a_table", "Questions", new { catid = catId, subid = subId, degid = degId });
                //return RedirectToAction("LoadData", "Questions", new { catid = catId, subid = subId, degid = degId });
                //return RedirectToAction("GetPaggedData", "Questions", new { catid = catId, subid = subId, degid = degId });
            }
            ViewBag.degreeId = new SelectList(db.tblDegrees.Where(c => c.status == true), "degreeID", "degreeName", tblq.degreeId);
            ViewBag.categoryId = new SelectList(db.tblCategories.Where(c => c.status == true), "categoryID", "categoryName", tblq.categoryId);
            ViewBag.subjectId = new SelectList(db.tblSubjects.Where(c => c.status == true), "subjectID", "subjectName", tblq.subjectId);

            return View();
        }


        public ActionResult a_table(int catid, int subid, int degid)
        {
            ViewBag.mydata = db.viewForVerfiers.Where(t => t.categoryID == catid && t.subjectID == subid && t.degreeID == degid && t.verifiedDate != null).ToList();
            //  Session["countdata"] = db.viewForVerfiers.Where(t => t.categoryID == catid && t.subjectID == subid && t.degreeID == degid && t.verifiedDate != null).Count();
            ViewBag.questiontbl = db.tblQuestions.ToList();
            ViewBag.cid = catid;
            ViewBag.subid = subid;
            ViewBag.degid = degid;
            return View();

        }

        public ActionResult a_Details(int idq, int ida, int c, int s, int d)
        {
            // int x1 = id;
            ViewBag.detail = db.viewForVerfiers.Where(t => t.rightID == ida).ToList();
            var bookid = db.tblQuestions.Where(t => t.questionID == idq).Select(t => t.bookId).FirstOrDefault();
            int i = Convert.ToInt32(bookid);
            ViewBag.bookdetail = db.tblBooks.Where(t => t.bookID == i).ToList();

            var complexid = db.tblQuestions.Where(t => t.questionID == idq).Select(t => t.complexityId).FirstOrDefault();
            int j = Convert.ToInt32(complexid);
            ViewBag.complexid = db.tblComplexities.Where(t => t.complexityID == j).ToList();

            ViewBag.catid = c;
            ViewBag.subid = s;
            ViewBag.degid = d;
            return View();
        }
        [HttpPost]
        public ActionResult a_Details()
        {
            return View();
        }

        public ActionResult a_approveClick(int id)
        {
            tblQuestion tbq = new tblQuestion();
            var data = db.tblQuestions.Where(t => t.questionID == id).FirstOrDefault();
            if (data != null)
            {
                data.approvedBy = User.Identity.Name;
                data.approvedDate = DateTime.Now;
                data.statusInfo = "approved";
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
            }

            //return Json(JsonRequestBehavior.AllowGet);
            //return View();
            return Redirect(Request.UrlReferrer.ToString());
        }


        public ActionResult a_GridRadioEditableNot(int? id, int? p_catid, int? p_subid, int? p_degid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.specificdata = db.viewForVerfiers.Where(q => q.questionID == id).ToList();
            tblQuestion tblQuestion = db.tblQuestions.Where(q => q.questionID == id).FirstOrDefault();

            if (ViewBag.specificdata == null)
            {
                return HttpNotFound();
            }
            return View(tblQuestion);
        }
        [HttpPost]
        public ActionResult a_GridRadioEditableNot([Bind(Include = "questionID,degreeId, subjectId, categoryId")] tblQuestion tblq)
        {
            int catid = tblq.categoryId;
            int subid = tblq.subjectId;
            int degid = tblq.degreeId;

            return RedirectToAction("a_table", "Questions", new { catid, subid, degid });
        }



        public ActionResult a_GridRadioEditable(int? id, int? p_catid, int? p_subid, int? p_degid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.specificdata = db.viewForVerfiers.Where(q => q.questionID == id).ToList();
            tblQuestion tblQuestion = db.tblQuestions.Where(q => q.questionID == id).FirstOrDefault();
            if (ViewBag.specificdata == null)
            {
                return HttpNotFound();
            }
            return View(tblQuestion);
        }
        [HttpPost]
        public ActionResult a_GridRadioEditable([Bind(Include = "questionID,degreeId, subjectId, categoryId")] tblQuestion tblq)
        {
            //if (Session["apprID"] != null)
            if (tblq.questionID != 0)
            {
                string YourRadioButton = Request.Form["chkOption"];
                int x = Convert.ToInt32(YourRadioButton);
                tblRighAnswer tra = new tblRighAnswer();
                // int qid = Convert.ToInt32(Session["apprID"]);
                var data = db.tblRighAnswers.Where(t => t.questionId == tblq.questionID).FirstOrDefault();
                data.answerId = x;
                data.answerid_A = x;
                data.reasonText_A = Request.Form["reasonTexta"].ToString();
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
            }
            int catid = tblq.categoryId;
            int subid = tblq.subjectId;
            int degid = tblq.degreeId;

            //return Redirect(Request.UrlReferrer.ToString());
            return RedirectToAction("a_table", "Questions", new { catid, subid, degid });

        }



        public ActionResult a_editQuestionBox(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tblQuestion tblQuestion = db.tblQuestions.Find(id);
            return View(tblQuestion);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult a_editQuestionBox([Bind(Include = "questionID,UserID,statusInfo,questionText,degreeId,categoryId,subjectId,complexityId,bookId,bookPageNum,createdBy,createdOn,picBool,questionPic,verifiedBy,verifiedDate")] tblQuestion tblQuestion)
        {
            if (ModelState.IsValid)
            {
                //tblQuestion.statusInfo = "";
                tblQuestion.updatedBy = User.Identity.Name;
                tblQuestion.updatedOn = DateTime.Now;
                db.Entry(tblQuestion).State = EntityState.Modified;
                db.SaveChanges();
                int id = tblQuestion.questionID;
                int p_catid = tblQuestion.categoryId;
                int p_subid = tblQuestion.subjectId;
                int p_degid = tblQuestion.degreeId;


                return RedirectToAction("a_GridRadioEditable", new { id, p_catid, p_subid, p_degid });
            }
            return View();
        }


        //here working
        public ActionResult a_editAnswerBox(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAnwer tblAnwer = db.tblAnwers.Find(id);
            return View(tblAnwer);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult a_editAnswerBox([Bind(Include = "answerID,questionId,answerText1,createdOn,createdBy,answerPic,ansPicBool")] tblAnwer tblAnwer)
        {
            if (ModelState.IsValid)
            {
                tblAnwer.updatedBy = User.Identity.Name;
                tblAnwer.updatedOn = DateTime.Now;
                db.Entry(tblAnwer).State = EntityState.Modified;
                db.SaveChanges();

                int id = 0;
                int p_catid = 0;
                int p_subid = 0;
                int p_degid = 0;

                using (db = new DBContext())
                {
                    var result = db.tblQuestions.Where(q => q.questionID == tblAnwer.questionId).FirstOrDefault();
                    if (result != null)
                    {
                        id = result.questionID;
                        p_catid = result.categoryId;
                        p_subid = result.subjectId;
                        p_degid = result.degreeId;
                    }
                }
                return RedirectToAction("a_GridRadioEditable", new { id, p_catid, p_subid, p_degid });
            }
            return View(tblAnwer);



        }


        //---------------------------------------------Approver------------ Section--------- Ends------------------------------------------

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
