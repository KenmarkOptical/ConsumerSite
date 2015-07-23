using MaxMind.GeoIP2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class MaxMindGeo
    {

        public MaxMind.GeoIP2.Responses.CityResponse  UserLocation()
        {
            string UserIP = GetIPAddress();
            var client = new WebServiceClient(102478, "c8Y3HbAbHgyx");
            var response = client.City(UserIP);            
            return response;
        }


        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            string ip = context.Request.ServerVariables["REMOTE_ADDR"];
            if (ip == "::1" ||ip.Contains("10.100"))
            {
                ip = "66.147.2.61";
            }
            return ip;
        }

        public string GetZipFromLatLong(string latitude, string longitude)
        {
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                string query = @"
                    SELECT zip, latitude, longitude, SQRT(
                        POWER(69.1 * (latitude - " + latitude + @"), 2) +
                        POWER(69.1 * (" + longitude + @" - longitude) * COS(latitude / 57.3), 2)) AS distance
                    FROM us_zipcodes
                    ORDER BY distance;";

               var result = db.Database.SqlQuery<GetZipFromLatLong_Result>(query).FirstOrDefault();

               return result.zip;
             }
            
        }

        private class GetZipFromLatLong_Result
        {
            public string zip { get; set; }
        }
    }
}