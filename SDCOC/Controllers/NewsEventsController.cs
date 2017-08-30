using SDCOC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDCOC.Controllers
{
    public class NewsEventsController : BaseController
    {
        //
        // GET: /NewsEvents/
        SDCOCEntities _db = new SDCOCEntities();

        Common cs = new Common();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult NewEventList(string serachword = "", int PageNo = 1, int PageSize = 10, string SortColumn = "CreatedDate", string SortOrder = "DESC")
        {
            List<NewsEventsDetails> lstUserDetails = new List<NewsEventsDetails>();

            lstUserDetails = (from s in _db.GetAll_tbl_NewsEvents_Details(serachword, PageNo, PageSize, SortColumn, SortOrder)
                              select new NewsEventsDetails
                              {
                                  NewsEventId = s.NewsEventId,
                                  Title = s.Title,
                                  Description = s.Description,
                                  Eventdate = s.Eventdate,
                                  EventTime = s.EventTime,
                                  EventImage = s.EventImage,
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
            return PartialView("~/Views/NewsEvents/Partial/NewEventList.cshtml", lstUserDetails);
        }


        public JsonResult ActiveInactiveNewEventsByID(long ID, string status)
        {
            try
            {
                tbl_NewsEvents news = _db.tbl_NewsEvents.Where(x => x.NewsEventId == ID).FirstOrDefault();
                if (news != null)
                {
                    if (status == "Active")
                    {
                        news.IsActive = true;
                    }
                    else
                    {
                        news.IsActive = false;
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

        public JsonResult DeleteNewEventsByID(long ID)
        {
            try
            {
                tbl_NewsEvents news = _db.tbl_NewsEvents.Where(x => x.NewsEventId == ID).FirstOrDefault();
                if (news != null)
                {
                    news.IsDeleted = true;

                    _db.SaveChanges();
                }
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception aes)
            {
                return Json("Error :- " + aes.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddNewsEvents()
        {
            long Id = 0;
            ViewBag.pkNewsEventID = Id;

            List<NewsEventsDetails> lstNewsEventsDetailsDetails = new List<NewsEventsDetails>();

            lstNewsEventsDetailsDetails = (from s in _db.tbl_NewsEvents
                                           where s.NewsEventId == Id
                                           select new NewsEventsDetails
                                           {
                                               NewsEventId = s.NewsEventId,
                                               Title = s.Title,
                                               Description = s.Description,
                                               Eventdate = s.Eventdate,
                                               EventTime = s.EventTime,
                                               EventImage = s.EventImage,
                                               IsActive = s.IsActive,
                                               IsDeleted = s.IsDeleted,
                                               CreatedDate = s.CreatedDate,
                                               ModifiedDate = s.ModifiedDate,


                                           }).ToList();
            ViewBag.PageTitle = "Add News and Event";
            
            return View("~/Views/NewsEvents/AddNewsEvents.cshtml", lstNewsEventsDetailsDetails);
        }
        public ActionResult UpdateNewsEvents(long Id)
        {
            ViewBag.pkNewsEventID = Id;

            List<NewsEventsDetails> lstNewsEventsDetailsDetails = new List<NewsEventsDetails>();

            lstNewsEventsDetailsDetails = (from s in _db.tbl_NewsEvents
                                           where s.NewsEventId == Id
                                           select new NewsEventsDetails
                                           {
                                               NewsEventId = s.NewsEventId,
                                               Title = s.Title,
                                               Description = s.Description,
                                               Eventdate = s.Eventdate,
                                               EventTime = s.EventTime,
                                               EventImage = s.EventImage,
                                               IsActive = s.IsActive,
                                               IsDeleted = s.IsDeleted,
                                               CreatedDate = s.CreatedDate,
                                               ModifiedDate = s.ModifiedDate,
                                               
                                           }).ToList();
            ViewBag.Title = "Update News and Event";
           
            return View("~/Views/NewsEvents/AddNewsEvents.cshtml", lstNewsEventsDetailsDetails);
        }

        public ActionResult ViewNewsEvents(long Id)
        {
            List<NewsEventsDetails> lstNewsEventsDetailsDetails = new List<NewsEventsDetails>();

            lstNewsEventsDetailsDetails = (from s in _db.tbl_NewsEvents
                                           where s.NewsEventId == Id
                                           select new NewsEventsDetails
                                           {
                                               NewsEventId = s.NewsEventId,
                                               Title = s.Title,
                                               Description = s.Description,
                                               Eventdate = s.Eventdate,
                                               EventTime = s.EventTime,
                                               EventImage = s.EventImage,
                                               IsActive = s.IsActive,
                                               IsDeleted = s.IsDeleted,
                                               CreatedDate = s.CreatedDate,
                                               ModifiedDate = s.ModifiedDate,


                                           }).ToList();
            return View("~/Views/NewsEvents/ViewNewsEvents.cshtml", lstNewsEventsDetailsDetails);
        }


        [HttpPost]
        public ActionResult SaveNewEventsDetails(HttpPostedFileBase ProfilePicture, FormCollection frm)
        {

            long pknewseventid = Convert.ToInt64(frm["pknewseventid"]);

            if (pknewseventid == 0)
            {
                //needs put logic here for check exist users

                tbl_NewsEvents news = new tbl_NewsEvents();
                news.Title = frm["txtTitle"];
                news.Description =frm["txtDescription"];
                news.Eventdate = Convert.ToDateTime(frm["txteventdate"]);
                news.EventTime = frm["txtevnettime"];
                news.IsDeleted = false;
                news.CreatedDate = DateTime.Now.Date;

                if (frm["hdnstatus"].ToLower().Trim() == "active")
                {
                    news.IsActive = true;
                }
                else
                {
                    news.IsActive = false;
                }

                string fileName = "";

                if (ProfilePicture != null)
                {
                    if (ProfilePicture.ContentLength > 0 && ProfilePicture.FileName != "")
                    {
                        fileName = Guid.NewGuid() + Path.GetExtension(ProfilePicture.FileName);
                        string path = Path.Combine(Server.MapPath("~/Images/EventImages"), fileName);
                        ProfilePicture.SaveAs(path);
                        news.EventImage = fileName;
                    }
                }


                _db.tbl_NewsEvents.Add(news);
                _db.SaveChanges();




            }
            else
            {

                tbl_NewsEvents news = _db.tbl_NewsEvents.Where(x => x.NewsEventId == pknewseventid).FirstOrDefault();
                if (news != null)
                {
                    news.Title = frm["txtTitle"];
                    news.Description = frm["txtDescription"];
                    news.Eventdate = Convert.ToDateTime(frm["txteventdate"]);
                    news.EventTime = frm["txtevnettime"];                   
                    news.ModifiedDate = DateTime.Now.Date;
                    if (frm["hdnstatus"].ToLower().Trim() == "active")
                    {
                        news.IsActive = true;
                    }
                    else
                    {
                        news.IsActive = false;
                    }

                    string fileName = "";
                    if (ProfilePicture != null)
                    {
                        if (ProfilePicture.ContentLength > 0 && ProfilePicture.FileName != "")
                        {
                            fileName = Guid.NewGuid() + Path.GetExtension(ProfilePicture.FileName);
                            string path = Path.Combine(Server.MapPath("~/Images/EventImages"), fileName);
                            ProfilePicture.SaveAs(path);
                            news.EventImage = fileName;
                        }
                    }
                    else if (frm["hdnprofileimage"] != "")
                    {
                        news.EventImage = frm["hdnprofileimage"];
                    }



                    _db.SaveChanges();
                }

            }
            return RedirectToAction("Index");



        }

    }
}
