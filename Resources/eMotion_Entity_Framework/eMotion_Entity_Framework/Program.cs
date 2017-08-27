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
                if (db.User.Any(o => o.firstName == "Giel") || db.User.Any(o => o.firstName == "Maarten"))
                {
                    Console.WriteLine("Giel of Maarten bestaat al");
                    
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                    return;
                }

                db.User.Add(new User
                {
                    firstName = "Giel",
                    lastName = "Reynders",
                    password = "456456"
                });


                db.SaveChanges();


                db.User.Add(new User
                {
                    firstName = "Maarten",
                    lastName = "Hermans",
                    password = "456456"
                });

                db.SaveChanges();

                foreach (var user in db.User)
                {
                    Console.WriteLine(user.firstName);
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
