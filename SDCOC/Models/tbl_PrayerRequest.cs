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
    using System.Collections.Generic;
    
    public partial class tbl_PrayerRequest
    {
        public long PrayerRequestId { get; set; }
        public Nullable<long> UserId { get; set; }
        public string Name { get; set; }
        public string PrayerRequestText { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
