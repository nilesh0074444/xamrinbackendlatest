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
    
    public partial class tbl_MessageSubject
    {
        public long MessageSubjectId { get; set; }
        public Nullable<long> SubjectId { get; set; }
        public Nullable<long> MessageFrom { get; set; }
        public Nullable<long> MessageTo { get; set; }
        public string MessageText { get; set; }
        public Nullable<System.DateTime> MessageSendDate { get; set; }
        public Nullable<bool> IsReaded { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
