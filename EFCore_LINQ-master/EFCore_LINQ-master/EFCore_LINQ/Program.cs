using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EFCore_LINQ.Models;
using System.Collections;

namespace EFCore_LINQ
{
    public class Program
    {
        public static void Main(string[] args)
        {
    

            using (FuelContext db = new FuelContext())
            {
                DbInitializer.Initialize(db);
                //Выполняем разные методы, содержащие операции выборки и изменения данных
                Console.WriteLine("====== Будет выполнена выборка данных (нажмите любую клавишу) ========");
                Console.ReadKey();
                Select(db);
                Console.WriteLine("====== Будет выполнена вставка данных (нажмите любую клавишу) ========");
                Console.ReadKey();
                Insert(db);
                Console.WriteLine("====== Выборка после вставки ========");
                Select(db);
                Console.WriteLine("====== Будет выполнено обновление данных (нажмите любую клавишу) ========");
                Console.ReadKey();
                Update(db);
                Console.WriteLine("====== Выборка после обновления ========");
                Select(db);
                Console.WriteLine("====== Будет выполнено удаление данных (нажмите любую клавишу) ========");
                Console.ReadKey();
                Delete(db);
                Console.WriteLine("====== Выборка после удаления ========");
                Select(db);

            }
            Console.Read();
        }

        static void Print(string sqltext, IEnumerable items)
        {
            Console.WriteLine(sqltext);
            Console.WriteLine("Записи: ");
            foreach (var item in items)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
            Console.ReadKey();
        }

        static void Insert(FuelContext db)
        {
            // Создать новую емкость
            Tank tank = new Tank
            {
                TankType = "Бочка",
                TankMaterial = "Дерево",
                TankVolume = 30,
                TankWeight = 100
            };
            // Создать новый вид топлива
            Fuel fuel = new Fuel
            {
                FuelType = "Нитроглицерин",
                FuelDensity = 3
            };

            // Добавить в DbSet
            db.Tanks.Add(tank);
            db.Fuels.Add(fuel);
            // Сохранить изменения в базе данных
            db.SaveChanges();

            // Создать новую операцию
            Operation operation = new Operation
            {
                TankID = tank.TankID,
                FuelID = fuel.FuelID,
                Inc_Exp = 1000,
                Date = DateTime.Now
            };

            // Добавить в DbSet
            db.Operations.Add(operation);
            // Сохранить изменения в базе данных
            db.SaveChanges();

        }
        static void Select(FuelContext db)
        {

            // Определение LINQ запроса 1
            var queryLINQ1 = from f in db.Operations
                             join t in db.Fuels
                             on f.FuelID equals t.FuelID
                             where (f.Inc_Exp > 0 && f.Date.Year == 2016)
                             orderby f.FuelID descending
                             select new
                             {
                                 Код_операции = f.OperationID,
                                 Название_топлива = t.FuelType,
                                 Приход_Расход = f.Inc_Exp,
                                 Месяц = f.Date.Month
                             };

            //то же, используя методы расширений
            //var queryLINQ1 = db.Operations.Where(f => (f.Inc_Exp > 0 && f.Date.Value.Year == 2016))
            //.OrderBy(f => f.FuelID)
            //.Join(db.Fuels, f => f.FuelID, t => t.FuelID, (f, t) => new { f.OperationID, t.FuelType, f.Inc_Exp, f.Date.Value.Month });

            string comment = "1. Результат выполнения запроса на выборку отсортированных записей из двух таблиц, удовлетворяющих заданному условию : \r\n";
            //для наглядности выводим не более 5 записей
            Print(comment, queryLINQ1.Take(5).ToList());

            // Определение LINQ запроса 2 
            var queryLINQ2 = from o in db.Operations
                             where (o.Inc_Exp > 0 && o.Date.Year == 2016)
                             group o.Inc_Exp by o.FuelID into gr
                             select new
                             {
                                 Код_топлива = gr.Key,
                                 Количество_топлива = gr.Sum()
                             };
            //то же, используя методы расширений:
            //var queryLINQ2 = db.Operations.Where(o => ((o.Inc_Exp > (Single?)0) && (o.Date.Value.Year == 2016)))
            //    .GroupBy(o => o.FuelID, o => o.Inc_Exp)
            //    .Select(gr => new
            //    {
            //        Код_топлива = gr.Key,
            //        Количества_топлива = gr.Sum()
            //    }
            //     );

            comment = "2. Результат выполнения запроса на выборку сгруппированных записей из одной таблицы, удовлетворяющих заданному условию, с выполнением групповой операции суммирования : \r\n";
            //для наглядности выводим не более 5 записей
            Print(comment, queryLINQ2.Take(5).ToList());

            // Определение LINQ запроса 3
            var queryLINQ3 = from t in db.Tanks
                             orderby t.TankID descending
                             select new
                             {
                                 Название_Емкости = t.TankType,
                                 Материал_Емкости = t.TankMaterial,
                                 Объем_Емкости = t.TankVolume,
                                 Вес = t.TankWeight
                             };

            comment = "3. Результат выполнения запроса на выборку записей из одной таблицы с выводом определенных полей: \r\n";
            //для наглядности выводим не более 5 записей
            Print(comment, queryLINQ3.Take(5).ToList());
        }

        static void Delete(FuelContext db)
        {
            //подлежащие удалению записи в таблице Tanks
            string nametank = "Бочка1";
            var tank = db.Tanks.Where(c => c.TankType == nametank);

            //подлежащие удалению записи в таблице Fuels
            string namefuel = "Нитроглицерин1";
            var fuel = db.Fuels
                .Where(c => c.FuelType == namefuel);

            //подлежащие удалению записи в связанной таблице Operations
            var someOperations = db.Operations
                .Include("Tank")
                .Include("Fuel")
                .Where(o => ((o.Tank.TankType == nametank)) && (o.Fuel.FuelType == namefuel));

            //Удаление нескольких записей в таблице Operations    
            db.Operations.RemoveRange(someOperations);
            // сохранить изменения в базе данных
            db.SaveChanges();

            //Удаление нескольких записей в таблице Tanks и в таблице Fuels
            db.Tanks.RemoveRange(tank);
            db.Fuels.RemoveRange(fuel);

            // сохранить изменения в базе данных
            db.SaveChanges();

        }
        static void Update(FuelContext db)
        {
            //подлежащие обновлению записи в таблице Tanks
            string nametank = "Бочка";
            var tank = db.Tanks.Where(c => c.TankType == nametank).FirstOrDefault();
            //обновление
            if (tank != null)
            {
                tank.TankType = "Бочка1";
                tank.TankMaterial = "Дерево1";
            };

            //подлежащие обновлению записи в таблице Fuels
            string namefuel = "Нитроглицерин";
            var fuel = db.Fuels.Where(c => c.FuelType == namefuel).FirstOrDefault();
            //обновление
            if (fuel != null)
            {
                fuel.FuelType = "Нитроглицерин1";
            };

            //подлежащие обновлению записи в связанной таблице Operations
            var someOperations = db.Operations.Include("Tank").Include("Fuel")
                .Where(o => ((o.Tank.TankType == nametank)) && (o.Fuel.FuelType == namefuel));
            //обновление
            if (someOperations != null)
            {
                foreach (var op in someOperations)
                {
                    op.Inc_Exp = 0;
                };
            }

            // сохранить изменения в базе данных
            db.SaveChanges();

        }


    }
}