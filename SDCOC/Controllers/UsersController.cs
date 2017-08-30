using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDCOC.Models;
using System.IO;

namespace SDCOC.Controllers
{
    public class UsersController : BaseController
    {
        //
        // GET: /Users/

        SDCOCEntities _db = new SDCOCEntities();
        
        Common cs = new Common();
        static string baseUrl = "";

        public ActionResult Index()
        {
           return View();
        }

        [HttpGet]
        public PartialViewResult GetUserList(string serachword ="", int PageNo = 1,int PageSize  = 10,string SortColumn  = "CreatedDate", string SortOrder= "DESC")
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
                                  ROWNUM=s.ROWNUM,
                                  TotalCount=s.TotalCount
                              }).ToList();

            ViewBag.serachword = serachword;
            ViewBag.PageNo = PageNo;
            ViewBag.PageSize = PageSize;
            ViewBag.SortColumn = SortColumn;
            ViewBag.SortOrder = SortOrder;

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
            return PartialView("~/Views/Users/Partial/Userlist.cshtml", lstUserDetails);
        }

        public JsonResult ActiveInactiveUsersByID(long ID, string status)
        {
            try
            {
                tbl_Users users = _db.tbl_Users.Where(x => x.UserId == ID).FirstOrDefault();
                if (users != null)
                {
                    if (status == "Active")
                    {
                        users.IsActive = true;
                    }
                    else
                    {
                        users.IsActive = false;
                    }

                    _db.SaveChanges();
                }
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception aes)
            {
                return Json("Error :- " +aes.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteUsersByID(long ID)
        {
            try
            {
                tbl_Users users = _db.tbl_Users.Where(x => x.UserId == ID).FirstOrDefault();
                if (users != null)
                {
                    users.IsDeleted = true;

                    _db.SaveChanges();
                }
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception aes)
            {
                return Json("Error :- " + aes.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Edit(long Id)
        {
            try
            {
                //long UserID = Convert.ToInt32(cs.Decrypt(Id));
                long UserID = Id;
                var userData = _db.tbl_Users.Where(x => x.UserId.Equals(UserID)).FirstOrDefault();
                return View(userData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "FileNotFound");
            }
        }

        public ActionResult ViewUser(long Id)
        {
           
                long UserID = Convert.ToInt64(Id);
                List<UserDetails> lstUserDetails = new List<UserDetails>();
               
                lstUserDetails = (from s in _db.tbl_Users
                                  where s.UserId == UserID
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
                                      ProfileImage = s.ProfileImage

                                  }).ToList();
                return View("~/Views/Users/ViewUserDetails.cshtml", lstUserDetails);
           
        }


        [HttpPost]
        public ActionResult SaveUserDetails(HttpPostedFileBase ProfilePicture, FormCollection frm)
        {

            long pkuserid = Convert.ToInt64(frm["pkuserid"]);

            if (pkuserid == 0)
            {
                //needs put logic here for check exist users

                  tbl_Users tblusers = new tbl_Users();
                  tblusers.Firstname = frm["txtFirstname"];
                  tblusers.Lastname = frm["txtLastname"];
                  tblusers.Birthdate = Convert.ToDateTime(frm["txtBirthdate"]);
                  tblusers.CreatedDate = DateTime.Now.Date;
                  tblusers.MobileNumber = frm["txtMobileNumber"];
                  tblusers.Address = frm["txtAddress"];
                  if (frm["hdnstatus"].ToLower().Trim() == "active")
                  {
                      tblusers.IsActive = true;
                  }
                  else
                  {
                      tblusers.IsActive = false;
                  }
                   
                    string fileName = "";

                    if (ProfilePicture != null)
                    {
                        if (ProfilePicture.ContentLength > 0 && ProfilePicture.FileName != "")
                        {
                            fileName = Guid.NewGuid() + Path.GetExtension(ProfilePicture.FileName);
                            string path = Path.Combine(Server.MapPath("~/Images/UserImages/ProfileImages"), fileName);
                            ProfilePicture.SaveAs(path);
                            tblusers.ProfileImage = fileName;
                        }
                    }


                    _db.tbl_Users.Add(tblusers);
                    _db.SaveChanges();

              


            }
            else
            {
              
                tbl_Users tblusers = _db.tbl_Users.Where(x => x.UserId == pkuserid).FirstOrDefault();
                if (tblusers != null)
                {
                    tblusers.Firstname = frm["txtFirstname"];
                    tblusers.Lastname = frm["txtLastname"];
                    tblusers.Birthdate = Convert.ToDateTime(frm["txtBirthdate"]);
                    tblusers.CreatedDate = DateTime.Now.Date;
                    tblusers.MobileNumber = frm["txtMobileNumber"];
                    tblusers.Address = frm["txtAddress"];
                    if (frm["hdnstatus"].ToLower().Trim() == "active")
                    {
                        tblusers.IsActive = true;
                    }
                    else
                    {
                        tblusers.IsActive = false;
                    }
                   
                    string fileName = "";
                    if (ProfilePicture != null)
                    {
                        if (ProfilePicture.ContentLength > 0 && ProfilePicture.FileName != "")
                        {
                            fileName = Guid.NewGuid() +  Path.GetExtension(ProfilePicture.FileName);
                            string path = Path.Combine(Server.MapPath("~/Images/UserImages/ProfileImages"), fileName);
                            ProfilePicture.SaveAs(path);
                            tblusers.ProfileImage = fileName;
                        }
                    }
                    else if (frm["hdnprofileimage"] != "")
                    {
                        tblusers.ProfileImage = frm["hdnprofileimage"];
                    }



                    _db.SaveChanges();
                }
               
            }
            return RedirectToAction("Index");



        }

    }
}
