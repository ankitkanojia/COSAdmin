using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COSAdmin.Models.ViewModels
{
    public class ViewGallaryMasterVM
    {
        public string ServiceName { get; set; }
        public string Image { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public long GallaryMasterID { get; set; }
    }

    public class ViewClubServiceVM
    {
        public string CategoryType { get; set; }
        public string ServiceName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsPaid { get; set; }
        public long ClubServiceID { get; set; }
    }
}