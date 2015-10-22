using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class Home
    {
        public List<NewReleaseFrame> NewFrames { get; set; }
        public List<CMS_Home_Carousel> Carousel { get; set; }

        public Home GetData()
        {
            Home h = new Home();
            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                h.NewFrames = (from i in db.inventories
                                        join o in db.ob_inv
                                        on i.sku equals o.sku into io
                                        join c in db.Kenmark_Collections_like on i.coll_group equals c.Group
                                        join oi in db.ob_inv on i.sku equals oi.sku into oii
                                            from oi in oii.DefaultIfEmpty()
                                      from o in io.DefaultIfEmpty()
                                      where i.customerportal_display == true
                                        && (c.Type == "D" || c.Type == "B")
                                        && oi.sku == null
                                      group i by new { style = i.style_name, image = i.sku.Substring(0, 4) + ".jpg", coll = i.coll_name } into g
                                      select new NewReleaseFrame
                                      {
                                          Style = g.Key.style,
                                          Image = g.Key.image,
                                          SKU = g.Key.image.Replace(".jpg", ""),
                                          Sort = (from t2 in g select t2.sort_order).Max().Substring((from t2 in g select t2.sort_order).Max().Length - 4, 4) + (from t2 in g select t2.sort_order).Max().Substring(0, 2) + (from t2 in g select t2.sort_order).Max().Substring(2, 2),
                                          Collection = g.Key.coll,
                                      })
                                      .Distinct()
                                      .OrderByDescending(m => m.Sort)
                                      .Take(20)
                                      .ToList();
              
                
                //get the sub colors of each frame
                List<FrameColor> fcList = db.inventory_info.Select(m => new FrameColor { Color = m.Color, LikeColor = m.Like_Color.Replace(" ", "_"), Style = m.style, Image = m.sku.Substring(0, 6), Sku = m.sku.Substring(0, 4) }).Distinct().ToList();
                foreach (var item in h.NewFrames)
                {
                    item.Colors = fcList.Where(m => m.Sku == item.SKU.Substring(0, 4)).ToList();
                }   
          
                //set up the carousel
                h.Carousel = db.CMS_Home_Carousel.Where(m => m.enabled == true).OrderBy(m => m.rank).ToList();
            }
            return h;
        }
    }

    public class NewReleaseFrame
    {
        public string Style { get; set; }
        public string SKU { get; set; }
        public string Image { get; set; }
        public string Collection { get; set; }
        public string Sort { get; set; }
        public string BestColor { get; set; }
        public List<FrameColor> Colors { get; set; }

    }

    public class FrameColor
    {
        public string Style { get; set; }
        public string Sku { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public string LikeColor { get; set; }
    }
}