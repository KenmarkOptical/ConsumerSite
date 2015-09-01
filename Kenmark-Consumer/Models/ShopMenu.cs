using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class ShopMenu
    {
        public List<ShopItem> Items = new List<ShopItem>();

        public ShopMenu GetItems()
        {
            ShopMenu m = new ShopMenu();            
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                m.Items = (from c in db.CMS_Like_Collection
                           join l in db.Kenmark_Collections_like on c.like_id equals l.ID
                           select new ShopItem
                           {
                               id = c.id,
                               like_id = c.like_id,
                               icon_image = c.icon_image,
                               icon_hover = c.icon_hover,
                               about = c.about,
                               about_image = c.about_image,
                               collection = l.Collecton,
                               site_display = l.Site_Display,
                               @group = l.Group,
                               type = l.Type,
                               order = l.Order,
                               enabled = l.Enabled
                           }).ToList();
            }

            return m;
        }
    }

    public class ShopItem
    {
        public int id { get; set; }
        public int like_id { get; set; }
        public string icon_image { get; set; }
        public string icon_hover { get; set; }
        public string about { get; set; }
        public string about_image { get; set; }
        public string collection { get; set; }
        public string site_display { get; set; }
        public string group { get; set; }
        public string type { get; set; }
        public int? order { get; set; }
        public bool? enabled { get; set; }
    }
}