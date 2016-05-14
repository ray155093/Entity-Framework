using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF
{
    class Program
    {
        static void Main(string[] args)
        {

            string a = null;
            string test = a ?? "test";

            /*
            int? b = a.Length;
            //C# 6.0 新功能
            //int? b = a?.Length;
            */

            using (var db = new ContosoUniversityEntities())
            {
                foreach (var item in db.Course)
                {
                    Console.WriteLine(item.Title);
 
                }
            }
            Console.WriteLine("Hello");
        }

    }
}
