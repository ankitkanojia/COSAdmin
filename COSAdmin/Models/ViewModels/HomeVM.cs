using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COSAdmin.Models.ViewModels
{
    public class PayuResponse
    {
        public string firstName { get; set; }
        public string lastname { get; set; }
        public string amount { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public long tblPKID { get; set; }
        public string mihpayid { get; set; }
        public string mode { get; set; }
        public string status { get; set; }
        public string txnid { get; set; }
        public string Error { get; set; }
        public string key { get; set; }
        public string discount { get; set; }
        public string offer { get; set; }
        public string productinfo { get; set; }
        public string country { get; set; }
        public string udf1 { get; set; }
        public string udf2 { get; set; }
        public string udf3 { get; set; }
        public string udf4 { get; set; }
        public string udf5 { get; set; }
        public string Hash { get; set; }
        public string Errorbankcode { get; set; }
        public string PG_TYPE { get; set; }
        public string bank_ref_num { get; set; }
        public string shipping_firstname { get; set; }
        public string shipping_lastname { get; set; }
        public string shipping_address1 { get; set; }
        public string shipping_address2 { get; set; }
        public string shipping_city { get; set; }
        public string shipping_state { get; set; }
        public string shipping_country { get; set; }
        public string shipping_zipcode { get; set; }
        public string shipping_phone { get; set; }
        public string unmappedstatus { get; set; }
        public bool IsSuccess { get; set; }

    }

    public class RegistrationVM
    {
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long CityMasterID { get; set; }
        public string Address { get; set; }
        public long ClubMasterID { get; set; }
    }
    public class LoginVM
    {
        public string Mobile { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

    }

    public class ResetPWDGetVM
    {
        public string UniqueCode { get; set; }

        public string Mobile { get; set; }

    }

    public class ResetPWDVM
    {
        public string UniqueCode { get; set; }

        public string OTP { get; set; }

        public string Password { get; set; }

        public string Mobile { get; set; }

    }
}