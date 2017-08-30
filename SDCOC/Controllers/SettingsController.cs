using SDCOC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDCOC.Controllers
{
    public class SettingsController : BaseController
    {
        //
        // GET: /Settings/
        SDCOCEntities _db = new SDCOCEntities();

        Common cs = new Common();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult PersonalInfo()
        {
            ViewBag.SuccessMsg = false;
            if (TempData["SaveProfile"] != "")
            {
                ViewBag.SuccessMsg = true;
                TempData["SaveProfile"] = "";

            }
            return PartialView("~/Views/Settings/Partial/Personal_Info.cshtml");
        }

        [HttpPost]
        public ActionResult UpdatePersonnelInfoDetails(HttpPostedFileBase AdminProfilePicture, FormCollection frm)
        {
            string Firstname = Convert.ToString(frm["txtFirstname"]).Trim();
            string Lastname = Convert.ToString(frm["txtLastname"]).Trim();

            long AdminId = Convert.ToInt32(Session["userid"]);
            tbl_AdminUser objAdmin = _db.tbl_AdminUser.Where(x => x.AdminUserId == AdminId).FirstOrDefault();

            if (objAdmin != null)
            {
                objAdmin.Firstname = Firstname;
                objAdmin.Lastname = Lastname;

                string fileName = "";
                if (AdminProfilePicture != null)
                {
                    if (AdminProfilePicture.ContentLength > 0 && AdminProfilePicture.FileName != "")
                    {
                        fileName = Guid.NewGuid() + Path.GetExtension(AdminProfilePicture.FileName);
                        string path = Path.Combine(Server.MapPath("~/Images/UserImages/ProfileImages"), fileName);
                        AdminProfilePicture.SaveAs(path);
                        objAdmin.ProfilePicture = fileName;
                    }
                }
                else
                {
                    objAdmin.ProfilePicture = frm["hdnpadminrofileimage"];
                    fileName = frm["hdnpadminrofileimage"];

                }
                Session["AdminProfile"] = fileName;

                _db.SaveChanges();

                Session["Firstname"] = Firstname;
                Session["Lastname"] = Lastname;

                TempData["SaveProfile"] = "True";
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public PartialViewResult ChangePassword()
        {
            return PartialView("~/Views/Settings/Partial/ChangePassword.cshtml");
        }
        [HttpPost]
        public string ChangePassword(FormCollection frm)
        {
            string ReturnMessage = "";

            try
            {

                string CurrentPassword = Convert.ToString(frm["txtCurrentPassword"]).Trim();
                string NewPassword = Convert.ToString(frm["txtNewPassword"]).Trim();
                long AdminId = Convert.ToInt32(Session["userid"]);

                string encryptedCurrentPassword = cs.Encrypt(CurrentPassword);

                var userdata = _db.tbl_AdminUser.Where(x => x.AdminUserId == AdminId && x.Password == encryptedCurrentPassword).FirstOrDefault();

                if (userdata != null)
                {
                    userdata.Password = cs.Encrypt(NewPassword);
                    _db.SaveChanges();
                    ReturnMessage = "passwordchanged";
                }
                else
                {
                    ReturnMessage = "currentpasswordnotmatched";
                }
            }
            catch (Exception ex)
            {
                ReturnMessage = "error";
            }

            return ReturnMessage;
        }
        

        [HttpGet]
        public PartialViewResult SocialConnect()
        {
            ViewBag.SocialConnect_FacebookLink = "";
            ViewBag.SocialConnect_TwitterLink = "";
            tbl_Setting SocialConnect = _db.tbl_Setting.ToList().FirstOrDefault();
            if (SocialConnect != null)
            {
                ViewBag.SocialConnect_FacebookLink = SocialConnect.SocialConnect_FacebookLink;
                ViewBag.SocialConnect_TwitterLink = SocialConnect.SocialConnect_TwitterLink;
            }

            return PartialView("~/Views/Settings/Partial/SocialConnect.cshtml");
        }

        [HttpPost]
        public JsonResult SaveSocialConnect(string FacebookLink, string TwitterLink)
        {

            tbl_Setting SocialConnect = _db.tbl_Setting.ToList().FirstOrDefault();
            if (SocialConnect != null)
            {
                SocialConnect.SocialConnect_FacebookLink = FacebookLink;
                SocialConnect.SocialConnect_TwitterLink = TwitterLink;
                _db.SaveChanges();
            }
            else
            {
                tbl_Setting SocialConnect1 = new tbl_Setting();
                SocialConnect1.SocialConnect_FacebookLink = FacebookLink;
                SocialConnect1.SocialConnect_TwitterLink = TwitterLink;
                _db.tbl_Setting.Add(SocialConnect1);
                _db.SaveChanges();
            }


            return Json("success", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult Livestreaming()
        {
            ViewBag.Livestream_FacebookLink = "";
            ViewBag.Livestream_YoutubeLink = "";
            tbl_Setting Livestreaming = _db.tbl_Setting.ToList().FirstOrDefault();
            if (Livestreaming != null)
            {
                ViewBag.Livestream_FacebookLink = Livestreaming.Livestream_FacebookLink;
                ViewBag.Livestream_YoutubeLink = Livestreaming.Livestream_YoutubeLink;
            }

            return PartialView("~/Views/Settings/Partial/Livestreaming.cshtml");
        }

        [HttpPost]
        public JsonResult SaveLivestreaming(string FacebookLink, string YoutubeLink)
        {

            tbl_Setting Livestreaming = _db.tbl_Setting.ToList().FirstOrDefault();
            if (Livestreaming != null)
            {
                Livestreaming.Livestream_FacebookLink = FacebookLink;
                Livestreaming.Livestream_YoutubeLink = YoutubeLink;
                _db.SaveChanges();
            }
            else
            {
                tbl_Setting Livestreaming1 = new tbl_Setting();
                Livestreaming1.Livestream_FacebookLink = FacebookLink;
                Livestreaming1.Livestream_YoutubeLink = YoutubeLink;
                _db.tbl_Setting.Add(Livestreaming1);
                _db.SaveChanges();
            }


            return Json("success", JsonRequestBehavior.AllowGet);
        }
        

      

        [HttpGet]
        public PartialViewResult Ministries()
        {
            return PartialView("~/Views/Settings/Partial/Ministries.cshtml");
        }

        [HttpPost]
        public JsonResult SaveMinistry(long ID, string title)
        {
            if (ID == 0)
            {
                tbl_MinistryInterests MinistryInterests = new tbl_MinistryInterests();
                MinistryInterests.IsDeleted = false;
                MinistryInterests.MinistryTitle = title;
                _db.tbl_MinistryInterests.Add(MinistryInterests);
                _db.SaveChanges();
            }
            else
            {
                tbl_MinistryInterests MinistryInterests = _db.tbl_MinistryInterests.Where(x => x.MinistryId == ID).ToList().FirstOrDefault();
                if (MinistryInterests != null)
                {
                    MinistryInterests.MinistryTitle = title;
                    _db.SaveChanges();
                }
            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteMinistryByID(long ID)
        {
            try
            {
                tbl_MinistryInterests MinistryInterests = _db.tbl_MinistryInterests.Where(x => x.MinistryId == ID).FirstOrDefault();
                if (MinistryInterests != null)
                {
                    MinistryInterests.IsDeleted = true;
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
        public PartialViewResult MinistriesList(string serachword = "", int PageNo = 1, int PageSize = 10, string SortColumn = "MinistryId", string SortOrder = "DESC")
        {

            List<MinistriesInterests> lstMinistriesInterests = new List<MinistriesInterests>();

            lstMinistriesInterests = (from s in _db.GetAll_tbl_MinistryInterests_Details(serachword, PageNo, PageSize, SortColumn, SortOrder)
                                      select new MinistriesInterests
                                      {
                                          MinistryId = s.MinistryId,
                                          MinistryTitle = s.MinistryTitle,
                                          IsDeleted = s.IsDeleted,
                                          ROWNUM = s.ROWNUM,
                                          TotalCount = s.TotalCount
                                      }).ToList();

            ViewBag.serachword = serachword;
            ViewBag.PageNo = PageNo;
            ViewBag.PageSize = PageSize;
            ViewBag.SortColumn = SortColumn;
            ViewBag.SortOrder = SortOrder;
            ViewBag.StartNumber = 1;
            if (lstMinistriesInterests.Count > 0)
            {
                long startingnumber = Convert.ToInt64(lstMinistriesInterests.FirstOrDefault().ROWNUM);
                long endingnumber = Convert.ToInt64(lstMinistriesInterests.LastOrDefault().ROWNUM);
                long TotalCount = Convert.ToInt64(lstMinistriesInterests.FirstOrDefault().TotalCount);

                ViewBag.StartNumber = startingnumber;
                ViewBag.EndingNumber = endingnumber;
                ViewBag.TotalCount = TotalCount;
                decimal perce = Convert.ToDecimal(TotalCount % PageSize);
                if (perce > 0)
                { ViewBag.TotalPages = Convert.ToInt64(TotalCount / PageSize) + 1; }
                else { ViewBag.TotalPages = Convert.ToInt64(TotalCount / PageSize); }
                ViewBag.currentpage = PageNo;
            }
            return PartialView("~/Views/Settings/Partial/_MinistriesList.cshtml", lstMinistriesInterests);
        }

        [HttpGet]
        public PartialViewResult ChurchAddress()
        {
            ViewBag.ChurchAddr = "";
            tbl_Setting ChurchAddr = _db.tbl_Setting.ToList().FirstOrDefault();
            if (ChurchAddr != null)
            {
                ViewBag.ChurchAddr = ChurchAddr.ChurchAddress;
            }
            return PartialView("~/Views/Settings/Partial/ChurchAddress.cshtml");
        }
        [HttpPost]
        public JsonResult SaveChurchAddress(string address)
        {

            tbl_Setting ChurchAddr = _db.tbl_Setting.ToList().FirstOrDefault();
            if (ChurchAddr != null)
            {
                ChurchAddr.ChurchAddress = address;
                _db.SaveChanges();
            }
            else
            {
                tbl_Setting ChurchAddr1 = new tbl_Setting();
                ChurchAddr1.ChurchAddress = address;
                _db.tbl_Setting.Add(ChurchAddr1);
                _db.SaveChanges();
            }
            

            return Json("success", JsonRequestBehavior.AllowGet);
        }
        
    }
}
