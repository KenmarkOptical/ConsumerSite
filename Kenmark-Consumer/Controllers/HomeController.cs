using Kenmark_Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kenmark_Consumer.Controllers
{
    public class HomeController : Controller
    {

        //mockup links:
        //https://projects.invisionapp.com/share/5C1PZSOYW#/screens/49215558?maintainScrollPosition=false
        //
        // GET: /Home/

        public ActionResult Index()
        {
            Home h = new Home().GetData();

            return View(h);// return h when ready
        }

        [Authorize]
        public ActionResult P()
        {
            Persons p = new Persons();
            p = p.GetPeople();
            
            
            return View("Person", p);
        }

        public ActionResult AddPerson(Persons p)
        {
            p.SavePeople(p);
            p.NewName = "";
            
            return View("Person", p);
        }

        public ActionResult GetBrandsData() {
            ShopMenu s = new ShopMenu().GetItems();
            return PartialView("/Views/Shared/_ShopMenu.cshtml", s);
        }

        public ActionResult GetProfileData() {
            return PartialView("/Views/Shared/_ProfileMenu.cshtml");
        }

        public ActionResult ParseOrderPad(string QuickSearchTB)
        {
            if (!QuickSearchTB.Contains("-"))
            {
                return null;
            }
            string frame = QuickSearchTB.Substring(0, QuickSearchTB.IndexOf("-")).Replace("-", "").Trim();
            string coll = QuickSearchTB.Substring(QuickSearchTB.IndexOf("-"), QuickSearchTB.Length - QuickSearchTB.IndexOf("-")).Replace("-","").Trim();
            string sku = Common.GetSku(frame, coll);

            return RedirectToAction("Index", "Style", new { @sku = sku });
        }
    }
}
