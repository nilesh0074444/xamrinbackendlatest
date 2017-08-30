using SDCOC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDCOC.Controllers
{
    public class NotificationsController : BaseController
    {
        //
        // GET: /Notifications/
        SDCOCEntities _db = new SDCOCEntities();

        Common cs = new Common();
        public PartialViewResult PendingNotifications()
        {
            List<OtherPendingNotications> lstOtherPendingNotications = new List<OtherPendingNotications>();
            lstOtherPendingNotications = (from s in _db.GetAll_tbl_OtherNotification_Details()
                                          select new OtherPendingNotications
                                          {
                                              OtherNotificationId = s.OtherNotificationId,
                                              DisplayText = s.DisplayText,
                                              NotificationReceivedUserId = s.NotificationReceivedUserId,
                                              FormName = s.FormName,
                                              IsReaded = s.IsReaded,
                                              IsDeleted = s.IsDeleted,
                                              NotificationReceivedDate = s.NotificationReceivedDate

                                          }).ToList();
            return PartialView("~/Views/Notifications/Partial/PendingNotifications.cshtml", lstOtherPendingNotications);
        }
        public PartialViewResult NewMessages()
        {
            List<MeesageNotications> lstMeesageNotications = new List<MeesageNotications>();
            lstMeesageNotications = (from s in _db.GetAll_tbl_MessageNotification_Details()
                                     select new MeesageNotications
                                     {
                                         MessageNotificationId = s.MessageNotificationId,
                                         MessageReceivedUserId = s.MessageReceivedUserId,
                                         MessageSubjectId = s.MessageSubjectId,
                                         MessageText = s.MessageText,
                                         IsReaded = s.IsReaded,
                                         IsDeleted = s.IsDeleted,
                                         MessageReceivedDate = s.MessageReceivedDate,
                                         MessageSendUserId = s.MessageSendUserId,
                                         Firstname = s.Firstname,
                                         Lastname = s.Lastname,
                                         ProfileImage = s.ProfileImage
                                     }).ToList();

            return PartialView("~/Views/Notifications/Partial/NewMessages.cshtml", lstMeesageNotications);
        }

        public JsonResult Readpendingnotifcations(long ID)
        {

            _db.Update_tbl_OtherNotification(0);//0 for admin
            return Json("success", JsonRequestBehavior.AllowGet);

        }
        public JsonResult ReadMessagesnotifcations(long ID)
        {

            _db.Update_tbl_MessageNotification(0);//0 for admin
            return Json("success", JsonRequestBehavior.AllowGet);

        }
    }
}
