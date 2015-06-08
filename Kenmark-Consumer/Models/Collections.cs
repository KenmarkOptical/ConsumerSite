﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class Collections
    {
        //github 
        public string CollectionName { get; set; }
        public string CollectionCode { get; set; }
        public string CollectionType { get; set; }
        public string CollectionGroup { get; set; }
        public List<Frame> Frames { get; set; }
        public int FrameCount { get; set; }
        public List<CollectionImage> Images { get; set; }
        public int Page { get; set; }
        public int PageCount = 8;
        
       

        public class CollectonCode
        {
            public string Name { get; set; }
            public List<string> Code { get; set; }
        }

        public class CollectionImage
        {
            public string Image { get; set; }
            public string Frame { get; set; }
        }

        public class FilteredFrame
        {
            public string Sku { get; set; }
            public string Color { get; set; }
            public string Gender { get; set; }
            public string Material { get; set; }
            public string Shape { get; set; }
            public int? Eye { get; set; }
            public bool? Closeout { get; set; }

        }

        public class Frame
        {
            public string Style { get; set; }
            public string Image { get; set; }
            public string SKU { get; set; }
            public string ReleaseYear { get; set; }
            public string ReleaseMonth { get; set; }
            public string ReleaseDay { get; set; }
            public string ReleaseSort { get; set; }
            public bool? NewRelease { get; set; }
            public bool? Closeout { get; set; }
            public int? UnitsPurchased { get; set; }
            public decimal? NetSales { get; set; }
            public decimal? A1Price { get; set; }
            public decimal? A2Price { get; set; }
            public decimal? A3Price { get; set; }
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

        public class POP
        {
            public List<Collection_POP> Items { get; set; }
            public string isInternational { get; set; }
        }
        
     

        public Collections GetFrames(string Group, string SubGroup, int page, int pageSize, int sort, Filters Filter)
        {
            
            KenmarkTestDBEntities db = new KenmarkTestDBEntities();
            InquiryEntities di = new InquiryEntities();

            List<Frame> f = new List<Frame>();
            List<FilteredFrame> FilteredFrames = new List<FilteredFrame>();
            List<string> FilteredSKUS = new List<string>();

            bool ApplyFilter = false;

            var codes = (from s in db.Kenmark_Collections_Sub
                         join c in db.Kenmark_Collections on new { g = s.Group, s = s.Sub_Group } equals new { g = c.Group, s = c.Sub_Group }
                         where s.Group == Group
                         && c.Site_Display == SubGroup

                         && s.Enabled == true
                         && c.Enabled == true
                         select s.Code).ToList();


            if (Filter.Colors != null)
            {
                ApplyFilter = true;
                List<string> skuList = new List<string>();



                if (Filter.CloseOut != null && Filter.CloseOut.Where(m => m.Value == true && m.Disabled == false).Count() > 0)
                {
                    List<string> qtyList = new List<string>();
                    if (Filter.CloseOut.Where(m => m.Value == true).Select(m => m.DisplayName).First() == "Yes")
                    {
                        if (Filter.Qty > 0)
                        {
                            qtyList = (from i in db.ob_inv
                                       group i by i.sku.Substring(0, 4) into grps
                                       where grps.Sum(m => m.qty_avail) > Filter.Qty
                                        && codes.Contains(grps.Max(m => m.coll_code))
                                       select grps.Key).ToList();
                        }

                        skuList = (from i in db.inventories
                                   join o in db.ob_inv
                                        on i.sku equals o.sku into io
                                   from o in io.DefaultIfEmpty()
                                   where codes.Contains(i.coll_sub)
                                        && (qtyList.Count >= 0 && Filter.Qty > 0 ? qtyList.Contains(i.sku.Substring(0, 4)) : 1 == 1)
                                        && o.sku != null
                                        && i.customerportal_display == true
                                   select i.sku).ToList();

                        Filter.CloseOut.Where(m => m.DisplayName == "No").Select(c => { c.Disabled = true; return c; }).ToList();
                    }
                    else
                    {
                        skuList = (from i in db.inventories
                                   join o in db.ob_inv
                                        on i.sku equals o.sku into io
                                   from o in io.DefaultIfEmpty()
                                   where codes.Contains(i.coll_sub)
                                        && o.sku == null
                                        && i.customerportal_display == true
                                   select i.sku).ToList();

                        Filter.CloseOut.Where(m => m.DisplayName == "Yes").Select(c => { c.Disabled = true; return c; }).ToList();
                    }

                }
                else
                {
                    skuList = (from i in db.inventories
                               where codes.Contains(i.coll_sub)
                               && i.customerportal_display == true
                               select i.sku).ToList();
                }


                var ColorList = Filter.Colors.Where(m => m.Value == true).Select(m => m.DisplayName).ToList();
                var GenderList = Filter.Genders.Where(m => m.Value == true).Select(m => m.DisplayName).ToList();
                var MaterialList = Filter.Material.Where(m => m.Value == true).Select(m => m.DisplayName).ToList();
                var ShapeList = Filter.Shape.Where(m => m.Value == true).Select(m => m.DisplayName).ToList();
                var MinEye = Filter.SelectedMinEyeSize;
                var MaxEye = Filter.SelectedMaxEyeSize;


                FilteredFrames = (from i in di.INQInventories
                                  where skuList.Contains(i.Item)
                                    && (ColorList.Count > 0 ? ColorList.Contains(i.J) : 1 == 1)
                                    && (GenderList.Count > 0 ? GenderList.Contains(i.X) : 1 == 1)
                                    && (MaterialList.Count > 0 ? MaterialList.Contains(i.Z) : 1 == 1)
                                    && (ShapeList.Count > 0 ? ShapeList.Contains(i.AU) : 1 == 1)
                                    && (ShapeList.Count > 0 ? ShapeList.Contains(i.AU) : 1 == 1)
                                    && i.O >= MinEye
                                    && i.O <= MaxEye

                                  select new FilteredFrame
                                  {
                                      Sku = i.Item.Substring(0, 4),
                                      Color = i.J,
                                      Gender = i.X,
                                      Material = i.I,
                                      Shape = i.AU,
                                      Eye = i.O
                                  }).Distinct().ToList();

                FilteredSKUS = FilteredFrames.Select(m => m.Sku).Distinct().ToList();

                ////Remove the filters that no longer apply
                Filter.Colors.Where(m => !FilteredFrames.Select(x => x.Color).Contains(m.DisplayName)).Select(c => { c.Disabled = true; return c; }).ToList();
                Filter.Genders.Where(m => !FilteredFrames.Select(x => x.Gender).Contains(m.DisplayName)).Select(c => { c.Disabled = true; return c; }).ToList();
                Filter.Material.Where(m => !FilteredFrames.Select(x => x.Material).Contains(m.DisplayName)).Select(c => { c.Disabled = true; return c; }).ToList();
                Filter.Shape.Where(m => !FilteredFrames.Select(x => x.Shape).Contains(m.DisplayName)).Select(c => { c.Disabled = true; return c; }).ToList();
                if (FilteredFrames.Count > 0)
                {
                    Filter.SelectedMinEyeSize = FilteredFrames.Min(x => Convert.ToInt16(x.Eye));
                    Filter.SelectedMaxEyeSize = FilteredFrames.Max(x => Convert.ToInt16(x.Eye));
                }



            }



            int total_frames = 0;

            if (sort == 1)
            {
                f = (from i in db.inventories
                     join o in db.ob_inv
                       on i.sku equals o.sku into io
                     from o in io.DefaultIfEmpty()
                     where codes.Contains(i.coll_sub)
                        && i.customerportal_display == true
                        && (ApplyFilter ? FilteredSKUS.Contains(i.sku.Substring(0, 4)) : 1 == 1)
                     group i by new { style = i.style_name, image = i.sku.Substring(0, 4) + ".jpg", ob = o.sku, a1 = o.a_1_price, a2 = o.a_2_price, a3 = o.a_3_price } into g
                     select new Frame
                     {
                         Style = g.Key.style,
                         Image = g.Key.image,
                         Closeout = g.Key.ob == null ? false : true,
                         A1Price = g.Key.a1,
                         A2Price = g.Key.a2,
                         A3Price = g.Key.a3,
                         SKU = g.Key.image.Replace(".jpg", ""),
                         ReleaseYear = (from t2 in g select t2.sort_order).Max().Substring((from t2 in g select t2.sort_order).Max().Length - 4, 4),
                         ReleaseMonth = (from t2 in g select t2.sort_order).Max().Substring(0, 2),
                         ReleaseDay = (from t2 in g select t2.sort_order).Max().Substring(2, 2),
                         ReleaseSort = (from t2 in g select t2.sort_order).Max().Substring((from t2 in g select t2.sort_order).Max().Length - 4, 4) + (from t2 in g select t2.sort_order).Max().Substring(0, 2) + (from t2 in g select t2.sort_order).Max().Substring(2, 2)
                     }).Distinct()
                       .OrderByDescending(m => m.ReleaseSort)                          
                       .ToList();

                total_frames = f.GroupBy(m => m.Style).Count();
                f = f.Skip((page == 0 ? 0 : page - 1) * pageSize).Take(pageSize).ToList();
            }
            else if (sort == 2)
            {
                f = (from i in db.inventories
                     join o in db.ob_inv
                       on i.sku equals o.sku into io
                     from o in io.DefaultIfEmpty()
                     where codes.Contains(i.coll_sub)
                             && i.customerportal_display == true
                             && (ApplyFilter ? FilteredSKUS.Contains(i.sku.Substring(0, 4)) : 1 == 1)
                     group i by new { style = i.style_name, image = i.sku.Substring(0, 4) + ".jpg", ob = o.sku } into g
                     select new Frame
                     {
                         Style = g.Key.style,
                         Image = g.Key.image,
                         SKU = g.Key.image.Replace(".jpg", ""),
                         ReleaseYear = (from t2 in g select t2.sort_order).Max().Substring((from t2 in g select t2.sort_order).Max().Length - 4, 4),
                         ReleaseMonth = (from t2 in g select t2.sort_order).Max().Substring(0, 2),
                         ReleaseDay = (from t2 in g select t2.sort_order).Max().Substring(2, 2),
                     }).Distinct()
                       .OrderBy(m => m.Style)
                       .ToList();

                total_frames = f.GroupBy(m => m.Style).Count();
                f = f.Skip((page == 0 ? 0 : page - 1) * pageSize).Take(pageSize).ToList();
            }
            else if (sort == 3)
            {
                f = (from i in db.inventories
                     join o in db.ob_inv
                        on i.sku equals o.sku into io
                     from o in io.DefaultIfEmpty()
                     where codes.Contains(i.coll_sub)
                              && i.customerportal_display == true
                              && (ApplyFilter ? FilteredSKUS.Contains(i.sku.Substring(0, 4)) : 1 == 1)
                     group i by new { style = i.style_name, image = i.sku.Substring(0, 4) + ".jpg", ob = o.sku } into g
                     select new Frame
                     {
                         Style = g.Key.style,
                         Image = g.Key.image,
                         SKU = g.Key.image.Replace(".jpg", ""),
                         ReleaseYear = (from t2 in g select t2.sort_order).Max().Substring((from t2 in g select t2.sort_order).Max().Length - 4, 4),
                         ReleaseMonth = (from t2 in g select t2.sort_order).Max().Substring(0, 2),
                         ReleaseDay = (from t2 in g select t2.sort_order).Max().Substring(2, 2),
                     }).Distinct()
                       .OrderByDescending(m => m.Style)
                       .ToList();

                total_frames = f.GroupBy(m => m.Style).Count();
                f = f.Skip((page == 0 ? 0 : page - 1) * pageSize).Take(pageSize).ToList();
            }
         
 

            //set flag for new frames
            foreach (var item in f)
            {
                try
                {
                    string date = item.ReleaseMonth + "/" + item.ReleaseDay + "/" + item.ReleaseYear;
                    DateTime dt = Convert.ToDateTime(date);
                    DateTime newDt = DateTime.Now.AddMonths(-6);  //new frames are considered a rolling 6 months
                    if (dt > newDt)
                    {
                        item.NewRelease = true;
                    }
                }
                catch (Exception)
                {

                }
            }

            var ct = db.Kenmark_Collections_like.Where(m => m.Group == Group).Select(m => m.Type).FirstOrDefault();

            //get the sub colors of each frame
            List<FrameColor> fcList = db.inventory_info.Select(m => new FrameColor { Color = m.Color, LikeColor = m.Like_Color.Replace(" ", "_"), Style = m.style, Image = m.sku.Substring(0, 6), Sku = m.sku.Substring(0, 4) }).Distinct().ToList();
            foreach (var item in f)
            {
                item.Colors = fcList.Where(m => m.Sku == item.SKU.Substring(0, 4)).ToList();
            }


            return new Collections { Frames = f, CollectionType = ct, FrameCount = total_frames };
        }

        public int GetFramesCount(string Group, string SubGroup)
        {
            KenmarkTestDBEntities db = new KenmarkTestDBEntities();
            var codes = (from s in db.Kenmark_Collections_Sub
                         join c in db.Kenmark_Collections on new { g = s.Group, s = s.Sub_Group } equals new { g = c.Group, s = c.Sub_Group }
                         where s.Group == Group
                         && c.Site_Display == SubGroup

                         && s.Enabled == true
                         && c.Enabled == true
                         select s.Code).ToList();


            return db.inventories.Where(m => codes.Contains(m.coll_sub) && m.consumerportal_display == true).GroupBy(m => m.style_name).Count();
        }



    }
}