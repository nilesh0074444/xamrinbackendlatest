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
    
    public partial class Get_Assigned_Unassigned_tbl_MinistryInterests_Result
    {
        public long MinistryId { get; set; }
        public string MinistryTitle { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<long> Assign_MinistryId { get; set; }
        public Nullable<long> FK_User_ID { get; set; }
        public Nullable<long> FK_MinistryId { get; set; }
    }
}
