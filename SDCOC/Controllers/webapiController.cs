using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SDCOC.Models;
using AttributeRouting;
using AttributeRouting.Web.Http;
using System.Configuration;

namespace SDCOC.Controllers
{
    [RoutePrefix("api")]
    public class webapiController : ApiController
    {
        SDCOCEntities dbContext = new SDCOCEntities();
        [HttpGet]
        [GET("GetAllUser")]
        public HttpResponseMessage GetAdminUsers()
        {
            var lstusers = dbContext.tbl_Users.ToList();

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, lstusers);
            return response;
        }

        [HttpGet]
        [GET("login/{username}/{password}")]
        public HttpResponseMessage GetLoggedUserDetails(string username, string password)
        {
            try
            {
                var _data = (from u in dbContext.tbl_Users
                             where u.EmailId.Equals(username) && u.Password.Equals(password)
                             select u).FirstOrDefault();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, _data);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }
        [HttpGet]
        [GET("checkexitsemail/{emailid}/{otp}")]
        public HttpResponseMessage CheckEmailIdExist(string emailid, string otp)
        {
            bool result = false;
            try
            {
                var data = (from usr in dbContext.tbl_Users
                            where usr.EmailId.Equals(emailid)
                            select usr.EmailId).FirstOrDefault();
                if (data != null)
                {
                    result = true;
                }

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }

        [HttpPost]
        [POST("SignupUser")]
        public HttpResponseMessage SignUpUser([FromBody]SignupUser signupUser)
        {
            try
            {
                tbl_Users DBUser = new tbl_Users();
                DBUser.EmailId = signupUser.EmailId.ToString().Trim();
                DBUser.Password = signupUser.Password.ToString().Trim();
                DBUser.Firstname = signupUser.Firstname.ToString().Trim();
                DBUser.Lastname = signupUser.Lastname.ToString().Trim();
                DBUser.MobileNumber = signupUser.MobileNumber.ToString().Trim();
                string date = signupUser.Birthdate.Date.ToString("MM/dd/yyyy");
                DBUser.Birthdate = Convert.ToDateTime(date);
                dbContext.tbl_Users.Add(DBUser);
                dbContext.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, DBUser.UserId);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }

        [HttpGet]
        [GET("GetAllNewsAndEvents")]
        public HttpResponseMessage GetNewsAndEventList()
        {
            try
            {
                var data = (from en in dbContext.tbl_NewsEvents
                            where en.IsActive == true && en.IsDeleted == false
                            select en).ToList();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);
                return response;
            }
            catch
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }

        [HttpGet]
        [GET("GetAllNewsAndEventsById/{id}")]
        public HttpResponseMessage GetNewsAndEventById(long id)
        {
            try
            {
                var data = (from en in dbContext.tbl_NewsEvents
                            where en.IsActive == true && en.IsDeleted == false && en.NewsEventId.Equals(id)
                            select en).ToList();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);
                return response;
            }
            catch
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }


        [HttpGet]
        [GET("GetAllUserDetailsById/{id}")]
        public HttpResponseMessage GetUserDetailsId(long id)
        {
            try
            {
                var data = dbContext.tbl_Users.Where(x => x.UserId.Equals(id)).FirstOrDefault();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);
                return response;
            }
            catch
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }

        [HttpGet]
        [GET("SendOPTEmail")]
        public HttpResponseMessage Sendotpemail(string toemail, string otp)
        {
            try
            {
                string Smtphost = ConfigurationManager.AppSettings["Smtphost"];
                string SmtpUsername = ConfigurationManager.AppSettings["SmtpUsername"];
                string SmtpPwd = ConfigurationManager.AppSettings["SmtpPwd"];
                string Smtpport = ConfigurationManager.AppSettings["Smtpport"];
                string Smtpsendername = ConfigurationManager.AppSettings["SmtpSenderEmail"];
                string SmtpEnablessl = ConfigurationManager.AppSettings["SmtpEnablessl"]; //added binal 22-aug-2017


                System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage(new System.Net.Mail.MailAddress(Smtpsendername, "SDCOC"), new System.Net.Mail.MailAddress(toemail));

                //   mm.IsBodyHtml = true; // This line
                mm.Subject = "New generated OTP ";
                mm.Body = "Please enter latest otp in your app and change your password. Your new generated opt is" + otp;
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

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, true);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }


        }

        [HttpGet]
        [GET("ScheduleCategory")]
        public HttpResponseMessage GetScheduleCategory()
        {
            try
            {
                var data = (from sc in dbContext.ScheduleCategories
                            where sc.ISdeleted == false
                            select sc).ToList();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }

        [HttpGet]
        [GET("GetScheduleByCategoryId/{id}")]
        public HttpResponseMessage GetScheduleByCategoryId(long id)
        {
            try
            {
                var data = dbContext.tbl_Schedule.Where(x => x.IsActive == true && x.IsDeleted == false && x.ScheduleCategory_ID == id).ToList();
                if (data.Count > 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "No Record Found");
                    return response;
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }

        [HttpGet]
        [GET("GetDonation/{userId}")]
        public HttpResponseMessage GetDonationList(long userId)
        {
            try
            {
                var data = dbContext.tbl_Donation.Where(x => x.IsActive == true && x.IsDeleted == false && x.UserId == userId && x.IsAutoRecurring == false).ToList();
                if (data.Count > 0)
                {

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "No Record Found");
                    return response;
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }

        [HttpGet]
        [GET("GetDonationHistoryById/{id}")]
        public HttpResponseMessage GetDonationById(long id)
        {
            try
            {
                var data = dbContext.tbl_DonationHistory.Where(x => x.IsActive == true && x.IsDeleted == false && x.DonationId == id).FirstOrDefault();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }

        [HttpGet]
        [GET("UpdateActiveInActiveRecurring/{id}/{active}")]
        public HttpResponseMessage UpdateAutoRecurring(long id, bool active)
        {
            try
            {
                var data = dbContext.tbl_Donation.Where(x => x.DonationId == id).FirstOrDefault();
                if (data != null)
                {
                    data.IsAutoRecurring = active;
                    dbContext.SaveChanges();
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Updated record successfully");
                    return response;
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "No record found");
                    return response;
                }

            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }

        [HttpGet]
        [GET("GetAutoRecurring/{userId}")]
        public HttpResponseMessage GetAutoRecurring(long userId)
        {

            var data = dbContext.tbl_Donation.Where(x => x.IsActive == true && x.IsDeleted == false && x.UserId == userId && x.IsAutoRecurring == true).ToList();
            if (data.Count > 0)
            {

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);
                return response;
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "No record found");
                return response;
            }
        }

        [HttpGet]
        [GET("GetAutoRecurringHistory/{donationId}")]
        public HttpResponseMessage GetAutoRecurringHistoryDetails(long donationId)
        {
            try
            {
                var data = (from aur in dbContext.tbl_DonationHistory
                            where aur.DonationId == donationId && aur.IsDeleted == false && aur.IsActive == true
                            select aur).ToList();
                if (data.Count > 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);
                    return response;

                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "No record found");
                    return response;
                }

            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }

        [HttpPost]
        [POST("UpdateDonation")]
        public HttpResponseMessage UpdateDonation([FromBody]Donation donationDetails)
        {
            try
            {
                var data = (from dh in dbContext.tbl_DonationHistory
                            where dh.DonationHistoryId.Equals(donationDetails.donationHistoryId)
                            select dh).FirstOrDefault();
                if (data != null)
                {
                    data.Firstname = donationDetails.firstName;
                    data.Lastname = donationDetails.lastName;
                    data.MobileNumber = donationDetails.mobileNumber;
                    data.EmailId = donationDetails.emailId;
                    data.DonatedAmount = donationDetails.amount;
                    data.PaymentType = donationDetails.paymentType;
                    data.CardHolderName = donationDetails.cardHolderName;
                    data.CardNumber = donationDetails.cardNumber;
                    data.ExpirationDate = donationDetails.ExpirationDate;
                    data.DonationDate = DateTime.Now.ToUniversalTime();
                    data.ModifiedDate = DateTime.Now.ToUniversalTime();
                    dbContext.SaveChanges();
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Record updated successfully");
                    return response;
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "No record found");
                    return response;
                }


            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }

        [HttpPost]
        [POST("CreateMessage")]
        public HttpResponseMessage CreateMessage([FromBody]connect connectDetails)
        {
            try
            {
                tbl_Connect DBConnect = new tbl_Connect();
                DBConnect.UserId = connectDetails.userId;
                DBConnect.MessageText = connectDetails.Message;
                DBConnect.ReceivedDate = DateTime.Now.ToUniversalTime();
                DBConnect.IsDeleted = false;
                dbContext.tbl_Connect.Add(DBConnect);
                dbContext.SaveChanges();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, DBConnect.ConnectId);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }

        [HttpGet]
        [GET("SubjectList/{userId}")]
        public HttpResponseMessage GetSubjectList(long userId)
        {
            try
            {
                var data = (from sb in dbContext.tbl_Subjects
                            where sb.IsActive == true && sb.IsDeleted == false && sb.SubjectUserId == userId
                            select new subject
                            {
                                SubjectId = sb.SubjectId,
                                SubjectName = sb.SubjectName,
                                SubjectUserId = sb.SubjectUserId,
                                SubjectCreatedDate = sb.SubjectCreatedDate,
                                SubjectCreatedBy = sb.SubjectCreatedBy,
                                IsActive = sb.IsActive,
                                IsDeleted = sb.IsDeleted,
                                IsRead = false
                            }).OrderByDescending(x => x.SubjectCreatedDate).ToList();
                if (data.Count > 0)
                {
                    List<subject> lstSubject = new List<subject>();
                    subject objSubject;
                    foreach (var item in data)
                    {
                        objSubject = new subject();
                        objSubject.SubjectId = item.SubjectId;
                        objSubject.SubjectName = item.SubjectName;
                        objSubject.SubjectUserId = item.SubjectUserId;
                        objSubject.SubjectCreatedDate = item.SubjectCreatedDate;
                        objSubject.SubjectCreatedBy = item.SubjectCreatedBy;
                        objSubject.IsActive = item.IsActive;
                        objSubject.IsDeleted = item.IsDeleted;
                        var subjectRead = dbContext.tbl_MessageSubject.Where(x => x.SubjectId == item.SubjectId && x.MessageFrom == userId).ToList();
                        if (subjectRead.Count > 0)
                        {
                            bool result = false;
                            foreach (var rdItem in subjectRead)
                            {

                                if (rdItem.IsReaded == true)
                                {
                                    result = true;
                                }
                                else
                                {
                                    result = false;
                                }
                            }
                            objSubject.IsRead = result;
                        }
                        lstSubject.Add(objSubject);
                    }
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, lstSubject);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "No record found");
                    return response;
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occurred");
                return response;
            }
        }

        [HttpGet]
        [GET("GetSubjectMessageList/{subjectId}/{loggedUserId}")]

        public HttpResponseMessage GetSubjectMessageList(long subjectId, long loggedUserId)
        {
            try
            {
                var data = (from dd in dbContext.tbl_MessageSubject
                            where dd.SubjectId == subjectId && dd.IsDeleted == false
                            select dd).ToList();
                if (data.Count > 0)
                {
                    var dataread = data.Where(x => x.SubjectId == subjectId && x.MessageFrom == loggedUserId).ToList();
                    foreach (var item in dataread)
                    {
                        item.IsReaded = true;
                        dbContext.SaveChanges();
                    }


                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "No Record found");
                    return response;
                }


            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }

        [HttpPost]
        [POST("SendMessage")]
        public HttpResponseMessage SaveSendMessage([FromBody]SubjectMessage subjectMessage)
        {
            try
            {
                tbl_MessageSubject DBMsgSubject = new tbl_MessageSubject();
                DBMsgSubject.SubjectId = subjectMessage.subjectId;
                DBMsgSubject.MessageFrom = subjectMessage.MessageFromId;
                DBMsgSubject.MessageTo = subjectMessage.MessageToId;
                DBMsgSubject.MessageText = subjectMessage.MessageText;
                DBMsgSubject.MessageSendDate = DateTime.Now.ToUniversalTime();
                DBMsgSubject.IsReaded = false;
                DBMsgSubject.IsDeleted = false;
                dbContext.tbl_MessageSubject.Add(DBMsgSubject);
                dbContext.SaveChanges();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, DBMsgSubject.MessageSubjectId);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occurred");
                return response;
            }
        }


        [HttpGet]
        [GET("deleteMessage/{MessageId}")]
        public HttpResponseMessage deleteMessage(string MessageId)
        {
            try
            {
                string[] MessageSubjectId = MessageId.Split(',');
                if (MessageSubjectId.Length > 0)
                {
                    foreach (var ids in MessageSubjectId)
                    {
                        long id = Convert.ToInt64(ids);
                        var data = dbContext.tbl_MessageSubject.Where(x => x.MessageSubjectId == id).ToList();
                        if (data != null)
                        {
                            foreach (var item in data)
                            {
                                item.IsDeleted = true;
                                dbContext.SaveChanges();
                            }
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Message Deleted Successfully ");
                            return response;
                        }
                        else
                        {
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Record not found");
                            return response;
                        }
                    }
                    HttpResponseMessage response1 = Request.CreateResponse(HttpStatusCode.OK, "Record not found");
                    return response1;
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Record not found");
                    return response;
                }

            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occurred");
                return response;
            }
        }

        [HttpPost]
        [POST("CreateSubject")]
        public HttpResponseMessage CreateSubject([FromBody]CreateMessage createMessage)
        {
            try
            {
                tbl_Subjects DBSubject = new tbl_Subjects();
                DBSubject.SubjectName = createMessage.subject;
                DBSubject.SubjectUserId = createMessage.LoggedUserId;
                DBSubject.SubjectCreatedDate = DateTime.Now.ToUniversalTime();
                DBSubject.SubjectCreatedBy = createMessage.LoggedUserId;
                DBSubject.IsActive = true;
                DBSubject.IsDeleted = false;
                dbContext.tbl_Subjects.Add(DBSubject);
                tbl_MessageSubject DBMsgSub = new tbl_MessageSubject();
                DBMsgSub.SubjectId = DBSubject.SubjectId;
                DBMsgSub.MessageFrom = createMessage.MessageFrom;
                DBMsgSub.MessageTo = createMessage.MessageTo;
                DBMsgSub.MessageText = createMessage.Message;
                DBMsgSub.MessageSendDate = DateTime.Now.ToUniversalTime();
                DBMsgSub.IsReaded = false;
                DBMsgSub.IsDeleted = false;
                dbContext.tbl_MessageSubject.Add(DBMsgSub);
                dbContext.SaveChanges();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Record inserted Successfully");
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occoured");
                return response;
            }
        }

        [HttpPost]
        [POST("CreatePrayerRequest")]
        public HttpResponseMessage CreatePrayerRequest([FromBody]CreatePrayerRequest createPrayer)
        {
            try
            {
                tbl_PrayerRequest DBPrayer = new tbl_PrayerRequest();
                DBPrayer.UserId = createPrayer.LoggedUserId;
                DBPrayer.Name = createPrayer.Name;
                DBPrayer.PrayerRequestText = createPrayer.PrayerRequestText;
                DBPrayer.ReceivedDate = DateTime.Now.ToUniversalTime();
                DBPrayer.IsDeleted = false;
                dbContext.tbl_PrayerRequest.Add(DBPrayer);
                dbContext.SaveChanges();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Record inserted Successfully");
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occoured");
                return response;
            }
        }



        [HttpGet]
        [GET("GetPrayerRequestDetails/{PrayerRequestId}")]
        public HttpResponseMessage GetPrayerRequestDetails(long PrayerRequestId)
        {
            try
            {
                var data = (from aur in dbContext.tbl_PrayerRequest
                            //join us in dbContext.tbl_Users
                            where aur.PrayerRequestId == PrayerRequestId && aur.IsDeleted == false || aur.IsDeleted == null
                            select aur).ToList();
                if (data.Count > 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data);
                    return response;

                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "No record found");
                    return response;
                }

            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }




        [HttpPost]
        [POST("UpdatePassword")]
        public HttpResponseMessage UpdatePassword([FromBody]UpdatePassword UpdatePassword)
        {
            try
            {
                var data = (from dh in dbContext.tbl_Users
                            where dh.UserId.Equals(UpdatePassword.UserId)
                            select dh).FirstOrDefault();
                if (data != null)
                {
                    data.Password = UpdatePassword.Password;

                    dbContext.SaveChanges();
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Password updated successfully");
                    return response;
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "No record found");
                    return response;
                }


            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }


        [HttpGet]
        [GET("GetUserSetting/{UserID}")]
        public HttpResponseMessage GetUserSetting(long UserID)
        {
            try
            {
                GetUserSetting lstgst = new Models.GetUserSetting();
                var data = (from aur in dbContext.tbl_Users
                            where aur.UserId == UserID && aur.IsDeleted == false || aur.IsDeleted == null
                            select aur).ToList();
                if (data.Count > 0)
                {
                    lstgst.Address = data.FirstOrDefault().Address;
                    lstgst.UserId = data.FirstOrDefault().UserId;
                    lstgst.EmailId = data.FirstOrDefault().EmailId;
                    lstgst.Firstname = data.FirstOrDefault().Firstname;
                    lstgst.Lastname = data.FirstOrDefault().Lastname;
                    lstgst.MobileNumber = data.FirstOrDefault().MobileNumber;
                    lstgst.Birthdate = data.FirstOrDefault().Birthdate;
                    lstgst.Address = data.FirstOrDefault().Address;
                    lstgst.IsReceiveEmailSMS = data.FirstOrDefault().IsReceiveEmailSMS;
                    lstgst.ProfileImage = data.FirstOrDefault().ProfileImage;

                    List<AssignedMinistries> lstassigned = new List<AssignedMinistries>();
                    lstassigned = (from s in dbContext.Get_Assigned_Unassigned_tbl_MinistryInterests(UserID)

                                   select new AssignedMinistries
                                   {
                                       FK_UserId = s.FK_User_ID,
                                       MinistryId = s.MinistryId,
                                       MinistryTitle = s.MinistryTitle,
                                       Assign_MinistryId = s.Assign_MinistryId
                                   }).ToList();
                    lstgst.LstAssignedMinistries = lstassigned;
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, lstgst);
                    return response;

                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "No record found");
                    return response;
                }

            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }


        [HttpPost]
        [POST("UpdatePersonelAccountDetails")]
        public HttpResponseMessage UpdatePersonelAccountDetails([FromBody]UpdateAccountPersonelDetails UpdateAccountPersonelDetails)
        {
            try
            {
                var data = (from dh in dbContext.tbl_Users
                            where dh.UserId.Equals(UpdateAccountPersonelDetails.UserId)
                            select dh).FirstOrDefault();
                if (data != null)
                {
                    data.Firstname = UpdateAccountPersonelDetails.Firstname;
                    data.Lastname = UpdateAccountPersonelDetails.Lastname;
                    data.MobileNumber = UpdateAccountPersonelDetails.MobileNumber;
                    if (!string.IsNullOrEmpty(Convert.ToString(UpdateAccountPersonelDetails.Birthdate)))
                    {
                        data.Birthdate = UpdateAccountPersonelDetails.Birthdate;
                    }
                    dbContext.SaveChanges();
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Record updated successfully");
                    return response;
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "No record found");
                    return response;
                }


            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }


        [HttpPost]
        [POST("UpdatePersonelAddressDetails")]
        public HttpResponseMessage UpdatePersonelAddressDetails([FromBody]UpdateAccountPersonelDetails UpdateAccountPersonelDetails)
        {
            try
            {
                var data = (from dh in dbContext.tbl_Users
                            where dh.UserId.Equals(UpdateAccountPersonelDetails.UserId)
                            select dh).FirstOrDefault();
                if (data != null)
                {

                    data.Address = UpdateAccountPersonelDetails.Address;

                    dbContext.SaveChanges();
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Record updated successfully");
                    return response;
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "No record found");
                    return response;
                }


            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }

        [HttpPost]
        [POST("UpdatePersonelMinistriesOfInterestsDetails")]
        public HttpResponseMessage UpdatePersonelMinistriesOfInterestsDetails([FromBody]UpdateAccountPersonelDetails UpdateAccountPersonelDetails)
         {
             try
             {
                 dbContext.Delete_Assigned_Unassigned_tbl_MinistryInterests(UpdateAccountPersonelDetails.UserId); //delete old entries

                 var data = (from dh in dbContext.tbl_Users
                             where dh.UserId.Equals(UpdateAccountPersonelDetails.UserId)
                             select dh).FirstOrDefault();
                 if (data != null)
                 {

                     if (UpdateAccountPersonelDetails.AssignedMinistriesID.Contains(","))
                     {
                         string[] aryMinistriesID = UpdateAccountPersonelDetails.AssignedMinistriesID.Split(',');
                         foreach (var i in aryMinistriesID)
                         {
                             if(!string.IsNullOrEmpty(Convert.ToString(i)))
                             {
                                     SDCOCEntities dbContext1 = new SDCOCEntities();
                                     tbl_Ministry_Assigned_Users tblMinistry_Assigned_Users = new tbl_Ministry_Assigned_Users();
                                     tblMinistry_Assigned_Users.FK_MinistryId = Convert.ToInt64(i);
                                     tblMinistry_Assigned_Users.FK_User_ID = UpdateAccountPersonelDetails.UserId;
                                     dbContext1.tbl_Ministry_Assigned_Users.Add(tblMinistry_Assigned_Users);
                                     dbContext1.SaveChanges();
                             }
                         }

                     }
                     else
                     {
                         SDCOCEntities dbContext1 = new SDCOCEntities();
                         tbl_Ministry_Assigned_Users tblMinistry_Assigned_Users = new tbl_Ministry_Assigned_Users();
                         tblMinistry_Assigned_Users.FK_MinistryId = Convert.ToInt64(UpdateAccountPersonelDetails.AssignedMinistriesID);
                         tblMinistry_Assigned_Users.FK_User_ID = UpdateAccountPersonelDetails.UserId;
                         dbContext1.tbl_Ministry_Assigned_Users.Add(tblMinistry_Assigned_Users);
                         dbContext1.SaveChanges();

                     }


                     HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Record updated successfully");
                     return response;
                 }
                 else
                 {
                     HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "No record found");
                     return response;
                 }


             }
             catch (Exception ex)
             {
                 HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                 return response;
             }
         }

        [HttpPost]
        [POST("UpdatePersonelProfileImage")]
        public HttpResponseMessage UpdatePersonelProfileImage([FromBody]UpdateAccountPersonelDetails UpdateAccountPersonelDetails)
        {
            try
            {
               

                var data = (from dh in dbContext.tbl_Users
                            where dh.UserId.Equals(UpdateAccountPersonelDetails.UserId)
                            select dh).FirstOrDefault();
                if (data != null)
                {

                    data.ProfileImage = UpdateAccountPersonelDetails.ProfileImage;

                    dbContext.SaveChanges();
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Record updated successfully");
                    return response;
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "No record found");
                    return response;
                }


            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }




        [HttpGet]
        [GET("GetMessageNotificationCounter/{UserId}")]
        public HttpResponseMessage GetMessageNotificationCounter(long UserId)
        {
            try
            {
                var data = (from aur in dbContext.tbl_MessageNotification
                            //join us in dbContext.tbl_Users
                            where aur.MessageReceivedUserId == UserId && (aur.IsDeleted == false || aur.IsDeleted == null) &&  (aur.IsReaded==null || aur.IsReaded==false)
                            select aur).ToList();
                if (data.Count > 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data.Count);
                    return response;

                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "No record found");
                    return response;
                }

            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }




        [HttpGet]
        [GET("GetActiveInActiveAutoRecurring/{DonationId}")]
        public HttpResponseMessage GetActiveInActiveAutoRecurring(long DonationId)
        {
            try
            {
                var data = (from aur in dbContext.tbl_Donation
                            //join us in dbContext.tbl_Users
                            where aur.DonationId == DonationId 
                            select aur).ToList();
                if (data.Count > 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, data.FirstOrDefault().IsAutoRecurring);
                    return response;

                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "No record found");
                    return response;
                }

            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }



        [HttpGet]
        [GET("GetDonationautoRecurrringeditgetlastpaydetails")]
        public HttpResponseMessage GetDonationautoRecurrringeditgetlastpaydetails()
        {
            try
            {
                List<EditDonationDetails> lstdonation = new List<EditDonationDetails>();
                lstdonation = (from s in dbContext.Get_DonationautoRecurrringeditgetlastpaydetails()
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
                                   DonationDate = s.DonationDate

                               }).ToList();

               
                if (lstdonation.Count > 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, lstdonation);
                    return response;

                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "No record found");
                    return response;
                }

            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "An error has occured");
                return response;
            }
        }
     
        
    }





}