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
    
    public partial class GallaryMaster
    {
        public long GallaryMasterID { get; set; }
        public string Image { get; set; }
        public long ClubServiceID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}