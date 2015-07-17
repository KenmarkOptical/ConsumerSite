using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class WhereToBuy
    {
        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "A Zip Code is required")]
        public string Zip { get; set; }

        [Display(Name = "Range")]
        [Required(ErrorMessage = "A Radius is required")]
        public int Radius { get; set; }

        public List<usp_where_to_buy_Result> Customers{ get; set; }

        public WhereToBuy GetCustomers(WhereToBuy data, int Max_Results = 0)
        {
            WhereToBuy wtb = new WhereToBuy();
            wtb.Zip = data.Zip;
            wtb.Radius = data.Radius;

            wtb.Customers = new List<usp_where_to_buy_Result>();

            //prevent altering data
            data.Radius = data.Radius > 90 ? 90 : data.Radius;
            
            using(KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                var cust_List = db.usp_where_to_buy(data.Zip, data.Radius);
                wtb.Customers = cust_List.ToList();
                if (Max_Results != 0)
                {
                    wtb.Customers = wtb.Customers.Take(Max_Results).ToList();
                }
            }
            return wtb;
        }
    }
}