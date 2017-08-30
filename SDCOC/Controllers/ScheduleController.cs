using SDCOC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDCOC.Controllers
{
    public class ScheduleController : BaseController
    {
        //
        // GET: /Schedule/
        SDCOCEntities _db = new SDCOCEntities();

        Common cs = new Common();

        #region Schedule

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult Schedulelist(string serachword = "", int PageNo = 1, int PageSize = 10, string SortColumn = "CreatedDate", string SortOrder = "DESC")
        {

            List<ScheduleDetails> lstscheduleDetails = new List<ScheduleDetails>();

            lstscheduleDetails = (from s in _db.GetAll_tbl_Schedule_Details(serachword, PageNo, PageSize, SortColumn, SortOrder)
                                  select new ScheduleDetails
                                  {
                                      ScheduleId = s.ScheduleId,
                                      Title = s.Title,
                                      Description = s.Description,
                                      EventDate = s.EventDate,
                                      EventStartTime = s.EventStartTime,
                                      EventEndTime = s.EventEndTime,
                                      ScheduleType = s.ScheduleType,
                                      IsActive = s.IsActive,
                                      IsDeleted = s.IsDeleted,
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
            if (lstscheduleDetails.Count > 0)
            {
                long startingnumber = Convert.ToInt64(lstscheduleDetails.FirstOrDefault().ROWNUM);
                long endingnumber = Convert.ToInt64(lstscheduleDetails.LastOrDefault().ROWNUM);
                long TotalCount = Convert.ToInt64(lstscheduleDetails.FirstOrDefault().TotalCount);

                ViewBag.StartNumber = startingnumber;
                ViewBag.EndingNumber = endingnumber;
                ViewBag.TotalCount = TotalCount;
                decimal perce = Convert.ToDecimal(TotalCount % PageSize);
                if (perce > 0)
                { ViewBag.TotalPages = Convert.ToInt64(TotalCount / PageSize) + 1; }
                else { ViewBag.TotalPages = Convert.ToInt64(TotalCount / PageSize); }
                ViewBag.currentpage = PageNo;
            }


            return PartialView("~/Views/Schedule/Partial/Schedulelist.cshtml", lstscheduleDetails);
        }

        public ActionResult AddSchedule(long Id=0)
        {
            List<ScheduleCategeoryDetails> lstscheduleCategoryDetails = new List<ScheduleCategeoryDetails>();

            lstscheduleCategoryDetails = (from s in _db.ScheduleCategories
                                          where s.ISdeleted == false || s.ISdeleted == null
                                          select new ScheduleCategeoryDetails
                                          {
                                              ScheduleTypeId = s.ScheduleTypeId,
                                              ScheduleType = s.ScheduleType,

                                          }).ToList();

            ViewData["lstscheduleCategoryDetails"] = lstscheduleCategoryDetails;

            ViewBag.pkScheduleid = Id;

            List<ScheduleDetails> lstscheduleDetails = new List<ScheduleDetails>();

            lstscheduleDetails = (from s in _db.tbl_Schedule
                                  where s.ScheduleId == Id
                                  select new ScheduleDetails
                                  {
                                      ScheduleId = s.ScheduleId,
                                      Title = s.Title,
                                      Description = s.Description,
                                      EventDate = s.EventDate,
                                      EventStartTime = s.EventStartTime,
                                      EventEndTime = s.EventEndTime,
                                      ScheduleCategory_ID = s.ScheduleCategory_ID
                                  }).ToList();
            if (Id == 0)
            {
                ViewBag.Title = "Add New Schedule";
            }
            else
            {
                ViewBag.Title = "Update Schedule";
            }
            return View(lstscheduleDetails);
        }

        public ActionResult ViewSchedule(long Id)
        {
            
                long ScheduleId = Convert.ToInt64(Id);
                List<ScheduleDetails> lstscheduleDetails = new List<ScheduleDetails>();

                lstscheduleDetails = (from s in _db.tbl_Schedule
                                      join  s1 in _db.ScheduleCategories on s.ScheduleCategory_ID equals s1.ScheduleTypeId
                                      where s.ScheduleId == ScheduleId
                                      select new ScheduleDetails
                                      {
                                          ScheduleId = s.ScheduleId,
                                          Title = s.Title,
                                          Description = s.Description,
                                          EventDate = s.EventDate,
                                          EventStartTime = s.EventStartTime,
                                          EventEndTime = s.EventEndTime,
                                          ScheduleType = s1.ScheduleType,
                                          IsActive = s.IsActive,
                                          IsDeleted = s.IsDeleted,
                                          CreatedDate = s.CreatedDate,
                                          ModifiedDate = s.ModifiedDate
                                      }).ToList();

                return View("~/Views/Schedule/ViewSchedule.cshtml", lstscheduleDetails);
           
        }

       

        public JsonResult DeleteScheduleByID(long ID)
        {
            try
            {
                tbl_Schedule sch = _db.tbl_Schedule.Where(x => x.ScheduleId == ID).FirstOrDefault();

                if (sch != null)
                {
                    sch.IsDeleted = true;

                    _db.SaveChanges();
                }
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception aes)
            {
                return Json("Error :- " + aes.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ActiveInactiveScheduleByID(long ID, string status)
        {
            try
            {
                tbl_Schedule sch = _db.tbl_Schedule.Where(x => x.ScheduleId == ID).FirstOrDefault();
                if (sch != null)
                {
                    if (status == "Active")
                    {
                        sch.IsActive = true;
                    }
                    else
                    {
                        sch.IsActive = false;
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

        public ActionResult EditSchedule(long Id)
        {
            ViewBag.pkScheduleid = Id;

            List<ScheduleCategeoryDetails> lstscheduleCategoryDetails = new List<ScheduleCategeoryDetails>();

            lstscheduleCategoryDetails = (from s in _db.ScheduleCategories
                                          where s.ISdeleted == false || s.ISdeleted == null
                                          select new ScheduleCategeoryDetails
                                          {
                                              ScheduleTypeId = s.ScheduleTypeId,
                                              ScheduleType = s.ScheduleType,

                                          }).ToList();

            ViewData["lstscheduleCategoryDetails"] = lstscheduleCategoryDetails;


            return View();
        }


        [HttpPost]
        public ActionResult SaveScheduleDetails(FormCollection frm)
        {

            long pkScheduleid = Convert.ToInt64(frm["pkScheduleid"]);

            if (pkScheduleid == 0)
            {
                tbl_Schedule tblsch = new tbl_Schedule();               
                tblsch.EventStartTime = frm["txtstarttime"];
                tblsch.EventEndTime = frm["txtendtime"];
                tblsch.EventDate = Convert.ToDateTime(frm["txtscheduledate"]);
                tblsch.Title = frm["txtTitle"];
                tblsch.Description = frm["txtDescription"];
                tblsch.CreatedDate = DateTime.Now.Date;
                tblsch.ScheduleCategory_ID = Convert.ToInt64(frm["txtCategories"]);
                tblsch.IsDeleted = false;
                tblsch.IsActive = true;
                _db.tbl_Schedule.Add(tblsch);
                _db.SaveChanges();

            }
            else
            {
                tbl_Schedule tblsch = _db.tbl_Schedule.Where(x => x.ScheduleId == pkScheduleid).ToList().FirstOrDefault();
                if (tblsch != null)
                {
                    tblsch.EventStartTime = frm["txtstarttime"];
                    tblsch.EventEndTime = frm["txtendtime"];
                    tblsch.EventDate = Convert.ToDateTime(frm["txtscheduledate"]);
                    tblsch.Title = frm["txtTitle"];
                    tblsch.Description = frm["txtDescription"];                   
                    tblsch.ScheduleCategory_ID = Convert.ToInt64(frm["txtCategories"]);
                    tblsch.ModifiedDate = DateTime.Now.Date;
                    _db.SaveChanges();

                }
                
            }
            return RedirectToAction("Index","Schedule");
        }

        #endregion

        #region ScheduleCategory

        public ActionResult CategorySchedule()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult ScheduleCategoryList(string serachword = "", int PageNo = 1, int PageSize = 10, string SortColumn = "ScheduleTypeId", string SortOrder = "DESC")
        {
            List<ScheduleCategeoryDetails> lstscheduleCategoryDetails = new List<ScheduleCategeoryDetails>();

            lstscheduleCategoryDetails = (from s in _db.GetAll_ScheduleCategory_Details(serachword, PageNo, PageSize, SortColumn, SortOrder)
                                          select new ScheduleCategeoryDetails
                                  {
                                      ScheduleTypeId = s.ScheduleTypeId,
                                      ScheduleType = s.ScheduleType,
                                      ISdeleted = s.ISdeleted,
                                      ROWNUM = s.ROWNUM,
                                      TotalCount = s.TotalCount
                                  }).ToList();

            ViewBag.serachword = serachword;
            ViewBag.PageNo = PageNo;
            ViewBag.PageSize = PageSize;
            ViewBag.SortColumn = SortColumn;
            ViewBag.SortOrder = SortOrder;
            ViewBag.StartNumber = 1;
            if (lstscheduleCategoryDetails.Count > 0)
            {
                long startingnumber = Convert.ToInt64(lstscheduleCategoryDetails.FirstOrDefault().ROWNUM);
                long endingnumber = Convert.ToInt64(lstscheduleCategoryDetails.LastOrDefault().ROWNUM);
                long TotalCount = Convert.ToInt64(lstscheduleCategoryDetails.FirstOrDefault().TotalCount);

                ViewBag.StartNumber = startingnumber;
                ViewBag.EndingNumber = endingnumber;
                ViewBag.TotalCount = TotalCount;
                decimal perce = Convert.ToDecimal(TotalCount % PageSize);
                if (perce > 0)
                { ViewBag.TotalPages = Convert.ToInt64(TotalCount / PageSize) + 1; }
                else { ViewBag.TotalPages = Convert.ToInt64(TotalCount / PageSize); }
                ViewBag.currentpage = PageNo;
            }
            return PartialView("~/Views/Schedule/Partial/ScheduleCategory.cshtml", lstscheduleCategoryDetails);
        }

        public JsonResult DeleteScheduleCategoryByID(long ID)
        {
            try
            {
                ScheduleCategory sch = _db.ScheduleCategories.Where(x => x.ScheduleTypeId == ID).FirstOrDefault();

                if (sch != null)
                {
                    sch.ISdeleted = true;

                    _db.SaveChanges();
                }
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception aes)
            {
                return Json("Error :- " + aes.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult SaveScheduleCategory(long ID, string ScheduleType)
        {
            try
            {

                if (ID == 0)
                {
                    ScheduleCategory schc = new ScheduleCategory();
                    schc.ScheduleType = ScheduleType;
                    _db.ScheduleCategories.Add(schc);
                    _db.SaveChanges();
                }
                else
                {

                    ScheduleCategory sch = _db.ScheduleCategories.Where(x => x.ScheduleTypeId == ID).FirstOrDefault();

                    if (sch != null)
                    {
                        sch.ScheduleType = ScheduleType;

                        _db.SaveChanges();
                    }
                }
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception aes)
            {
                return Json("Error :- " + aes.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}
