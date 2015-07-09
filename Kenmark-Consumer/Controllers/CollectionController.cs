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

        public ActionResult ViewCollection(string collection, string sub, int page, int sort = 1, Filters filter = null)
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
            collection = "PEO";
            sub = "RX";
          

            int totalCount = c.GetFramesCount(collection, sub);
           

            int pageSize = c.PageCount;

            int count = page * c.PageCount;
            if (count < totalCount)
            {
                if (pageSize > (totalCount - count))
                {
                    //we are at the end, so we do not need a full page
                    c = c.GetFrames(collection, sub, page, (totalCount - count), sort, filter);
                }
                else
                {//return full page                
                    c = c.GetFrames(collection, sub, page, pageSize, sort, filter);
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
            c.CollectionCode = "PEO";
            c.CollectionGroup = "RX";
            ViewBag.Sort = sort;
            ViewBag.Filter = new JavaScriptSerializer().Serialize(filter);

            return View(c);
        }

        [HttpGet]
        public ActionResult GetFrames(Filters fm, string coll, string group, int page, int count, int sort )
        {
                Collections c = new Collections();
                int pageSize = c.PageCount;      

                int totalCount = c.GetFramesCount(coll, group);

                if (count < totalCount)
                {
                    if (pageSize > (totalCount - count))
                    {
                        //we are at the end, so we do not need a full page
                        c = c.GetFrames(coll, group, page, (totalCount - count), sort, fm);
                    }
                    else
                    {//return full page                
                    c = c.GetFrames(coll, group, page, pageSize, sort, fm);
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

        public ActionResult GetFilters(string coll, string group, int sort, string f)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Filters filter = (Filters)js.Deserialize(f, typeof(Filters));

            //get the filter from session
            if (filter == null)
            {
                filter = new Filters().GetFilters(coll, group);
                filter.sort = sort;
            }
                        

            return PartialView("_Filter", filter);
        }

        public ActionResult FilterChange(Filters f)
        {           
            Collections c = new Collections();
            int pageSize = c.PageCount;
            
            c = c.GetFrames(f.coll, f.group,f.CurrPage, pageSize, f.sort, f);

            int totalCount = c.GetFramesCount(f.coll, f.group);
           
            c.Page = 1;
            c.CollectionCode = "PEO";
            c.CollectionGroup = "RX";

            //create a query string from the filter model
            string query = QueryStringExtensions.ToQueryString<Filters>(f);      
       
            ViewBag.Sort = f.sort;
            return Json(new { html = RenderPartialViewToString("_Grid", c), filterChange = f.Reload == true ? 1 : 0, htmlfilter = RenderPartialViewToString("_Filter", f), count = c.Frames.Count(), query = query });
        }

    }
}

