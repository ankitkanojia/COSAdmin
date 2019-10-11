using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COSAdmin.Models.ViewModels
{
    public class LoginVM
    {
        public string Mobile { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

    }

    public class ResetPWDVM
    {
        public string UniqueCode { get; set; }

        public string OTP { get; set; }

        public string Password { get; set; }

        public string Mobile { get; set; }

    }
}