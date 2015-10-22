using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class PressRelease
    {
       public List<press_releases> Items { get; set; }

        public PressRelease GetItems()
        {
            PressRelease pr = new PressRelease();
            pr.Items = new List<press_releases>();

            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                pr.Items = db.press_releases.Where(m => m.active == true && m.release_date <= DateTime.Now).OrderByDescending(m => m.release_date).ToList();
            }

            return pr;
        }
    }
}