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
    }
}
