using eMotion.DomainClasses.Classes;
using eMotion.EF.Console.Display.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMotion_Entity_Framework
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new eMotionDBContext())
            {
                if (db.User.Any(o => o.FirstName == "Giel") || db.User.Any(o => o.FirstName == "Maarten"))
                {
                    Console.WriteLine("Giel of Maarten bestaat al");
                    
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                    return;
                }

                db.User.Add(new User
                {
                    FirstName = "Giel",
                    LastName = "Reynders",
                    Password = "456456"
                });


                db.SaveChanges();


                db.User.Add(new User
                {
                    FirstName = "Maarten",
                    LastName = "Hermans",
                    Password = "456456"
                });

                db.SaveChanges();

                foreach (var user in db.User)
                {
                    Console.WriteLine(user.FirstName);
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
