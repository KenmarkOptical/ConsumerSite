using Kenmark_Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Kenmark_Consumer.Controllers
{
    public class CollectionController : MyBaseController
    {
        //
        // GET: /Collection/

        public ActionResult Index(string id)
        {
            return View();
        }
         

        public ActionResult ViewCollection(string collection, int page, int sort = 1,string sub = "", Filters filter = null, string Type = "")
        {         

            //get the filter from session
            if (filter == null)
            {
                 filter = new Filters();
            }
            else
            {
                filter.sort = sort;
            }


            Collections c = new Collections();          
            //collection = "PEO";
            //sub = "RX";

            if (!String.IsNullOrEmpty(Type))
            {
                collection = "ALL";
                sub = "ALL";               
            }
         

            int totalCount = c.GetFramesCount(collection, sub, Type);
           

            int pageSize = c.PageCount;

            int count = page * c.PageCount;
            if (count < totalCount)
            {
                if (pageSize > (totalCount - count))
                {
                    //we are at the end, so we do not need a full page
                    c = c.GetFrames(collection, sub, page, (totalCount - count), sort, filter, Type);
                }
                else
                {//return full page                
                    c = c.GetFrames(collection, sub, page, pageSize, sort, filter, Type);
                    if (page == 0 && c.Frames.Count == 0)
                    {
                        ViewBag.NoResults = "1";
                    }
                }
            }
            else
            {
                c.Frames = new List<Collections.Frame>();
                ViewBag.NoResults = "1";
            }

         
            c.Page = page;
            c.CollectionCode = collection;
            c.CollectionGroup = sub;
            c.Type = Type;
            
            ViewBag.Sort = sort;
            ViewBag.Filter = new JavaScriptSerializer().Serialize(filter);

            return View(c);
        }

        [HttpGet]
        public ActionResult GetFrames(Filters fm, string coll, string group, int count, int sort, int page, string Type = "")
        {
                Collections c = new Collections();
                int pageSize = c.PageCount;      

                int totalCount = c.GetFramesCount(coll, group);

                if (count < totalCount)
                {
                    if (pageSize > (totalCount - count))
                    {
                        //we are at the end, so we do not need a full page
                        c = c.GetFrames(coll, group, page, (totalCount - count), sort, fm, Type);
                    }
                    else
                    {//return full page                
                        c = c.GetFrames(coll, group, page, pageSize, sort, fm, Type);
                        if (page == 0 && c.Frames.Count == 0)
                        {
                            ViewBag.NoResults = "1";
                        }
                    }
                }
                else
                {
                    return null;
                }
                return Json(new { html = RenderPartialViewToString("_Grid", c) }, JsonRequestBehavior.AllowGet);
            

            return null;
        }

        public ActionResult GetFilters(string coll, string group, int sort, string f, string s_type)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Filters filter = (Filters)js.Deserialize(f, typeof(Filters));

            //get the filter from session
            if (filter.Colors == null)
            {
                filter = new Filters().GetFilters(coll, group, s_type);
                filter.sort = sort;
            }

            filter.Type = s_type;

            return PartialView("_Filter", filter);
        }

        public ActionResult FilterChange(Filters f)
        {           
            Collections c = new Collections();
            int pageSize = c.PageCount;
            
            c = c.GetFrames(f.coll, f.group,f.CurrPage, pageSize, f.sort, f, f.Type);

                   
            c.Page = 1;
            c.CollectionCode = f.coll;
            c.CollectionGroup = f.group;
            c.Type = f.Type;                
            

            //create a query string from the filter model
            string query = QueryStringExtensions.ToQueryString<Filters>(f);

            string grid_html = RenderPartialViewToString("_Grid", c);
            string filter_html = RenderPartialViewToString("_Filter", f);
            ViewBag.Sort = f.sort;
            return Json(new { html = grid_html, filterChange = f.Reload == true ? 1 : 0, htmlfilter = filter_html, count = c.Frames.Count(), query = query });
        }

    }
}

