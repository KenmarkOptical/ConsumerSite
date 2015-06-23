﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class WhereToBuy
    {
        [Display(Name = "Zip Code:")]
        [Required(ErrorMessage = "A Zip Code is required")]
        public string Zip { get; set; }

        [Display(Name = "Radius:")]
        [Required(ErrorMessage = "A Radius is required")]
        public int Radius { get; set; }

        public List<usp_where_to_buy_Result> Customers{ get; set; }

        public WhereToBuy GetCustomers(WhereToBuy data)
        {
            WhereToBuy wtb = new WhereToBuy();
            wtb.Customers = new List<usp_where_to_buy_Result>();

            //prevent altering data
            data.Radius = data.Radius > 90 ? 90 : data.Radius;
            
            using(KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                var cust_List = db.usp_where_to_buy(data.Zip, data.Radius);
                wtb.Customers = cust_List.ToList();
            }
            return wtb;
        }
    }
}