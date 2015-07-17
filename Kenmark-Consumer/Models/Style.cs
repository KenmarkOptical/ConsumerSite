using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class Style
    {
        public string Style_Name { get; set; }
        public string Collection { get; set; }
        public List<String> Colors { get; set; }
        public string Main_Color { get; set; }

        //specs
        public string Material { get; set; }
        public string Temples { get; set; }
        public string Bridge { get; set; }

        //measurement
        public List<Style_Measurements> Measurements { get; set; }

        //where to buy 
        public WhereToBuy customers { get; set; }

        public Style GetStyle(string sku)
        {
            Style s = new Style();
            s.Colors = new List<string>();
            s.Measurements = new List<Style_Measurements>();
            s.customers = new WhereToBuy();
            KenmarkTestDBEntities db = new KenmarkTestDBEntities();
            InquiryEntities db2 = new InquiryEntities();

            var data1 = db.inventories.Where(m => m.sku.Substring(0,4) == sku).ToList(); 
            string main_sku = data1[0].sku.Substring(0,4);
            var data2 = db2.INQInventories.Where(i => i.Item.Trim().Substring(0, 4) == main_sku).ToList();

            //set the data
            s.Style_Name = data1.Select(m => m.style_name).FirstOrDefault();
            s.Collection = data1.Select(m => m.coll_code).FirstOrDefault();
            s.Main_Color = data1.Select(m=> m.sku.Substring(0,4)).FirstOrDefault() + ".jpg";
            s.Material = data1.Select(m => m.material).FirstOrDefault();
            s.Material = s.Material == "M" ? "Metal" : "Plastic";
            s.Temples = data2.Select(m => m.AS).FirstOrDefault();
            s.Bridge = data2.Select(m => m.AG).FirstOrDefault();

            s.Colors = data1.Select(m => m.sku.Substring(0, 6) + ".jpg").Distinct().ToList();
            s.Measurements = (from d in data2
                              select new Style_Measurements()
                              {
                                  A = d.P,
                                  B = d.Q,
                                  ED = d.R,
                                  Circ = d.W,
                                  Eye = d.O,
                                  Color = d.J,
                                  Temples = d.T
                              }).OrderBy(x => x.Eye)
                              .ToList();

            db.Database.Connection.Close();
            db2.Database.Connection.Close();

            return s;
        }
    }

    public class Style_Measurements
    {
        public int? Eye { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string ED { get; set; }
        public string Temples { get; set; }
        public string Circ { get; set; }
        public string Color { get; set; }
    }
}