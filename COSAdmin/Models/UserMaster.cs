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
    
    public partial class UserMaster
    {
        public long UserMasterID { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public System.DateTime DOB { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string Profile { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public long RoleID { get; set; }
        public bool IsPaid { get; set; }
    }
}
