﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SDCOC.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class SDCOCEntities : DbContext
    {
        public SDCOCEntities()
            : base("name=SDCOCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ScheduleCategory> ScheduleCategories { get; set; }
        public DbSet<tbl_AdminUser> tbl_AdminUser { get; set; }
        public DbSet<tbl_Connect> tbl_Connect { get; set; }
        public DbSet<tbl_Donation> tbl_Donation { get; set; }
        public DbSet<tbl_DonationHistory> tbl_DonationHistory { get; set; }
        public DbSet<tbl_MessageNotification> tbl_MessageNotification { get; set; }
        public DbSet<tbl_MessageSubject> tbl_MessageSubject { get; set; }
        public DbSet<tbl_MinistryInterests> tbl_MinistryInterests { get; set; }
        public DbSet<tbl_NewsEvents> tbl_NewsEvents { get; set; }
        public DbSet<tbl_OtherNotification> tbl_OtherNotification { get; set; }
        public DbSet<tbl_PrayerRequest> tbl_PrayerRequest { get; set; }
        public DbSet<tbl_Schedule> tbl_Schedule { get; set; }
        public DbSet<tbl_Setting> tbl_Setting { get; set; }
        public DbSet<tbl_Subjects> tbl_Subjects { get; set; }
        public DbSet<tbl_Users> tbl_Users { get; set; }
        public DbSet<tbl_Ministry_Assigned_Users> tbl_Ministry_Assigned_Users { get; set; }
    
        public virtual ObjectResult<Get_Dashboard_Connect_Details_Result> Get_Dashboard_Connect_Details()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_Dashboard_Connect_Details_Result>("Get_Dashboard_Connect_Details");
        }
    
        public virtual ObjectResult<Get_Dashboard_PrayerRequest_Details_Result> Get_Dashboard_PrayerRequest_Details()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_Dashboard_PrayerRequest_Details_Result>("Get_Dashboard_PrayerRequest_Details");
        }
    
        public virtual ObjectResult<Nullable<decimal>> Get_Dashboard_Total_Donation_Details()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("Get_Dashboard_Total_Donation_Details");
        }
    
        public virtual ObjectResult<Nullable<int>> Get_Dashboard_Total_Users_Details()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("Get_Dashboard_Total_Users_Details");
        }
    
        public virtual ObjectResult<Get_Dashboard_Upcoming_NewsEvents_Details_Result> Get_Dashboard_Upcoming_NewsEvents_Details()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_Dashboard_Upcoming_NewsEvents_Details_Result>("Get_Dashboard_Upcoming_NewsEvents_Details");
        }
    
        public virtual ObjectResult<GetAll_ScheduleCategory_Details_Result> GetAll_ScheduleCategory_Details(string serachword, Nullable<int> pageNo, Nullable<int> pageSize, string sortColumn, string sortOrder)
        {
            var serachwordParameter = serachword != null ?
                new ObjectParameter("serachword", serachword) :
                new ObjectParameter("serachword", typeof(string));
    
            var pageNoParameter = pageNo.HasValue ?
                new ObjectParameter("PageNo", pageNo) :
                new ObjectParameter("PageNo", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAll_ScheduleCategory_Details_Result>("GetAll_ScheduleCategory_Details", serachwordParameter, pageNoParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter);
        }
    
        public virtual ObjectResult<GetAll_tbl_Connect_Details_Result> GetAll_tbl_Connect_Details(string serachword, Nullable<int> pageNo, Nullable<int> pageSize, string sortColumn, string sortOrder)
        {
            var serachwordParameter = serachword != null ?
                new ObjectParameter("serachword", serachword) :
                new ObjectParameter("serachword", typeof(string));
    
            var pageNoParameter = pageNo.HasValue ?
                new ObjectParameter("PageNo", pageNo) :
                new ObjectParameter("PageNo", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAll_tbl_Connect_Details_Result>("GetAll_tbl_Connect_Details", serachwordParameter, pageNoParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter);
        }
    
        public virtual ObjectResult<GetAll_tbl_Donation_Details_Result> GetAll_tbl_Donation_Details(string serachword, Nullable<int> pageNo, Nullable<int> pageSize, string sortColumn, string sortOrder)
        {
            var serachwordParameter = serachword != null ?
                new ObjectParameter("serachword", serachword) :
                new ObjectParameter("serachword", typeof(string));
    
            var pageNoParameter = pageNo.HasValue ?
                new ObjectParameter("PageNo", pageNo) :
                new ObjectParameter("PageNo", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAll_tbl_Donation_Details_Result>("GetAll_tbl_Donation_Details", serachwordParameter, pageNoParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter);
        }
    
        public virtual ObjectResult<GetAll_tbl_DonationHistory_Details_Result> GetAll_tbl_DonationHistory_Details(string serachword, Nullable<int> pageNo, Nullable<int> pageSize, string sortColumn, string sortOrder)
        {
            var serachwordParameter = serachword != null ?
                new ObjectParameter("serachword", serachword) :
                new ObjectParameter("serachword", typeof(string));
    
            var pageNoParameter = pageNo.HasValue ?
                new ObjectParameter("PageNo", pageNo) :
                new ObjectParameter("PageNo", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAll_tbl_DonationHistory_Details_Result>("GetAll_tbl_DonationHistory_Details", serachwordParameter, pageNoParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter);
        }
    
        public virtual ObjectResult<GetAll_tbl_MessageNotification_Details_Result> GetAll_tbl_MessageNotification_Details()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAll_tbl_MessageNotification_Details_Result>("GetAll_tbl_MessageNotification_Details");
        }
    
        public virtual ObjectResult<GetAll_tbl_MinistryInterests_Details_Result> GetAll_tbl_MinistryInterests_Details(string serachword, Nullable<int> pageNo, Nullable<int> pageSize, string sortColumn, string sortOrder)
        {
            var serachwordParameter = serachword != null ?
                new ObjectParameter("serachword", serachword) :
                new ObjectParameter("serachword", typeof(string));
    
            var pageNoParameter = pageNo.HasValue ?
                new ObjectParameter("PageNo", pageNo) :
                new ObjectParameter("PageNo", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAll_tbl_MinistryInterests_Details_Result>("GetAll_tbl_MinistryInterests_Details", serachwordParameter, pageNoParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter);
        }
    
        public virtual ObjectResult<GetAll_tbl_NewsEvents_Details_Result> GetAll_tbl_NewsEvents_Details(string serachword, Nullable<int> pageNo, Nullable<int> pageSize, string sortColumn, string sortOrder)
        {
            var serachwordParameter = serachword != null ?
                new ObjectParameter("serachword", serachword) :
                new ObjectParameter("serachword", typeof(string));
    
            var pageNoParameter = pageNo.HasValue ?
                new ObjectParameter("PageNo", pageNo) :
                new ObjectParameter("PageNo", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAll_tbl_NewsEvents_Details_Result>("GetAll_tbl_NewsEvents_Details", serachwordParameter, pageNoParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter);
        }
    
        public virtual ObjectResult<GetAll_tbl_OtherNotification_Details_Result> GetAll_tbl_OtherNotification_Details()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAll_tbl_OtherNotification_Details_Result>("GetAll_tbl_OtherNotification_Details");
        }
    
        public virtual ObjectResult<GetAll_tbl_PrayerRequest_Details_Result> GetAll_tbl_PrayerRequest_Details(string serachword, Nullable<int> pageNo, Nullable<int> pageSize, string sortColumn, string sortOrder)
        {
            var serachwordParameter = serachword != null ?
                new ObjectParameter("serachword", serachword) :
                new ObjectParameter("serachword", typeof(string));
    
            var pageNoParameter = pageNo.HasValue ?
                new ObjectParameter("PageNo", pageNo) :
                new ObjectParameter("PageNo", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAll_tbl_PrayerRequest_Details_Result>("GetAll_tbl_PrayerRequest_Details", serachwordParameter, pageNoParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter);
        }
    
        public virtual ObjectResult<GetAll_tbl_Schedule_Details_Result> GetAll_tbl_Schedule_Details(string serachword, Nullable<int> pageNo, Nullable<int> pageSize, string sortColumn, string sortOrder)
        {
            var serachwordParameter = serachword != null ?
                new ObjectParameter("serachword", serachword) :
                new ObjectParameter("serachword", typeof(string));
    
            var pageNoParameter = pageNo.HasValue ?
                new ObjectParameter("PageNo", pageNo) :
                new ObjectParameter("PageNo", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAll_tbl_Schedule_Details_Result>("GetAll_tbl_Schedule_Details", serachwordParameter, pageNoParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter);
        }
    
        public virtual ObjectResult<GetAll_tbl_Subjects_MessagesOrderBY_Details_Result> GetAll_tbl_Subjects_MessagesOrderBY_Details(string serachword, Nullable<int> pageNo, Nullable<int> pageSize, string sortColumn, string sortOrder, Nullable<long> userID)
        {
            var serachwordParameter = serachword != null ?
                new ObjectParameter("serachword", serachword) :
                new ObjectParameter("serachword", typeof(string));
    
            var pageNoParameter = pageNo.HasValue ?
                new ObjectParameter("PageNo", pageNo) :
                new ObjectParameter("PageNo", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAll_tbl_Subjects_MessagesOrderBY_Details_Result>("GetAll_tbl_Subjects_MessagesOrderBY_Details", serachwordParameter, pageNoParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter, userIDParameter);
        }
    
        public virtual ObjectResult<GetAll_tbl_Users_Details_Result> GetAll_tbl_Users_Details(string serachword, Nullable<int> pageNo, Nullable<int> pageSize, string sortColumn, string sortOrder)
        {
            var serachwordParameter = serachword != null ?
                new ObjectParameter("serachword", serachword) :
                new ObjectParameter("serachword", typeof(string));
    
            var pageNoParameter = pageNo.HasValue ?
                new ObjectParameter("PageNo", pageNo) :
                new ObjectParameter("PageNo", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAll_tbl_Users_Details_Result>("GetAll_tbl_Users_Details", serachwordParameter, pageNoParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter);
        }
    
        public virtual int Update_tbl_MessageNotification(Nullable<long> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Update_tbl_MessageNotification", userIDParameter);
        }
    
        public virtual int Update_tbl_OtherNotification(Nullable<long> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Update_tbl_OtherNotification", userIDParameter);
        }
    
        public virtual ObjectResult<Get_EditDonationDetails_Result> Get_EditDonationDetails(Nullable<long> donationID)
        {
            var donationIDParameter = donationID.HasValue ?
                new ObjectParameter("DonationID", donationID) :
                new ObjectParameter("DonationID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_EditDonationDetails_Result>("Get_EditDonationDetails", donationIDParameter);
        }
    
        public virtual ObjectResult<Get_Assigned_Unassigned_tbl_MinistryInterests_Result> Get_Assigned_Unassigned_tbl_MinistryInterests(Nullable<long> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_Assigned_Unassigned_tbl_MinistryInterests_Result>("Get_Assigned_Unassigned_tbl_MinistryInterests", userIDParameter);
        }
    
        public virtual int Delete_Assigned_Unassigned_tbl_MinistryInterests(Nullable<long> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Delete_Assigned_Unassigned_tbl_MinistryInterests", userIDParameter);
        }
    
        public virtual ObjectResult<Get_DonationautoRecurrringeditgetlastpaydetails_Result> Get_DonationautoRecurrringeditgetlastpaydetails()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_DonationautoRecurrringeditgetlastpaydetails_Result>("Get_DonationautoRecurrringeditgetlastpaydetails");
        }
    }
}
