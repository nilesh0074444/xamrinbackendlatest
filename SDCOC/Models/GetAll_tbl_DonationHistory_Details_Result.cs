//------------------------------------------------------------------------------
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
    
    public partial class GetAll_tbl_DonationHistory_Details_Result
    {
        public Nullable<int> TotalCount { get; set; }
        public Nullable<long> ROWNUM { get; set; }
        public long DonationHistoryId { get; set; }
        public Nullable<long> DonationId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public Nullable<decimal> DonatedAmount { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string PaymentType { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string CVVCode { get; set; }
        public string ExpirationDate { get; set; }
        public Nullable<System.DateTime> DonationDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
