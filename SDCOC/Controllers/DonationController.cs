using SDCOC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDCOC.Controllers
{
    public class DonationController : BaseController
    {
        //
        // GET: /Donation/
        SDCOCEntities _db = new SDCOCEntities();

        Common cs = new Common();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult GetDonationList(string serachword = "", int PageNo = 1, int PageSize = 10, string SortColumn = "CreatedDate", string SortOrder = "DESC")
        {

            List<DonationDetails> lstDonationDetails = new List<DonationDetails>();

            lstDonationDetails = (from s in _db.GetAll_tbl_Donation_Details(serachword, PageNo, PageSize, SortColumn, SortOrder)
                                  select new DonationDetails
                              {
                                  DonationId = s.DonationId,
                                  UserId = s.UserId,
                                  Username = s.Username,
                                  Firstname = s.Firstname,
                                  Lastname = s.Lastname,
                                  MobileNumber = s.MobileNumber,
                                  EmailId = s.EmailId,
                                  DonationAmount = s.DonationAmount,
                                  PaymentType = s.PaymentType,
                                  IsAutoRecurring = s.IsAutoRecurring,
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
            if (lstDonationDetails.Count > 0)
            {
                long startingnumber = Convert.ToInt64(lstDonationDetails.FirstOrDefault().ROWNUM);
                long endingnumber = Convert.ToInt64(lstDonationDetails.LastOrDefault().ROWNUM);
                long TotalCount = Convert.ToInt64(lstDonationDetails.FirstOrDefault().TotalCount);

                ViewBag.StartNumber = startingnumber;
                ViewBag.EndingNumber = endingnumber;
                ViewBag.TotalCount = TotalCount;
                decimal perce = Convert.ToDecimal(TotalCount % PageSize);
                if (perce > 0)
                { ViewBag.TotalPages = Convert.ToInt64(TotalCount / PageSize) + 1; }
                else { ViewBag.TotalPages = Convert.ToInt64(TotalCount / PageSize); }
                ViewBag.currentpage = PageNo;
            }
            return PartialView("~/Views/Donation/Partial/DonationList.cshtml", lstDonationDetails);
        }

        public JsonResult ActiveInactiveDonationByID(long ID, string status)
        {
            try
            {
                tbl_Donation tblDonation = _db.tbl_Donation.Where(x => x.DonationId == ID).FirstOrDefault();
                if (tblDonation != null)
                {
                    if (status == "Active")
                    {
                        tblDonation.IsActive = true;
                    }
                    else
                    {
                        tblDonation.IsActive = false;
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

        public JsonResult DeleteDonationByID(long ID)
        {
            try
            {
                tbl_Donation tblDonation = _db.tbl_Donation.Where(x => x.DonationId == ID).FirstOrDefault();
                if (tblDonation != null)
                {
                    tblDonation.IsDeleted = true;

                    _db.SaveChanges();
                }
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception aes)
            {
                return Json("Error :- " + aes.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditDonation(long Id)
        {
            List<EditDonationDetails> lstdonation = new List<EditDonationDetails>();
            lstdonation = (from s in _db.Get_EditDonationDetails(Id)
                           select new EditDonationDetails
                           {
                               DonationId = s.DonationId,                               
                               Username = s.Username,
                               Firstname = s.Firstname,
                               Lastname = s.Lastname,
                               MobileNumber = s.MobileNumber,
                               EmailId = s.EmailId,
                               DonatedAmount = s.DonatedAmount,                               
                               IsAutoRecurring = s.IsAutoRecurring,
                               DonationDate=s.DonationDate
                              
                           }).ToList();
            return PartialView("~/Views/Donation/ViewDonationDetails.cshtml", lstdonation);
        }

        public JsonResult DonationByRecurringID(long ID, string status)
        {
            try
            {
                tbl_Donation tblDonation = _db.tbl_Donation.Where(x => x.DonationId == ID).FirstOrDefault();
                if (tblDonation != null)
                {
                    if (status.Trim().ToLower() == "yes")
                    {
                        tblDonation.IsAutoRecurring = true;
                    }
                    else
                    {
                        tblDonation.IsAutoRecurring = false;
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


        [HttpGet]
        public PartialViewResult GetDonationHistoryList(string serachword = "", int PageNo = 1, int PageSize = 10, string SortColumn = "DonationDate", string SortOrder = "DESC")
        {

            List<DonationHistory> lstDonationDetails = new List<DonationHistory>();

            lstDonationDetails = (from s in _db.GetAll_tbl_DonationHistory_Details(serachword, PageNo, PageSize, SortColumn, SortOrder)
                                  select new DonationHistory
                                  {
                                      DonatedAmount = s.DonatedAmount,
                                      DonationId = s.DonationId,
                                      DonationHistoryId = s.DonationHistoryId,
                                      DonationDate = s.DonationDate,
                                      ROWNUM = s.ROWNUM,
                                      TotalCount = s.TotalCount
                                  }).ToList();

            ViewBag.serachword = serachword;
            ViewBag.PageNo = PageNo;
            ViewBag.PageSize = PageSize;
            ViewBag.SortColumn = SortColumn;
            ViewBag.SortOrder = SortOrder;
            ViewBag.StartNumber = 1;
            if (lstDonationDetails.Count > 0)
            {
                long startingnumber = Convert.ToInt64(lstDonationDetails.FirstOrDefault().ROWNUM);
                long endingnumber = Convert.ToInt64(lstDonationDetails.LastOrDefault().ROWNUM);
                long TotalCount = Convert.ToInt64(lstDonationDetails.FirstOrDefault().TotalCount);

                ViewBag.StartNumber = startingnumber;
                ViewBag.EndingNumber = endingnumber;
                ViewBag.TotalCount = TotalCount;
                decimal perce = Convert.ToDecimal(TotalCount % PageSize);
                if (perce > 0)
                { ViewBag.TotalPages = Convert.ToInt64(TotalCount / PageSize) + 1; }
                else { ViewBag.TotalPages = Convert.ToInt64(TotalCount / PageSize); }
                ViewBag.currentpage = PageNo;
            }
            return PartialView("~/Views/Donation/Partial/ViewDonationHistoryList.cshtml", lstDonationDetails);
        }

    }
}
