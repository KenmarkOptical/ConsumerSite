using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class CMS_Menu
    {
        public List<CollectionItem> Collections = new List<CollectionItem>();

        public CMS_Menu GetCollection()
        {
            CMS_Menu m = new CMS_Menu();
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                m.Collections = (from c in db.CMS_Like_Collection
                                 select new CollectionItem { item = c }
                                 ).ToList();
            }
            return m;
        }

        public CollectionItem EditCollection(CollectionItem item)
        {
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                var i = db.CMS_Like_Collection.Where(m => m.id == item.item.id).FirstOrDefault();
                i.like_id = item.item.like_id;
                i.icon_image = item.item.icon_image;
                i.icon_hover = item.item.icon_hover;
                i.about = item.item.about;
                i.about_image = item.item.about_image;
                db.SaveChanges();
            }

            return item;
        }

    } 
    
    public class CollectionItem{
      public  CMS_Like_Collection item {get;set;}
    }
}