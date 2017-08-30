using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDCOC.Models;
using System.Web.Security;
using System.Configuration;
using System.Web.Hosting;
using System.IO;

namespace SDCOC.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        SDCOCEntities _db = new SDCOCEntities();
        Common cs = new Common();
        static string baseUrl = "";

        public ActionResult Index()
        {
            ViewBag.Logout = "";
            if (TempData["Logout"] != "")
            {
                ViewBag.Logout = TempData["Logout"];
                TempData["Logout"] = "";
            }
            if (Session["userid"] != null)
            {
               
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }



        public string CheckLoginAuthentication(FormCollection frm)
        {
            string ReturnMessage = "";
            try
            {
                string unm = frm["username"].ToString();
                string pwd = cs.Encrypt(frm["password"].ToString());


                var lstLogin = _db.tbl_AdminUser.Where(m => m.EmailId == unm && m.Password == pwd && m.IsActive == true).FirstOrDefault();

                if (lstLogin != null)
                {
                    Session["userid"] = lstLogin.AdminUserId;
                    Session["Firstname"] = lstLogin.Firstname;
                    Session["Lastname"] = lstLogin.Lastname;
                    Session["EmailId"] = lstLogin.EmailId;
                    Session["MobileNumber"] = lstLogin.MobileNumber;
                    Session["Birthdate"] = lstLogin.Birthdate;
                    Session["AdminProfile"] = lstLogin.ProfilePicture;
                    ReturnMessage = "success";
                }
                else
                {
                    ReturnMessage = "fail";
                }
            }
            catch (Exception ex)
            {
                ReturnMessage = "exception";
            }
            return ReturnMessage;

        }

        public ActionResult Logout()
        {
            Session["userid"] = null;
            TempData["Logout"] = "Logged out Successfully";
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
        public string CheckForgetPassword1(FormCollection frm)
        {
            string ReturnMessage = "";
            string email = frm["email"].ToString().Trim();

            string body = "";

            body = "";



            SendEmail(email, "Reset your password in SDCOC", body);

            return ReturnMessage;
        }

        public string CheckForgetPassword(FormCollection frm)
        {
            string ReturnMessage = "";
            string email = frm["email"].ToString().Trim();
            if (!string.IsNullOrEmpty(email))
            {
                try
                {

                    var userdetails = _db.tbl_AdminUser.Where(m => m.EmailId == email && m.IsActive == true).FirstOrDefault();
                    if (userdetails != null)
                    {
                        tbl_AdminUser update_tbladminuser = _db.tbl_AdminUser.Where(x => x.AdminUserId == userdetails.AdminUserId).ToList().FirstOrDefault();
                        update_tbladminuser.ModifiedDate = DateTime.Now;

                        Random _r1 = new Random();
                        int reandomNumber = _r1.Next();
                        update_tbladminuser.ResetCode = Convert.ToString(reandomNumber);
                        string UserId = @cs.Encrypt(Convert.ToString(userdetails.AdminUserId));

                        string strbody = "";
                        string strReadHtmlFile = HostingEnvironment.MapPath("~/EmailTemplates/Sample.html");
                        StreamReader strHtmlReader = new StreamReader(strReadHtmlFile, System.Text.Encoding.UTF8);
                        try
                        {
                            strbody = strHtmlReader.ReadToEnd();
                        }
                        catch (Exception ex)
                        {
                            strbody = "";
                        }

                        strHtmlReader.Close();

                        strbody = strbody.Replace("-Subject-", "Reset Password - SDCOC");
                        strbody = strbody.Replace("-User-", userdetails.Firstname);

                        baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
                        string url = baseUrl + "/ResetPassword/Index?userId=" + UserId + "&uCode=" + reandomNumber;
                        _db.SaveChanges();
                        string desc = "<a href=" + url + ">Click here to change your password</a>";
                        string title = "Reset Password";
                        strbody = strbody.Replace("-Content-", desc);

                        //strbody = strbody.Replace("-Logo-", baseUrl + "/assets/pages/img/logo-big.png");
                        strbody = strbody.Replace("-Logo-", "https://mir-s3-cdn-cf.behance.net/project_modules/disp/b685c95120097.56289ab3b6bd6.png");

                        // string desc1 = "";
                        //desc1 = "Hello " + userdetails.FirstName + ",<br>" + desc;

                        SendEmail(email, title, strbody);// Send Email 
                        ReturnMessage = "success"; // 
                        return ReturnMessage;
                    }
                    else
                    {
                        ReturnMessage = "invalidemail";
                        return ReturnMessage;
                    }

                }
                catch (Exception ex)
                {
                    ReturnMessage = "wrong";
                    return ReturnMessage;
                }
            }
            else
            {
                ReturnMessage = "Email Address is Required";
                return ReturnMessage;
            }
        }

        //public ActionResult ResetPassword()
        //{
        //    int UserID = Convert.ToInt32(cs.Decrypt(Request.QueryString["userId"]));
        //    string ResetCode = Request.QueryString["uCode"];

        //    var record = _db.tbl_AdminUser.Where(x => x.AdminUserId == UserID && x.ResetCode == ResetCode && x.IsActive == true).FirstOrDefault();

        //    if (record != null)
        //    {

        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "FileNotFound");
        //    }

        //    return View();
        //}

        #region common function for  send email
        private bool SendEmail(string recipient, string subject, string body)
        {
            try
            {
                string Smtphost = ConfigurationManager.AppSettings["Smtphost"];
                string SmtpUsername = ConfigurationManager.AppSettings["SmtpUsername"];
                string SmtpPwd = ConfigurationManager.AppSettings["SmtpPwd"];
                string Smtpport = ConfigurationManager.AppSettings["Smtpport"];
                string Smtpsendername = ConfigurationManager.AppSettings["SmtpSenderEmail"];
                string SmtpEnablessl = ConfigurationManager.AppSettings["SmtpEnablessl"]; //added binal 22-aug-2017


                System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage(new System.Net.Mail.MailAddress(Smtpsendername, "SDCOC"), new System.Net.Mail.MailAddress(recipient));

                //   mm.IsBodyHtml = true; // This line
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = Smtphost;
                smtp.EnableSsl = Convert.ToBoolean(SmtpEnablessl); //added binal 22-aug-2017
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = SmtpUsername;
                NetworkCred.Password = SmtpPwd;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = Convert.ToInt32(Smtpport);
                smtp.Send(mm);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion


    }
}
