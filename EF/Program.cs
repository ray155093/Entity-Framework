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
                //基本
                foreach (var item in db.Course)
                {
                    Console.WriteLine(item.Title);
 
                }
                //取出課程名稱中有Git的
                Console.WriteLine("\n");
                var data = from p in db.Course
                           where p.Title.Contains("Git")
                           select p;
                foreach (var item in data)
                {
                    Console.WriteLine(item.Title);

                }

            }
           


            Console.WriteLine("Hello");
        }

    }
}
