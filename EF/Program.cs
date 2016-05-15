using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace EF
{
    class Program
    {
        public class DeptCouseCount
        {
            public Int32 DepartmentID { set; get; }
            public string Name { set; get; }
            public Int32 CourseCount { set; get; }
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

                //SelectbyView(db);
                //Console.WriteLine("------");

                //InsertByInit(db);
                //Console.WriteLine("------");

                //DbEntityEntryTest(db);
                //Console.WriteLine("------");

                //DbPropertyValues(db);
                //Console.WriteLine("------");
                離線模式(db);
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
            var data = db.Database.SqlQuery<DeptCouseCount>(sql);

            foreach (var item in data)
            {
                Console.WriteLine(item.Name + " \t" + item.CourseCount);
            }
        }
        /// <summary>
        /// 查詢ByView
        /// </summary>
        /// <param name="db"></param>
        public static void SelectbyView(ContosoUniversityEntities db)
        {

            var data = db.View_DeptsourceCount;

            if (data.Any())
            { }
            foreach (var item in data)
            {
                Console.WriteLine(item.Name + " \t" + item.CourseCount);
            }
        }
        /// <summary>
        /// 新增欄位 Credits使用預設值
        /// </summary>
        /// <param name="db"></param>
        public static void InsertByInit(ContosoUniversityEntities db)
        {
            Console.WriteLine("Insert");
            //新增欄位
            db.Course.Add(new Course()
            {
                Title = "Git Test",
                DepartmentID = 1
            });

            db.SaveChanges();

        }
        /// <summary>
        /// 狀態分析
        /// </summary>
        /// <param name="db"></param>
        public static void DbEntityEntryTest(ContosoUniversityEntities db)
        {
            //從物件找狀態
            //從狀態找物件
            //EF是用狀態來進行判斷資料有沒有被修改

            //AsNoTracking 取得資料為唯讀，可以讓資料只取得一份(沒有修改資料 ex:CurrentValues) 結果:讀取速度加快，使用記憶體減少，但資料為唯讀。
            //var c = db.Course.Find(1).AsNoTracking();

            //將產生的log輸出
            db.Database.Log = Console.WriteLine;

            var c = db.Course.Find(8);
            var cee = db.Entry(c);
            //cee.OriginalValues;
            //cee.CurrentValues;
            Console.WriteLine(c.Title + "\t" + db.Entry(c).State);
            c.Credits += 1;
            Console.WriteLine(c.Title + "\t" + db.Entry(c).State);
            db.Course.Remove(c);
            db.SaveChanges();
            db.Entry(c).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();


            //物件(Course)沒有狀態 狀態存在DB中
            // DbEntityEntry ce = db.Entry(db.Course);

        }
        /// <summary>
        /// 說明Dbser的屬性
        /// </summary>
        /// <param name="db"></param>
        public static void Create(ContosoUniversityEntities db)
        {
            //資料只唯讀 加快讀取速度 降低記憶體
            // var data =db.Course.AsNoTracking() ;

            //取出資料類行為Ccourse
            // data = db.Course.SqlQuery("SELECT * FROM ........");
        }

        /// <summary>
        /// 說明DbPropertyValues的屬性
        /// </summary>
        /// <param name="db"></param>
        public static void DbPropertyValues(ContosoUniversityEntities db)
        {
            var c = db.Course.ToList().LastOrDefault();

            c.Title = "Test 123";

            if (db.Entry(c).State == System.Data.Entity.EntityState.Modified)
            {
                var ce = db.Entry(c);
                var Oldvalue = ce.CurrentValues.GetValue<string>("Title");
                var Newvalue = ce.CurrentValues.GetValue<string>("Title");

                Console.WriteLine("OldTitle:" + Oldvalue + " NewValue:" + Newvalue);


                foreach (var prop in ce.OriginalValues.PropertyNames)
                {
                    Console.WriteLine(prop + " :" + prop.GetType().Name);

                    //Console.WriteLine("prop :" + prop.ToString() + " : " + ce.OriginalValues.GetValue<prop.GetType()>(prop));


                }

                //可以把這段加入Setting.cs裡 統一管理
                ce.CurrentValues.SetValues(new
                    {
                        ModifiedOn = DateTime.Now
                    });
            }

        }

        public static void 離線模式(ContosoUniversityEntities db)
        {
            var c = new Course()
            {
                CourseID = 8,
                Title = "離線模式",
                DepartmentID = 1,
                Credits = 1
            };

            using (var db2 = new ContosoUniversityEntities())
            {
                
                Console.WriteLine(db2.Entry(c).State);
                db2.Course.Attach(c);
                Console.WriteLine(db2.Entry(c).State);
                c.Title = "離線模式2";
                Console.WriteLine(db2.Entry(c).State);
                db2.SaveChanges();


                //資料庫中的title 是離線模式2 但是兩次console.writeline都是tttt 因為被cache住了
                db2.Course.Add(c);
                 c.Title = "ttttt";
                Console.WriteLine(c.Title);
                
                db2.Course.Find(8);
                Console.WriteLine(c.Title);
                db2.SaveChanges();



            }
        }
    }
}
