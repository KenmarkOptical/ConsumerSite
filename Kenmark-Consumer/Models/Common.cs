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


namespace Kenmark_Consumer.Models
{
    public static class Common
    {
        public static string Environment
        {
            get { return "live"; }
        }

        

        [HttpGet, OutputCache(Duration = 7200, VaryByParam = "None")]
        public static List<string> GetQuickSearchFrames()
        {
            List<string> a = new List<string>();
            KenmarkTestDBEntities db = new KenmarkTestDBEntities();

            var result = db.inventories.Where(m => m.customerportal_display == true).Select(m => new { m.style_name, sku = m.sku.Substring(0, 4), m.coll_code }).Distinct().ToList();

            foreach (var frame in result)
            {
                a.Add(ToTitleCase(frame.style_name) + " (" + frame.sku.ToLower() + ") - " + frame.coll_code);
            }
            return a;
        }

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