using SDCOC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDCOC.Controllers
{
    public class MessagesController : BaseController
    {
        //
        // GET: /Messages/
        SDCOCEntities _db = new SDCOCEntities();

        Common cs = new Common();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult GetMessagesUserList(string serachword = "", int PageNo = 1, int PageSize = 10, string SortColumn = "CreatedDate", string SortOrder = "DESC")
        {

            List<UserDetails> lstUserDetails = new List<UserDetails>();

            lstUserDetails = (from s in _db.GetAll_tbl_Users_Details(serachword, PageNo, PageSize, SortColumn, SortOrder)
                              select new UserDetails
                              {
                                  UserId = s.UserId,
                                  EmailId = s.EmailId,
                                  Password = s.Password,
                                  Firstname = s.Firstname,
                                  Lastname = s.Lastname,
                                  MobileNumber = s.MobileNumber,
                                  Birthdate = s.Birthdate,
                                  Address = s.Address,
                                  IsActive = s.IsActive,
                                  IsDeleted = s.IsDeleted,
                                  IsReceiveEmailSMS = s.IsReceiveEmailSMS,
                                  ReceivedOTP = s.ReceivedOTP,
                                  CreatedDate = s.CreatedDate,
                                  ModifiedDate = s.ModifiedDate,
                                  ROWNUM = s.ROWNUM,
                                  TotalCount = s.TotalCount
                              }).ToList();

            ViewBag.serachword = serachword;
            ViewBag.PageNo = PageNo;
            ViewBag.PageSize = PageSize;
            ViewBag.SortColumn = SortColumn;
            ViewBag.SortOrder = SortOrder;
            ViewBag.StartNumber = 1;
            if (lstUserDetails.Count > 0)
            {
                long startingnumber = Convert.ToInt64(lstUserDetails.FirstOrDefault().ROWNUM);
                long endingnumber = Convert.ToInt64(lstUserDetails.LastOrDefault().ROWNUM);
                long TotalCount = Convert.ToInt64(lstUserDetails.FirstOrDefault().TotalCount);

                ViewBag.StartNumber = startingnumber;
                ViewBag.EndingNumber = endingnumber;
                ViewBag.TotalCount = TotalCount;
                decimal perce = Convert.ToDecimal(TotalCount % PageSize);
                if (perce > 0)
                { ViewBag.TotalPages = Convert.ToInt64(TotalCount / PageSize) + 1; }
                else { ViewBag.TotalPages = Convert.ToInt64(TotalCount / PageSize); }
                ViewBag.currentpage = PageNo;
            }
            return PartialView("~/Views/Messages/Partial/MessageUserlist.cshtml", lstUserDetails);
        }




        public ActionResult GetUserSubjects(long Id)
        {
            ViewBag.UserID = Id;
            string name = "";

            tbl_Users tbluser = _db.tbl_Users.Where(x => x.UserId == Id).ToList().FirstOrDefault();
            if (tbluser != null)
            {
                name = tbluser.Firstname + " " + tbluser.Lastname;
            }
            ViewBag.UserName = name;

            return View("~/Views/Messages/SubjectsMessages.cshtml");
        }

        [HttpGet]
        public PartialViewResult ViewSubjects(string serachword = "", int PageNo = 1, int PageSize = 10, string SortColumn = "SubjectCreatedDate", string SortOrder = "DESC", long UserID = 0)
        {

            List<MessageDetails> lstMessageDetails = new List<MessageDetails>();

            lstMessageDetails = (from s in _db.GetAll_tbl_Subjects_MessagesOrderBY_Details(serachword, PageNo, PageSize, SortColumn, SortOrder, UserID)
                                 select new MessageDetails
                                 {
                                     SubjectName = s.SubjectName,
                                     SubjectUserId = s.SubjectUserId,
                                     SubjectCreatedDate = s.SubjectCreatedDate,
                                     SubjectCreatedBy = s.SubjectCreatedBy,
                                     LastMessage = s.LastMessage,
                                     MessageSendDate = s.MessageSendDate,
                                     IsReaded = s.IsReaded,
                                     MessageSubjectId = s.MessageSubjectId,
                                     MessageTo = s.MessageTo,
                                     MessageFrom = s.MessageFrom,
                                     ROWNUM = s.ROWNUM,
                                     SubjectId = s.SubjectId,
                                     TotalCount = s.TotalCount
                                 }).ToList();

            ViewBag.serachword = serachword;
            ViewBag.PageNo = PageNo;
            ViewBag.PageSize = PageSize;
            ViewBag.SortColumn = SortColumn;
            ViewBag.SortOrder = SortOrder;
            ViewBag.StartNumber = 1;
            if (lstMessageDetails.Count > 0)
            {
                long startingnumber = Convert.ToInt64(lstMessageDetails.FirstOrDefault().ROWNUM);
                long endingnumber = Convert.ToInt64(lstMessageDetails.LastOrDefault().ROWNUM);
                long TotalCount = Convert.ToInt64(lstMessageDetails.FirstOrDefault().TotalCount);

                ViewBag.StartNumber = startingnumber;
                ViewBag.EndingNumber = endingnumber;
                ViewBag.TotalCount = TotalCount;
                decimal perce = Convert.ToDecimal(TotalCount % PageSize);
                if (perce > 0)
                { ViewBag.TotalPages = Convert.ToInt64(TotalCount / PageSize) + 1; }
                else { ViewBag.TotalPages = Convert.ToInt64(TotalCount / PageSize); }
                ViewBag.currentpage = PageNo;
            }
            return PartialView("~/Views/Messages/Partial/SubjectsMessagesList.cshtml", lstMessageDetails);
        }

        [HttpPost]
        public JsonResult SaveSubjectMessages(long ID, string subjectname, string messages)
        {
            long UserId = ID;
            tbl_Subjects tblsubj = new tbl_Subjects();
            tblsubj.SubjectName = subjectname;
            tblsubj.SubjectUserId = UserId;
            tblsubj.SubjectCreatedDate = DateTime.Now;
            tblsubj.SubjectCreatedBy = 0;
            tblsubj.IsActive = true;
            tblsubj.IsDeleted = false;
            tblsubj.ModifiedDate = DateTime.Now;
            _db.tbl_Subjects.Add(tblsubj);
            _db.SaveChanges();

            long SubjectId = tblsubj.SubjectId;
            tbl_MessageSubject tblMessageSubject = new tbl_MessageSubject();
            tblMessageSubject.SubjectId = SubjectId;
            tblMessageSubject.MessageFrom = 0;
            tblMessageSubject.MessageTo = UserId;
            tblMessageSubject.MessageText = messages;
            tblMessageSubject.MessageSendDate = DateTime.Now;
            tblMessageSubject.IsReaded = false;
            tblMessageSubject.IsDeleted = false;
            _db.tbl_MessageSubject.Add(tblMessageSubject);
            _db.SaveChanges();

            tbl_MessageNotification tblmessagenot = new tbl_MessageNotification();
            tblmessagenot.MessageReceivedUserId = UserId;
            tblmessagenot.MessageSubjectId = SubjectId;
            tblmessagenot.MessageText = messages;
            tblmessagenot.IsReaded = false;
            tblmessagenot.IsDeleted = false;
            tblmessagenot.MessageReceivedDate = DateTime.Now;
            tblmessagenot.MessageSendUserId = 0;
            _db.tbl_MessageNotification.Add(tblmessagenot);
            _db.SaveChanges();

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActiveInactiveMessagesSubjectByID(long ID, string status)
        {
            try
            {
                tbl_Subjects Subjects = _db.tbl_Subjects.Where(x => x.SubjectId == ID).FirstOrDefault();
                if (Subjects != null)
                {
                    if (status == "Active")
                    {
                        Subjects.IsActive = true;
                    }
                    else
                    {
                        Subjects.IsActive = false;
                    }

                    _db.SaveChanges();
                }
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception aes)
            {
                return Json("Error :- " + aes.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteMessagesSubjectByID(long ID)
        {
            try
            {
                tbl_Subjects Subjects = _db.tbl_Subjects.Where(x => x.SubjectId == ID).FirstOrDefault();
                if (Subjects != null)
                {
                    Subjects.IsDeleted = true;

                    _db.SaveChanges();
                }
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception aes)
            {
                return Json("Error :- " + aes.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ViewMessageDetails(long Id)
        {
            string name = "";
            string SubjectName = "";
            long UserID = 0;
            ViewBag.SubjectID = Id;
            tbl_Subjects tblSubjects = _db.tbl_Subjects.Where(x => x.SubjectId == Id).ToList().FirstOrDefault();
            if (tblSubjects != null)
            {
                UserID = Convert.ToInt64(tblSubjects.SubjectUserId);
                SubjectName = tblSubjects.SubjectName;
            }

            ViewBag.UserID = UserID;

            tbl_Users tbluser = _db.tbl_Users.Where(x => x.UserId == UserID).ToList().FirstOrDefault();
            if (tbluser != null)
            {
                name = tbluser.Firstname + " " + tbluser.Lastname;
            }
            ViewBag.UserName = name;

            ViewBag.SubjectName = SubjectName;
            return View("~/Views/Messages/ViewMessageDetails.cshtml");
        }

        public PartialViewResult LoadMessages(long Id)
        {
            long subjectid = Id;
            List<MessageDetails> lstMessageDetails = new List<MessageDetails>();

            lstMessageDetails = (from s in _db.tbl_MessageSubject
                                 join s1 in _db.tbl_Subjects on s.SubjectId equals s1.SubjectId
                                 where s.SubjectId == subjectid && (s.IsDeleted != true || s.IsDeleted != null)
                                 select new MessageDetails
                                 {
                                     SubjectName = s1.SubjectName,
                                     SubjectUserId = s1.SubjectUserId,
                                     SubjectCreatedDate = s1.SubjectCreatedDate,
                                     SubjectCreatedBy = s1.SubjectCreatedBy,
                                     Message = s.MessageText,
                                     MessageSendDate = s.MessageSendDate,
                                     IsReaded = s.IsReaded,
                                     MessageSubjectId = s.MessageSubjectId,
                                     MessageTo = s.MessageTo,
                                     MessageFrom = s.MessageFrom,
                                     // ROWNUM = s.ROWNUM,
                                     SubjectId = s.SubjectId,
                                     //TotalCount = s.TotalCount
                                 }).ToList().OrderBy(x => x.MessageSendDate).ToList();

            return PartialView("~/Views/Messages/Partial/LoadMessages.cshtml", lstMessageDetails);
        }

        [HttpPost]
        public JsonResult SendMessages(long ID, string messages)
        {
            long UserId = 0;
            tbl_Subjects tblSubjects = _db.tbl_Subjects.Where(x => x.SubjectId == ID).ToList().FirstOrDefault();
            if (tblSubjects != null)
            {
                UserId = Convert.ToInt64(tblSubjects.SubjectUserId);

            }

            long SubjectId = ID;
            tbl_MessageSubject tblMessageSubject = new tbl_MessageSubject();
            tblMessageSubject.SubjectId = SubjectId;
            tblMessageSubject.MessageFrom = 0;
            tblMessageSubject.MessageTo = UserId;
            tblMessageSubject.MessageText = messages;
            tblMessageSubject.MessageSendDate = DateTime.Now;
            tblMessageSubject.IsReaded = false;
            tblMessageSubject.IsDeleted = false;
            _db.tbl_MessageSubject.Add(tblMessageSubject);
            _db.SaveChanges();


            tbl_MessageNotification tblmessagenot = new tbl_MessageNotification();
            tblmessagenot.MessageReceivedUserId = UserId;
            tblmessagenot.MessageSubjectId = SubjectId;
            tblmessagenot.MessageText = messages;
            tblmessagenot.IsReaded = false;
            tblmessagenot.IsDeleted = false;
            tblmessagenot.MessageReceivedDate = DateTime.Now;
            tblmessagenot.MessageSendUserId = 0;
            _db.tbl_MessageNotification.Add(tblmessagenot);
            _db.SaveChanges();

            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}
