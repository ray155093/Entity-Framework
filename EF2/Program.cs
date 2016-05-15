using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("App2");
            using (var db = new ContosoUniversityEntities())
            {
                db.Database.Log = Console.WriteLine;
                var c = db.Department.Find(1);

                c.Name += "??";
                Console.ReadLine();

                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    Console.WriteLine("rowVersion錯誤");
                    throw;
                }
            }
        }
    }
}
