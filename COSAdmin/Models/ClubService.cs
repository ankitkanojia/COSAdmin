//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace COSAdmin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClubService
    {
        public long ClubServiceID { get; set; }
        public string ServiceName { get; set; }
        public long ServiceCategoryID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsPaid { get; set; }
        public Nullable<long> ClubMasterID { get; set; }
    }
}