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
}