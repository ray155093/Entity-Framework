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
                
                Console.WriteLine("\n");

                //取出課程名稱中有Git的 用LINQ
                var data = from p in db.Course
                           where p.Title.Contains("Git")
                           select p;
                foreach (var item in data)
                {
                    Console.WriteLine(item.Title);

                }
                Console.WriteLine("\n");

                //新增欄位
                db.Course.Add(new Course()
                {
                    Title="Git Test",
                    Credits=5,
                    DepartmentID=1
                });
                

                //新增欄位
                var c = new Course()
                {
                    Title = "Git Test 2",
                    Credits = 5,
                };
                c.Department = db.Department.Find(2);

                //更新前
                Console.WriteLine(c.DepartmentID);
               
                db.Course.Add(c);
                //更新後
                db.SaveChanges();
                Console.WriteLine(c.DepartmentID);

                Console.ReadLine();


                foreach (var item in data)
                {
                    Console.WriteLine(item.Title);

                }
           
            }
           

        }

    }
}
