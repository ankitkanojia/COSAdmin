using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COSAdmin.Models.ViewModels
{
    public class UserPanelVM
    {

    }

    public class CluubServicesVM
    {
        public long CouchMasterID { get; set; }
        public decimal TraineeCharge { get; set; }
        public long CoachServiceID { get; set; }
        public long ServiceCategoryID { get; set; }
        public long ClubServiceID { get; set; }
        public long ClubMasterID { get; set; }
        public long UserID { get; set; }
    }
}