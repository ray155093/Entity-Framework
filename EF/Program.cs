using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF
{
    class Program
    {
        public class  DeptCouseCount
        {
            public Int32 DepartmentID {set;get;}
            public string Name {set;get;}
            public Int32 CourseCount {set;get;}
        }
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

                //Select(db);
                //Console.WriteLine("------");
                //SelectByLinq(db);
                //Console.WriteLine("------");
                //Insert(db);
                //Console.WriteLine("------");
                //InsertByFind(db);
                //Console.WriteLine("------");
                //Delete(db);
                //Console.WriteLine("------");
                //Select(db);


                //SelectMult(db);
                //Console.WriteLine("------");

                //SelectbyNativeSQL(db);
                //Console.WriteLine("------");

                SelectbyView(db);
                Console.WriteLine("------");
                // Select(db);


                Console.ReadLine();
            }


        }
        /// <summary>
        /// 基本查詢
        /// </summary>
        /// <param name="db"></param>
        static void Select(ContosoUniversityEntities db)
        {
            Console.WriteLine("基本查詢");
            //基本
            foreach (var item in db.Course)
            {
                Console.WriteLine(item.Title);
            }
        }
        /// <summary>
        /// 取出課程名稱中有Git的 用LINQ
        /// </summary>
        /// <param name="db"></param>
        static void SelectByLinq(ContosoUniversityEntities db)
        {

            Console.WriteLine("SelectByLinq");
            var data = from p in db.Course
                       where p.Title.Contains("Git")
                       select p;
            foreach (var item in data)
            {
                Console.WriteLine(item.Title);

            }
        }

        /// <summary>
        /// 新增欄位
        /// </summary>
        /// <param name="db"></param>
        public static void Insert(ContosoUniversityEntities db)
        {
            Console.WriteLine("Insert");
            //新增欄位
            db.Course.Add(new Course()
            {
                Title = "Git Test",
                Credits = 5,
                DepartmentID = 1
            });

            db.SaveChanges();

        }
        /// <summary>
        /// 新增欄位ByFind Find只能找主鍵
        /// </summary>
        /// <param name="db"></param>
        public static void InsertByFind(ContosoUniversityEntities db)
        {
            Console.WriteLine("InsertByFind");
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

        }

        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="db"></param>
        public static void Update(ContosoUniversityEntities db)
        {
            Console.WriteLine("Update");
            var c12 = db.Course.Find(12);

            c12.Credits += 1;
            db.SaveChanges();

        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="db"></param>
        public static void Delete(ContosoUniversityEntities db)
        {
            Console.WriteLine("Delete");
            ////刪除資料
            ///*
            // * 此句會刪除一筆最新的資料 但不是CourseID編號12 new時並沒有在new時被讀取 因為CourseID是PK會自動新增
            //db.Course.Remove(new Course() { CourseID = 12 });
            // * */

            //var c12 = db.Course.Find(12);

            //db.Course.Remove(c12);
            //db.SaveChanges();

            //刪除CourseID的資料
            var dataDel = from p in db.Course
                          where p.Title.Contains("Git Test")
                          select p;

            foreach (var item in dataDel)
            {
                db.Course.Remove(item);
            }
            db.SaveChanges();

        }

        /// <summary>
        /// 跨表輸出 延遲載入 導覽屬性
        /// </summary>
        /// <param name="db"></param>
        public static void SelectMult(ContosoUniversityEntities db)
        {

            //開關 是否啟動導覽屬性
            //這段程式可下中斷點看一下他的類別、開啟 sql profiler 觀察叫用方式
            // db.Configuration.ProxyCreationEnabled = false;
            var one = db.Course.Find(1);

            Console.WriteLine(one.Title + " \t" + one.Department.Name);

            foreach (var dept in db.Department)
            {
                Console.WriteLine(dept.Name);
                foreach (var course in dept.Course)
                {
                    Console.WriteLine(dept.Name + "\t" + course.Title);
                }
            }

        }

        /// <summary>
        /// 查詢fromSQL
        /// </summary>
        /// <param name="db"></param>
        public static void SelectbyNativeSQL(ContosoUniversityEntities db)
        {
            var sql = @"SELECT  Department.DepartmentID AS DepartmentID, Department.Name AS Name,count(*) CourseCount

FROM      Course INNER JOIN
                   Department ON Course.DepartmentID = Department.DepartmentID
				   group by Department.DepartmentID, Department.Name ";
            var data= db.Database.SqlQuery<DeptCouseCount>(sql);

            foreach (var item in data)
            {
                Console.WriteLine(item.Name+" \t"+item.CourseCount);
            }
        }
        /// <summary>
        /// 查詢ByView
        /// </summary>
        /// <param name="db"></param>
        public static void SelectbyView(ContosoUniversityEntities db)
        {

            var data = db.View_DeptsourceCount;

            foreach (var item in data)
            {
                Console.WriteLine(item.Name + " \t" + item.CourseCount);
            }
        }
    }
}
