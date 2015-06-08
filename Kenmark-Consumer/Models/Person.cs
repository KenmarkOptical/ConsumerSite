using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenmark_Consumer.Models
{
    public class Persons
    {
        public List<Person> People { get; set; }
        public string NewName { get; set; }

        public Persons GetPeople()
        {
            Persons p = new Persons();
            p.People = new List<Person>();

            using (KenmarkTestDBEntities db = new KenmarkTestDBEntities())
            {
                p.People = db.inventories.Select(f => new Person() { FirstName = f.sku, LastName = f.coll_code }).ToList();
            }

            //p.People.Add(new Person() { FirstName = "matt", LastName = "carden" });
            return p;
        }


        public void SavePeople(Persons p)
        {
            //p save person
        }
    }
    public class Person    {
        public string FirstName { get; set; }
        public string LastName { get; set; }        
    }
}