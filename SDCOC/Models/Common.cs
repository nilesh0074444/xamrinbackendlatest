using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDCOC.Models
{
    public class Common
    {
        /* ENCRYPTION */
        public string Encrypt(string text = "")
        {
            byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(text);
            string EncryptText = Convert.ToBase64String(bytes);
            return EncryptText;
        }
        /* DECRYPTION */
        public string Decrypt(string text)
        {
            byte[] bytes = Convert.FromBase64String(text);
            string DescriptText = System.Text.ASCIIEncoding.ASCII.GetString(bytes);
            return DescriptText;
        }

        public static string TimeAgo(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("about {0} {1} ago",
                years, years == 1 ? "year" : "years");
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format("about {0} {1} ago",
                months, months == 1 ? "month" : "months");
            }
            if (span.Days > 0)
                return String.Format("about {0} {1} ago",
                span.Days, span.Days == 1 ? "day" : "days");
            if (span.Hours > 0)
                return String.Format("about {0} {1} ago",
                span.Hours, span.Hours == 1 ? "hour" : "hours");
            if (span.Minutes > 0)
                return String.Format("about {0} {1} ago",
                span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
            if (span.Seconds > 5)
                return String.Format("about {0} seconds ago", span.Seconds);
            if (span.Seconds <= 5)
                return "just now";
            return string.Empty;
        }
    }
    public class SignupUser
    {
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MobileNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
        public string ProfileImage { get; set; }
    }

    public class UpdatePassword
    {
        public long UserId { get; set; }
        public string Password { get; set; }
    }

    public class GetUserSetting
    {
        public long UserId { get; set; }
        public string EmailId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MobileNumber { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Address { get; set; }
        public bool? IsReceiveEmailSMS { get; set; }
        public string ProfileImage { get; set; }
        public List<AssignedMinistries> LstAssignedMinistries  { get; set; }
    }
    public class AssignedMinistries
    {
        public long? FK_UserId { get; set; }
        public long? MinistryId { get; set; }
        public string MinistryTitle { get; set; }
        public long? Assign_MinistryId { get; set; }
    }
    public class UpdateAccountPersonelDetails
    {
        public long UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MobileNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
        public bool? IsReceiveEmailSMS { get; set; }
        public string ProfileImage { get; set; }
        public string AssignedMinistriesID { get; set; }
    }

    public class Donation
    {
        public decimal? amount { get; set; }
        public bool autoRecurring { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailId { get; set; }
        public string mobileNumber { get; set; }
        public string paymentType { get; set; }
        public string cardHolderName { get; set; }
        public string cardNumber { get; set; }
        public string cvvCode { get; set; }
        public string ExpirationDate { get; set; }
        public long donationHistoryId { get; set; }
        public DateTime donationDate { get; set; }


    }
    public class connect
    {
        public long userId { get; set; }
        public string Message { get; set; }
        public DateTime ReceivedDate { get; set; }

    }

    public class SubjectMessage
    {
        public long subjectId { get; set; }
        public long MessageFromId { get; set; }
        public long MessageToId { get; set; }
        public string MessageText { get; set; }
    }

    public class subject
    {
        public long SubjectId { get; set; }
        public string SubjectName { get; set; }
        public long? SubjectUserId { get; set; }
        public DateTime? SubjectCreatedDate { get; set; }
        public long? SubjectCreatedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool IsRead { get; set; }
    }
    public class CreateMessage
    {
        public string subject { get; set; }
        public string Message { get; set; }
        public long LoggedUserId { get; set; }
        public long MessageFrom { get; set; }
        public long MessageTo { get; set; }
    }

    public class CreatePrayerRequest
    {
        public string Name { get; set; }
        public string PrayerRequestText { get; set; }
        public long LoggedUserId { get; set; }

    }

    public class UserDetails
    {
        public long UserId { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MobileNumber { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Address { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsReceiveEmailSMS { get; set; }
        public string ReceivedOTP { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ROWNUM { get; set; }
        public long? TotalCount { get; set; }
        public string ProfileImage { get; set; }
    }

    public class ScheduleDetails
    {
        public long ScheduleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? EventDate { get; set; }
        public string EventStartTime { get; set; }
        public string EventEndTime { get; set; }
        public string ScheduleType { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ROWNUM { get; set; }
        public long? TotalCount { get; set; }
        public long? ScheduleCategory_ID { get; set; }
    }

    public class ScheduleCategeoryDetails
    {
        public long ScheduleTypeId { get; set; }
        public string ScheduleType { get; set; }
        public bool? ISdeleted { get; set; }
        public long? ROWNUM { get; set; }
        public long? TotalCount { get; set; }

    }

    public class NewsEventsDetails
    {
        public long NewsEventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Eventdate { get; set; }
        public string EventTime { get; set; }
        public string EventImage { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ROWNUM { get; set; }
        public long? TotalCount { get; set; }
    }

    public class ConnectDetails
    {
        public long ConnectId { get; set; }
        public long? UserId { get; set; }
        public string EmailId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MessageText { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public long? ROWNUM { get; set; }
        public long? TotalCount { get; set; }
    }

    public class PrayerRequestDetails
    {
        public long PrayerRequestId { get; set; }
        public long? UserId { get; set; }
        public string EmailId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PrayerRequestName { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string PrayerRequestText { get; set; }
        public long? ROWNUM { get; set; }
        public long? TotalCount { get; set; }
    }

    public class DonationDetails
    {

        public long DonationId { get; set; }
        public long? UserId { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public decimal? DonationAmount { get; set; }
        public string PaymentType { get; set; }
        public bool? IsAutoRecurring { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? TotalCount { get; set; }
        public long? ROWNUM { get; set; }
    }

    public class MessageDetails
    {
        public long? SubjectId { get; set; }
        public string SubjectName { get; set; }
        public long? SubjectUserId { get; set; }
        public DateTime? SubjectCreatedDate { get; set; }
        public long? SubjectCreatedBy { get; set; }
        public string LastMessage { get; set; }
        public DateTime? MessageSendDate { get; set; }
        public bool? IsReaded { get; set; }
        public long? MessageSubjectId { get; set; }
        public long? MessageTo { get; set; }
        public long? MessageFrom { get; set; }
        public long? TotalCount { get; set; }
        public long? ROWNUM { get; set; }
        public string Message { get; set; }
    }

    public class MeesageNotications
    {
        public long MessageNotificationId { get; set; }
        public long? MessageReceivedUserId { get; set; }
        public long? MessageSubjectId { get; set; }
        public string MessageText { get; set; }
        public bool? IsReaded { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? MessageReceivedDate { get; set; }
        public long? MessageSendUserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string ProfileImage { get; set; }
    }
    public class OtherPendingNotications
    {
        public long OtherNotificationId { get; set; }
        public string DisplayText { get; set; }
        public long? NotificationReceivedUserId { get; set; }
        public string FormName { get; set; }
        public bool? IsReaded { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? NotificationReceivedDate { get; set; }



    }
    public class MinistriesInterests
    {

        public long MinistryId { get; set; }
        public string MinistryTitle { get; set; }
        public bool? IsDeleted { get; set; }
        public long? TotalCount { get; set; }
        public long? ROWNUM { get; set; }
    }
    public class DonationHistory
    {

        public long DonationHistoryId { get; set; }
        public long? DonationId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public decimal? DonatedAmount { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string PaymentType { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string CVVCode { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? DonationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? TotalCount { get; set; }
        public long? ROWNUM { get; set; }
    }
    public class EditDonationDetails
    {
        public long? DonationId { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public decimal? DonatedAmount { get; set; }
        public DateTime? DonationDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsAutoRecurring { get; set; }
    }


}