using SDCOC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDCOC.Controllers
{
    public class DashboardController : BaseController
    {
        //
        // GET: /Dashboard/
        SDCOCEntities _db = new SDCOCEntities();

        Common cs = new Common();
        public ActionResult Index()
        {
            
            ViewBag.Total_Users = 0;
            ViewBag.Total_Donation = 0;

            var  strDonation = (from s in _db.Get_Dashboard_Total_Donation_Details()
                       select s).ToList();
            if (strDonation != null)
            {
                ViewBag.Total_Donation = strDonation.FirstOrDefault().Value;
            }

            var strTotal_Users = (from s in _db.Get_Dashboard_Total_Users_Details()
                               select s).ToList();
            if (strTotal_Users != null)
            {
                ViewBag.Total_Users = strTotal_Users.FirstOrDefault().Value;
            }

            List<NewsEventsDetails> lstNewsEvents = new List<NewsEventsDetails>();

            lstNewsEvents = (from s in _db.Get_Dashboard_Upcoming_NewsEvents_Details()
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
            ViewData["lstNewsEvents"] = lstNewsEvents;

            List<ConnectDetails> lstConnectDetails = new List<ConnectDetails>();

            lstConnectDetails = (from s in _db.Get_Dashboard_Connect_Details()
                                 select new ConnectDetails
                                 {
                                     UserId = s.UserId,
                                     EmailId = s.EmailId,
                                     Firstname = s.Firstname,
                                     Lastname = s.Lastname,
                                     MessageText = s.MessageText,
                                     ReceivedDate = s.ReceivedDate,
                                     ConnectId = s.ConnectId
                                 }).ToList();
            ViewData["lstConnectDetails"] = lstConnectDetails;


            List<PrayerRequestDetails> lstPrayerDetails = new List<PrayerRequestDetails>();

            lstPrayerDetails = (from s in _db.Get_Dashboard_PrayerRequest_Details()
                                select new PrayerRequestDetails
                                {
                                    PrayerRequestId = s.PrayerRequestId,
                                    UserId = s.UserId,
                                    EmailId = s.EmailId,
                                    Firstname = s.Firstname,
                                    Lastname = s.Lastname,
                                    PrayerRequestName = s.Name,
                                    ReceivedDate = s.ReceivedDate,
                                    PrayerRequestText = s.PrayerRequestText
                                }).ToList();

            ViewData["lstPrayerDetails"] = lstPrayerDetails;

            return View();
        }

    }
}
