using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentUploader.Helper
{
    public class HelperMethod
    {
        public static string GetConnectionString()
        {
            string whichDbToSelect = System.Configuration.ConfigurationManager.AppSettings["database"];
            return System.Configuration.ConfigurationManager.ConnectionStrings[whichDbToSelect].ToString();
        }

        public static DateTime GetCurrentBDTime()
        {
            DateTime bdtime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Bangladesh Standard Time"); // bangladesh time
            return bdtime;
        }
    }
}