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

        
        public static string SaveImage(HttpPostedFileBase image, string directory)
        {           
            if (image != null && image.ContentLength > 0)
            {
                string extension = image.FileName.Substring(image.FileName.IndexOf('.'), image.FileName.Length - image.FileName.IndexOf('.'));
                var fileName = Path.GetFileName(System.IO.Path.GetRandomFileName());
                fileName = fileName.Substring(0, fileName.IndexOf('.')) + extension;
                var imagePath = (Path.Combine(directory, fileName));
                image.SaveAs(imagePath);
                var save_path = imagePath.Substring(imagePath.IndexOf("Content"), imagePath.Length - imagePath.IndexOf("Content"));
                return save_path;
            }
            return "";
        }

        public static List<string> CheckImage(HttpPostedFileBase image, string target_image_name, int width, int height)
        {
            //use 0 if it doesnt matter

            int min_width = (int)Math.Ceiling((decimal)width * (decimal).95);
            int min_height = (int)Math.Ceiling((decimal)height * (decimal).95);
            int max_width = (int)Math.Ceiling((decimal)width * (decimal)1.5);
            int max_height = (int)Math.Ceiling((decimal)height * (decimal)1.5);

            decimal ratio = (width == 0 || height == 0) ? 0 : Decimal.Divide(width, height);
            if(ratio != 0)
            {
                ratio = decimal.Round(ratio, 2, MidpointRounding.AwayFromZero);
            }
            List<string> FileTypes = new List<string>() { ".png", ".jpg", ".gif" };
            string extension = image.FileName.Substring(image.FileName.IndexOf('.'), image.FileName.Length - image.FileName.IndexOf('.'));

            List<string> Results = new List<string>();
            using (System.Drawing.Image i = System.Drawing.Image.FromStream(image.InputStream, true, true))
            {
                decimal img_ratio = (width == 0 || height == 0) ? 0 : Decimal.Divide(i.Width, i.Height);
                
                if ((width != 0 && i.Width < min_width) || (height != 0 && i.Height < min_height))
                {
                    Results.Add(target_image_name +" is " + i.Width + "x" + i.Height + " but must be at least " + min_width + "x" + min_height);
                }
                if ((width != 0 && i.Width > max_width) || (height != 0 && i.Height > max_height))
                {
                    Results.Add(target_image_name +" is " + i.Width + "x" + i.Height + " but must be no greater than " + max_width + "x" + max_height);
                }
                if ((width != 0 && height != 0) && (img_ratio < (ratio * (decimal).97) || img_ratio > (ratio * (decimal)1.03)))
                {
                    Results.Add(target_image_name +" ratio (width / height) should be " + ratio + " please resize the width and height to meet these requirements");
                }


                if (!FileTypes.Contains(extension))
                {
                    string e = target_image_name+ " must be one of the following formats: ";
                    foreach (var item in FileTypes)
                    {
                        e += item + "  ";
                    }
                    Results.Add(e);
                }
            }

            return Results;
        }

        /// <summary>
        /// Produces optional, URL-friendly version of a title, "like-this-one". 
        /// hand-tuned for speed, reflects performance refactoring contributed
        /// by John Gietzen (user otac0n) 
        /// </summary>
        public static string URLFriendly(string title)
        {
            if (title == null) return "";

            const int maxlen = 80;
            int len = title.Length;
            bool prevdash = false;
            var sb = new StringBuilder(len);
            char c;

            for (int i = 0; i < len; i++)
            {
                c = title[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lowercase
                    sb.Append((char)(c | 32));
                    prevdash = false;
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' ||
                    c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevdash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if ((int)c >= 128)
                {
                    int prevlen = sb.Length;
                    sb.Append(RemapInternationalCharToAscii(c));
                    if (prevlen != sb.Length) prevdash = false;
                }
                if (i == maxlen) break;
            }

            if (prevdash)
                return sb.ToString().Substring(0, sb.Length - 1);
            else
                return sb.ToString();
        }

        public static string RemapInternationalCharToAscii(char c)
        {
            string s = c.ToString().ToLowerInvariant();
            if ("àåáâäãåą".Contains(s))
            {
                return "a";
            }
            else if ("èéêëę".Contains(s))
            {
                return "e";
            }
            else if ("ìíîïı".Contains(s))
            {
                return "i";
            }
            else if ("òóôõöøőð".Contains(s))
            {
                return "o";
            }
            else if ("ùúûüŭů".Contains(s))
            {
                return "u";
            }
            else if ("çćčĉ".Contains(s))
            {
                return "c";
            }
            else if ("żźž".Contains(s))
            {
                return "z";
            }
            else if ("śşšŝ".Contains(s))
            {
                return "s";
            }
            else if ("ñń".Contains(s))
            {
                return "n";
            }
            else if ("ýÿ".Contains(s))
            {
                return "y";
            }
            else if ("ğĝ".Contains(s))
            {
                return "g";
            }
            else if (c == 'ř')
            {
                return "r";
            }
            else if (c == 'ł')
            {
                return "l";
            }
            else if (c == 'đ')
            {
                return "d";
            }
            else if (c == 'ß')
            {
                return "ss";
            }
            else if (c == 'Þ')
            {
                return "th";
            }
            else if (c == 'ĥ')
            {
                return "h";
            }
            else if (c == 'ĵ')
            {
                return "j";
            }
            else
            {
                return "";
            }
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