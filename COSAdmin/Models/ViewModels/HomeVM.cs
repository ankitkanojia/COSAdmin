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
}