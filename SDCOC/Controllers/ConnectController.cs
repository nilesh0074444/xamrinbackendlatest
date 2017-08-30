using SDCOC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDCOC.Controllers
{
    public class ConnectController :  BaseController
    {
        //
        // GET: /Connect/
        SDCOCEntities _db = new SDCOCEntities();

        Common cs = new Common();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult ConnectList(string serachword = "", int PageNo = 1, int PageSize = 10, string SortColumn = "ReceivedDate", string SortOrder = "DESC")
        {

            List<ConnectDetails> lstConnectDetails = new List<ConnectDetails>();

            lstConnectDetails = (from s in _db.GetAll_tbl_Connect_Details(serachword, PageNo, PageSize, SortColumn, SortOrder)
                              select new ConnectDetails
                              {
                                  UserId = s.UserId,
                                  EmailId = s.EmailId,                                
                                  Firstname = s.Firstname,
                                  Lastname = s.Lastname,
                                  MessageText = s.MessageText,
                                  ReceivedDate = s.ReceivedDate,
                                  ConnectId = s.ConnectId,
                                  ROWNUM=s.ROWNUM,
                                  TotalCount=s.TotalCount
                              }).ToList();

            ViewBag.serachword = serachword;
            ViewBag.PageNo = PageNo;
            ViewBag.PageSize = PageSize;
            ViewBag.SortColumn = SortColumn;
            ViewBag.SortOrder = SortOrder;
            ViewBag.StartNumber = 1;
            if (lstConnectDetails.Count > 0)
            {
                long startingnumber = Convert.ToInt64(lstConnectDetails.FirstOrDefault().ROWNUM);
                long endingnumber = Convert.ToInt64(lstConnectDetails.LastOrDefault().ROWNUM);
                long TotalCount = Convert.ToInt64(lstConnectDetails.FirstOrDefault().TotalCount);

                ViewBag.StartNumber = startingnumber;
                ViewBag.EndingNumber = endingnumber;
                ViewBag.TotalCount = TotalCount;
                decimal perce = Convert.ToDecimal(TotalCount % PageSize);
                if (perce > 0)
                { ViewBag.TotalPages = Convert.ToInt64(TotalCount / PageSize) + 1; }
                else { ViewBag.TotalPages = Convert.ToInt64(TotalCount / PageSize); }
                ViewBag.currentpage = PageNo;
            }


            return PartialView("~/Views/Connect/Partial/ConnectList.cshtml", lstConnectDetails);
        }
        public JsonResult DeleteConnectByID(long ID)
        {
            try
            {
                tbl_Connect conts = _db.tbl_Connect.Where(x => x.ConnectId == ID).FirstOrDefault();
                if (conts != null)
                {
                    conts.IsDeleted = true;

                    _db.SaveChanges();
                }
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception aes)
            {
                return Json("Error :- " + aes.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ViewConnect(long Id)
        {

            long connetID = Convert.ToInt64(Id);
            List<ConnectDetails> lstConnectDetails = new List<ConnectDetails>();

            lstConnectDetails =  (from s in _db.tbl_Connect
                                  join s1 in _db.tbl_Users on s.UserId equals s1.UserId
                                  where s.ConnectId == connetID && (s.IsDeleted == false || s.IsDeleted == null) && (s1.IsDeleted == false || s1.IsDeleted == null)
                                  select new ConnectDetails
                              {
                                  UserId = s.UserId,
                                  EmailId = s1.EmailId,
                                  Firstname = s1.Firstname,
                                  Lastname = s1.Lastname,
                                  MessageText = s.MessageText,
                                  ReceivedDate = s.ReceivedDate,
                                  ConnectId = s.ConnectId

                              }).ToList();
            return View("~/Views/Connect/ViewConnect.cshtml", lstConnectDetails);

        }
       
    }
}
