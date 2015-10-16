using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class CMS_PressRelease
    {
        public List<SinglePressRelease> Items { get; set; }

        public bool AddPressRelease(SinglePressRelease p)
        {
            { 
            
            }// db code here
            return true;
        }

        public void DeletePressRelease(int id)
        {
            { 
            
            }// db code here
        }

        public void EditPressRelease(int id)
        { 
        
        }// db code here

        public class SinglePressRelease
        {
            public CMS_PressRelease data { get; set; }
            public HttpPostedFileBase main_image { get; set; }
            public HttpPostedFileBase sub_image { get; set; }
        }
    }
}