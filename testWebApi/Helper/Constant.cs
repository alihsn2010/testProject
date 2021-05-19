using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace testWebApi.Helper
{
    public class Constant
    {
       
        
    
    }
    public enum ErrorCode
    {
        No_Access = 701,
        Hash_Miss_Matched = 702,
        Controller_Exception = 703,
        Exception = 704,
        Merchant_ID_Error = 705
    };

    //public enum Source
    //{
    //    Mobile ,
    //    Web ,
    //    Portals ,
    //    Others 
    //}



    public enum ObjectResponce
    {
        Accepted = 1,
        Rejected = 2,
        Invalid_Operation = 3,
        Exception = 704
    }
    public class Source
    {
        public const string Mobile = "Mobile";
        public const string Web = "Web";
        public const string Portals = "Portals";
        public const string Others = "Others";
       
    }

    public static class ReponseCode
    {
        public const string Success = "00";
        public const string InvalidOperation = "-1";
        public const string InvalidIBAN = "-2";
        public const string SystemError = "-3";

    }

}