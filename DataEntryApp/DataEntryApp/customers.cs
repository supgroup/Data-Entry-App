//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataEntryApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class customers
    {
        public long custId { get; set; }
        public string custname { get; set; }
        public string lastName { get; set; }
        public string mobile { get; set; }
        public string department { get; set; }
        public string barcode { get; set; }
        public Nullable<System.DateTime> printDate { get; set; }
        public string image { get; set; }
        public string notes { get; set; }
        public Nullable<long> createUserId { get; set; }
        public Nullable<long> updateUserId { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<long> nationalityId { get; set; }
    
        public virtual nationalities nationalities { get; set; }
        public virtual users users { get; set; }
        public virtual users users1 { get; set; }
    }
}
