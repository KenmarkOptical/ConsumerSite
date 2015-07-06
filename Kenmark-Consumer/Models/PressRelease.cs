using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class PressRelease
    {
        List<press_releases> Items { get; set; }

        public PressRelease GetItems()
        {
            PressRelease pr = new PressRelease();
            pr.Items = new List<press_releases>();

            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                pr.Items = db.press_releases.Where(m => m.active == true).ToList();
            }

            return pr;
        }
    }
}