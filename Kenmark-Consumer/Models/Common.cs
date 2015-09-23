using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Text;
using System.Web.Mvc;
using System.IO;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.Script.Serialization;


namespace Kenmark_Consumer.Models
{
    public static class Common
    {
        public static string Environment
        {
            get { return "test"; }
        }


        public static List<SelectListItem> Collections
        {
            get{
                KenmarkTestDBEntities db = new KenmarkTestDBEntities();
                return db.Kenmark_Collections_like
                    .Where(m => m.Enabled == true)
                    .Select(m => new SelectListItem() { Text = m.Site_Display, Value = m.ID.ToString() })
                    .ToList();                
            }
        }

        [HttpGet, OutputCache(Duration = 7200, VaryByParam = "None")]
        public static List<string> GetQuickSearchFrames()
        {
            List<string> a = new List<string>();
            KenmarkTestDBEntities db = new KenmarkTestDBEntities();

            var result = db.inventories.Where(m => m.consumerportal_display == true).Select(m => new { m.style_name, sku = m.sku.Substring(0, 4), m.coll_code }).Distinct().ToList();

            foreach (var frame in result)
            {
                a.Add(ToTitleCase(frame.style_name) + " - " + frame.coll_code);
            }
            return a;
        }


        public static string GetSku(string style, string collection)
        {
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                var result = db.inventories.Where(m => m.style_name == style && m.coll_code == collection).Select(m => m.sku).FirstOrDefault();
                if (result != null)
                {
                    result = result.Substring(0, 4);
                }
                return result;
            }
        }


       public static List<SelectListItem> States = new List<SelectListItem>()
    {
        new SelectListItem() {Text="Alabama", Value="AL"},
        new SelectListItem() { Text="Alaska", Value="AK"},
        new SelectListItem() { Text="Arizona", Value="AZ"},
        new SelectListItem() { Text="Arkansas", Value="AR"},
        new SelectListItem() { Text="California", Value="CA"},
        new SelectListItem() { Text="Colorado", Value="CO"},
        new SelectListItem() { Text="Connecticut", Value="CT"},
        new SelectListItem() { Text="District of Columbia", Value="DC"},
        new SelectListItem() { Text="Delaware", Value="DE"},
        new SelectListItem() { Text="Florida", Value="FL"},
        new SelectListItem() { Text="Georgia", Value="GA"},
        new SelectListItem() { Text="Hawaii", Value="HI"},
        new SelectListItem() { Text="Idaho", Value="ID"},
        new SelectListItem() { Text="Illinois", Value="IL"},
        new SelectListItem() { Text="Indiana", Value="IN"},
        new SelectListItem() { Text="Iowa", Value="IA"},
        new SelectListItem() { Text="Kansas", Value="KS"},
        new SelectListItem() { Text="Kentucky", Value="KY"},
        new SelectListItem() { Text="Louisiana", Value="LA"},
        new SelectListItem() { Text="Maine", Value="ME"},
        new SelectListItem() { Text="Maryland", Value="MD"},
        new SelectListItem() { Text="Massachusetts", Value="MA"},
        new SelectListItem() { Text="Michigan", Value="MI"},
        new SelectListItem() { Text="Minnesota", Value="MN"},
        new SelectListItem() { Text="Mississippi", Value="MS"},
        new SelectListItem() { Text="Missouri", Value="MO"},
        new SelectListItem() { Text="Montana", Value="MT"},
        new SelectListItem() { Text="Nebraska", Value="NE"},
        new SelectListItem() { Text="Nevada", Value="NV"},
        new SelectListItem() { Text="New Hampshire", Value="NH"},
        new SelectListItem() { Text="New Jersey", Value="NJ"},
        new SelectListItem() { Text="New Mexico", Value="NM"},
        new SelectListItem() { Text="New York", Value="NY"},
        new SelectListItem() { Text="North Carolina", Value="NC"},
        new SelectListItem() { Text="North Dakota", Value="ND"},
        new SelectListItem() { Text="Ohio", Value="OH"},
        new SelectListItem() { Text="Oklahoma", Value="OK"},
        new SelectListItem() { Text="Oregon", Value="OR"},
        new SelectListItem() { Text="Pennsylvania", Value="PA"},
        new SelectListItem() { Text="Rhode Island", Value="RI"},
        new SelectListItem() { Text="South Carolina", Value="SC"},
        new SelectListItem() { Text="South Dakota", Value="SD"},
        new SelectListItem() { Text="Tennessee", Value="TN"},
        new SelectListItem() { Text="Texas", Value="TX"},
        new SelectListItem() { Text="Utah", Value="UT"},
        new SelectListItem() { Text="Vermont", Value="VT"},
        new SelectListItem() { Text="Virginia", Value="VA"},
        new SelectListItem() { Text="Washington", Value="WA"},
        new SelectListItem() { Text="West Virginia", Value="WV"},
        new SelectListItem() { Text="Wisconsin", Value="WI"},
        new SelectListItem() { Text="Wyoming", Value="WY"}
    };
        


        public static string ToTitleCase(this string aString)
        {
            try
            {
                return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(aString.ToLower());
            }
            catch
            {
                return aString;
            }
        }


        public static string ToMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        }

        public static string ToShortMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dateTime.Month);
        }

        public static int[] IndexOfAll(String oString, String sString)
        {
            //List of Occurances
            List<int> sOccurances = new List<int>();
            int pos = 0;
            for (int i = 0; i < oString.Length; i++)
            {
                pos = oString.IndexOf(sString, i);
                if (pos != -1)
                {
                    sOccurances.Add(pos);
                    i = pos;
                }
            }
            return (int[])sOccurances.ToArray();
        }


        public static string JavaScriptArray(this HtmlHelper htmlHelper, IList<string> values, string varName)
        {
            StringBuilder sb = new StringBuilder("var ");
            sb.Append(varName);
            sb.Append(" = [");
            for (int i = 0; i < values.Count; i++)
            {
                sb.Append("'");
                sb.Append(values[i].Replace("'", "\\'"));
                sb.Append("'");
                sb.Append(i == values.Count - 1 ? "" : ","); // Not the prettiest but it works...
            }
            sb.Append("];");
            string result = sb.ToString();
            return result;
        }

        public static string ToJson(this Object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }

        public static string JavaScriptArrayString(IList<string> values)
        {
            bool allNumeric = true;

            //check the list to see if it is all numeric
            for (int i = 0; i < values.Count; i++)
            {
                string s = values[i];
                Double a;
                if (!Double.TryParse(s, out a))
                {
                    allNumeric = false;
                }
            }


            StringBuilder sb = new StringBuilder();
            sb.Append(" [");
            for (int i = 0; i < values.Count; i++)
            {
                if (allNumeric)
                {
                    sb.Append(values[i]);
                    sb.Append(i == values.Count - 1 ? "" : ", "); // Not the prettiest but it works...
                }
                else
                {
                    sb.Append("'");
                    sb.Append(values[i]);
                    sb.Append("'");
                    sb.Append(i == values.Count - 1 ? "" : ", "); // Not the prettiest but it works...
                }
            }
            sb.Append("]");
            string result = sb.ToString();
            return result;
        }


        public static bool IsNumeric(string checkString)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            string strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            string strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

            if (checkString != null)
            {
                return !objNotNumberPattern.IsMatch(checkString) &&
                    !objTwoDotPattern.IsMatch(checkString) &&
                    !objTwoMinusPattern.IsMatch(checkString) &&
                    objNumberPattern.IsMatch(checkString);
            }
            else
            {
                return false;
            }
        }

       

    }
}