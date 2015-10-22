using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class CollectionMain
    {
        public string main_image { get; set; }
        public string collection { get; set; }
        public string collection_name { get; set; }
        public string about { get; set; }
        public List<CollectionLink> Links = new List<CollectionLink>();

        public CollectionMain GetMain(string collection)
        {
            collection = collection.Replace("-", " ");
            CollectionMain c = new CollectionMain();
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                if (collection == "House Collections")
                {
                    c.collection = "House Collections";
                    c.collection_name = "House";
                    c.main_image = "about.jpg";
                    c.about = "About House Collections";

                    c.Links.Add(new CollectionLink() { code = "COM", sub = "RX" });
                    c.Links.Add(new CollectionLink() { code = "DES", sub = "RX" });
                    c.Links.Add(new CollectionLink() { code = "GAL", sub = "RX" });

                }
                else
                {
                    var lc = db.Kenmark_Collections_like.Where(m => m.Site_Display == collection).FirstOrDefault();
                    c.collection = lc.Group;
                    c.collection_name = lc.Site_Display;

                    var cm = db.CMS_Like_Collection.Where(m => m.like_id == lc.ID).FirstOrDefault();
                    c.main_image = cm.about_image;
                    c.about = cm.about;

                    c.Links = (from kc in db.Kenmark_Collections
                               where kc.Group == c.collection
                               select new CollectionLink { code = kc.Group, sub = kc.Site_Display }).ToList();
                }
               
            }
            return c;
        }

       

    }

    public class CollectionLink
    {
        public string code { get; set; }
        public string sub { get; set; }
    }
}