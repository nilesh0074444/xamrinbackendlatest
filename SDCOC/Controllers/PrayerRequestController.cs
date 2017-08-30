using SDCOC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDCOC.Controllers
{
    public class PrayerRequestController : BaseController
    {
        //
        // GET: /PrayerRequest/
        SDCOCEntities _db = new SDCOCEntities();

        Common cs = new Common();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult PrayerList(string serachword = "", int PageNo = 1, int PageSize = 10, string SortColumn = "CreatedDate", string SortOrder = "DESC")
        {
          
            List<PrayerRequestDetails> lstPrayerDetails = new List<PrayerRequestDetails>();
            
            lstPrayerDetails = (from s in _db.GetAll_tbl_PrayerRequest_Details(serachword, PageNo, PageSize, SortColumn, SortOrder)
                                select new PrayerRequestDetails
                              {
                                  PrayerRequestId = s.PrayerRequestId,
                                  UserId = s.UserId,
                                  EmailId = s.EmailId,
                                  Firstname = s.Firstname,
                                  Lastname = s.Lastname,
                                  PrayerRequestName = s.Name,
                                  ReceivedDate = s.ReceivedDate,
                                  PrayerRequestText = s.PrayerRequestText,
                                  ROWNUM = s.ROWNUM,
                                  TotalCount = s.TotalCount
                              }).ToList();

            ViewBag.serachword = serachword;
            ViewBag.PageNo = PageNo;
            ViewBag.PageSize = PageSize;
            ViewBag.SortColumn = SortColumn;
            ViewBag.SortOrder = SortOrder;
            ViewBag.StartNumber = 1;
            if (lstPrayerDetails.Count > 0)
            {
                long startingnumber = Convert.ToInt64(lstPrayerDetails.FirstOrDefault().ROWNUM);
                long endingnumber = Convert.ToInt64(lstPrayerDetails.LastOrDefault().ROWNUM);
                long TotalCount = Convert.ToInt64(lstPrayerDetails.FirstOrDefault().TotalCount);

                ViewBag.StartNumber = startingnumber;
                ViewBag.EndingNumber = endingnumber;
                ViewBag.TotalCount = TotalCount;
                decimal perce = Convert.ToDecimal(TotalCount % PageSize);
                if (perce > 0)
                { ViewBag.TotalPages = Convert.ToInt64(TotalCount / PageSize) + 1; }
                else { ViewBag.TotalPages = Convert.ToInt64(TotalCount / PageSize); }
                ViewBag.currentpage = PageNo;
            }
            return PartialView("~/Views/PrayerRequest/Partial/PrayerList.cshtml", lstPrayerDetails);
        }

        public ActionResult PrayerView(long Id)
        {

            long PrayerRequestId = Convert.ToInt64(Id);
            List<PrayerRequestDetails> lstPrayerDetails = new List<PrayerRequestDetails>();

            lstPrayerDetails = (from s1 in _db.tbl_PrayerRequest
                                join s in _db.tbl_Users on s1.UserId equals s.UserId
                                where s1.PrayerRequestId == PrayerRequestId && (s.IsDeleted == false || s.IsDeleted == null) && (s1.IsDeleted == false || s1.IsDeleted == null)
                                select new PrayerRequestDetails
                              {
                                  PrayerRequestId = s1.PrayerRequestId,
                                  UserId = s.UserId,
                                  EmailId = s.EmailId,
                                  Firstname = s.Firstname,
                                  Lastname = s.Lastname,
                                  PrayerRequestName = s1.Name,
                                  ReceivedDate = s1.ReceivedDate,
                                  PrayerRequestText = s1.PrayerRequestText,

                              }).ToList();
            return View("~/Views/PrayerRequest/PrayerView.cshtml", lstPrayerDetails);

        }

        public JsonResult DeletePrayerRequestByID(long ID)
        {
            try
            {
                tbl_PrayerRequest tblPrayerRequest = _db.tbl_PrayerRequest.Where(x => x.PrayerRequestId == ID).FirstOrDefault();
                if (tblPrayerRequest != null)
                {
                    tblPrayerRequest.IsDeleted = true;

                    _db.SaveChanges();
                }
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception aes)
            {
                return Json("Error :- " + aes.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }

    }
}
