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
    
    public partial class CoachMaster
    {
        public long CouchMasterID { get; set; }
        public Nullable<decimal> TraineeCharge { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long CityMasterID { get; set; }
        public Nullable<long> ClubMasterID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDtae { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string CoachCertificate { get; set; }
        public Nullable<decimal> Experience { get; set; }
    }
}
