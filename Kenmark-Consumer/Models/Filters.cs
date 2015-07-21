﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    [Serializable]
    public class Filters
    {
        public string coll { get; set; }
        public string group { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public int sort { get; set; }
        public int CurrPage { get; set; }
        public bool Reload { get; set; }


        //Filters
        public List<BoolSetting> Collection_Like { get; set; }
        public List<BoolSetting> Colors { get; set; }
        public List<BoolSetting> Genders { get; set; }
        public List<BoolSetting> Material { get; set; }
        public List<BoolSetting> Shape { get; set; }
        public int SelectedMinEyeSize { get; set; }
        public int SelectedMaxEyeSize { get; set; }
        public int MinEyeSize { get; set; }
        public int MaxEyeSize { get; set; }
        public List<BoolSetting> CloseOut { get; set; }
        public int Qty { get; set; }

        public class BoolSetting
        {
            public string DisplayName { get; set; }
            public bool Value { get; set; }
            public bool Disabled { get; set; }
        }

        public Filters GetFilters(string Group, string SubGroup)
        {
            KenmarkTestDBEntities db = new KenmarkTestDBEntities();
            List<BoolSetting> CollectionFilter = new List<BoolSetting>();
            List<BoolSetting> ColorFilter = new List<BoolSetting>();
            List<BoolSetting> GenderFilter = new List<BoolSetting>();
            List<BoolSetting> MaterialFilter = new List<BoolSetting>();
            List<BoolSetting> ShapeFilter = new List<BoolSetting>();
            List<BoolSetting> CloseOut = new List<BoolSetting>();
            List<int?> EyeSize = new List<int?>();
            int MinEye = 0;
            int MaxEye = 0;

            var codes = (from s in db.Kenmark_Collections_Sub
                         join c in db.Kenmark_Collections on new { g = s.Group, s = s.Sub_Group } equals new { g = c.Group, s = c.Sub_Group }
                         where s.Group == Group
                         && c.Site_Display == SubGroup

                         && s.Enabled == true
                         && c.Enabled == true
                         select s.Code).ToList();

            //set filters
            using (InquiryEntities di = new InquiryEntities())
            {
                var skuList = (from i in db.inventories
                               where codes.Contains(i.coll_sub)
                               && i.customerportal_display == true
                               select i.sku).ToList();

                CollectionFilter = db.Kenmark_Collections_like
                                .Where(m => m.Enabled == true)
                                .Select(m => new BoolSetting { DisplayName = m.Site_Display, Value = false })
                                .Distinct()
                                .OrderBy(m => m.DisplayName)
                                .ToList();
                
                ColorFilter = di.INQInventories
                                .Where(m => skuList.Contains(m.Item) && m.J != null)
                                .Select(m => new BoolSetting { DisplayName = m.J, Value = false })
                                .Distinct()
                                .OrderBy(m => m.DisplayName)
                                .ToList();

                GenderFilter = di.INQInventories
                                .Where(m => skuList.Contains(m.Item) && m.X != null)
                                .Select(m => new BoolSetting { DisplayName = m.X, Value = false })
                                .Distinct()
                                .OrderBy(m => m.DisplayName)
                                .ToList();

                MaterialFilter = di.INQInventories
                                .Where(m => skuList.Contains(m.Item) && m.Z != null)
                                .Select(m => new BoolSetting { DisplayName = m.Z, Value = false })
                                .Distinct()
                                .OrderBy(m => m.DisplayName)
                                .ToList();

                ShapeFilter = di.INQInventories
                                .Where(m => skuList.Contains(m.Item) && m.AU != null)
                                .Select(m => new BoolSetting { DisplayName = m.AU, Value = false })
                                .Distinct()
                                .OrderBy(m => m.DisplayName)
                                .ToList();

                EyeSize = di.INQInventories
                                .Where(m => skuList.Contains(m.Item) && m.O != null)
                                .Select(m => m.O)
                                .Distinct()
                                .ToList();
            }

            //set the min and max eye
            if (EyeSize.Count > 0)
            {
                MinEye = EyeSize.Where(m => m > 10).Min(m => Convert.ToInt16(m));
                MaxEye = EyeSize.Where(m => m > 10).Max(m => Convert.ToInt16(m));
            }
            //set the closeout filter                     
            var cf = (from o in db.ob_inv
                      where codes.Contains(o.coll_code)
                      select o.sku).ToList();

            var type = db.Kenmark_Collections_like.Where(m => m.Group == Group).FirstOrDefault().Type;


            return new Filters()
            {
                Collection_Like = CollectionFilter,
                Colors = ColorFilter,
                Genders = GenderFilter,
                Material = MaterialFilter,
                Shape = ShapeFilter,
                coll = group,
                group = SubGroup,
                MinEyeSize = MinEye,
                MaxEyeSize = MaxEye,
                SelectedMinEyeSize = MinEye,
                SelectedMaxEyeSize = MaxEye,
                CloseOut = CloseOut
            };
        }
    }
}