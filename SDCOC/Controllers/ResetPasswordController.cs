using SDCOC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDCOC.Controllers
{
    public class ResetPasswordController : Controller
    {
        //
        // GET: /ResetPassword/
        SDCOCEntities _db = new SDCOCEntities();
        Common cs = new Common();
        static string baseUrl = "";
        public ActionResult Index()
        {
            int UserID = Convert.ToInt32(cs.Decrypt(Request.QueryString["userId"]));
            ViewBag.UserID = Request.QueryString["userId"];
            string ResetCode = Request.QueryString["uCode"];
            ViewBag.ResetCode = Request.QueryString["uCode"];

            var record = _db.tbl_AdminUser.Where(x => x.AdminUserId == UserID && x.ResetCode == ResetCode && x.IsActive == true).FirstOrDefault();

            if (record != null)
            {

            }
            else
            {
                return RedirectToAction("Expire", "ResetPassword");
            }

            return View();
        }

        [HttpPost]
        public string SavePassword(FormCollection frm)
        {
            string ReturnMessage = "";

            try
            {


                string NewPassword = Convert.ToString(frm["password"]).Trim();
                long AdminId = Convert.ToInt64(cs.Decrypt(frm["hdnuid"]));
                string resetcode=frm["hdncodid"].Trim();


                var userdata = _db.tbl_AdminUser.Where(x => x.AdminUserId == AdminId && x.ResetCode == resetcode).FirstOrDefault();

                if (userdata != null)
                {
                    userdata.Password = cs.Encrypt(NewPassword);
                    userdata.ResetCode = "";
                    _db.SaveChanges();
                    ReturnMessage = "success";
                }
                else
                {
                    ReturnMessage = "Reset code is already used !";
                }
                
            }
            catch (Exception ex)
            {
                ReturnMessage = "fail";
            }

            return ReturnMessage;
        }

        public ActionResult Expire()
        {
            return View();
        }
    }
}
