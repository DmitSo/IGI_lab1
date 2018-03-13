using System;
using System.Linq;

namespace lab1_ef
{
    public static class Initializer
    {        
        static string[] malenameusual = {"Александр", "Алексей", "Анатолий", "Андрей", "Антон", "Аркадий",
            "Артём", "Богдан", "Борис", "Вадим", "Валентин", "Валерий", "Василий", "Виктор", "Виталий", "Владимир",
            "Владислав", "Вячеслав", "Гавриил", "Геннадий", "Георгий", "Глеб", "Григорий", "Даниил", "Данила", "Денис",
            "Дмитрий", "Евгений", "Егор", "Кирилл", "Иван", "Игорь", "Илья", "Иннокентий", "Лев", "Леонид", "Максим",
            "Матвей", "Михаил", "Никита", "Николай", "Олег", "Павел", "Пётр", "Роман", "Ростислав", "Руслан", "Семён",
            "Святослав", "Сергей", "Станислав", "Степан", "Тимофей", "Тимур", "Фёдор", "Филипп", "Эдуард", "Юрий", "Яков", "Ярослав" };

        static string[] patronimusual = { "Александрович", "Алексеевич", "Анатольевич", "Андреевич", "Антонович",
            "Аркадьевич", "Артёмович", "Богданович", "Борисович", "Вадимович", "Валентинович", "Валерьевич",
            "Васильевич", "Викторович", "Витальевич", "Владимирович", "Владиславович", "Вячеславович", "Гавриилович",
            "Геннадьевич", "Георгиевич", "Глебович", "Григорьевич", "Даниилович", "Данилович", "Денисович", "Дмитриевич",
            "Евгеньевич", "Егорович", "Кириллович", "Иванович", "Игоревич", "Ильич", "Иннокентьевич", "Львович",
            "Леонидович", "Максимович", "Матвеевич", "Михайлович", "Никитич", "Николаевич", "Олегович", "Павлович",
            "Петрович", "Романович", "Ростиславович", "Русланович", "Семёнович", "Святославович", "Сергеевич", "Станиславович",
            "Степанович", "Тимофеевич", "Тимурович", "Фёдорович", "Филиппович", "Эдуардович", "Юрьевич", "Яковлевич", "Ярославович" };

        static string[] lastnameusual = { "Смирнов", "Иванов", "Кузнецов", "Новиков", "Морозов", "Петров", "Павлов",
            "Семёнов", "Богданов", "Воробьёв", "Тарасов", "Белов", "Киселёв", "Макаров", "Андреев", "Ковалёв", "Ильин",
            "Гусев", "Титов", "Кузьмин", "Кудрявцев", "Баранов", "Куликов", "Алексеев", "Степанов", "Яковлев", "Сорокин",
            "Сергеев", "Романов", "Захаров", "Борисов", "Королёв", "Герасимов", "Пономарёв", "Григорьев", "Лазарев", "Ершов",
            "Никитин", "Соболев", "Рябов", "Цветков", "Данилов", "Журавлёв", "Николаев", "Крылов", "Максимов", "Сидоров",
            "Осипов", "Белоусов", "Федотов", "Дорофеев", "Егоров", "Матвеев", "Бобров", "Дмитриев", "Анисимов", "Антонов",
            "Тимофеев", "Никифоров", "Веселов", "Филиппов", "Марков", "Большаков", "Суханов", "Миронов", "Ширяев", "Александров",
            "Коновалов", "Шестаков", "Казаков", "Громов", "Фомин", "Давыдов", "Мельников", "Щербаков", "Блинов", "Колесников",
            "Афанасьев", "Власов", "Исаков", "Тихонов", "Аксёнов", "Родионов", "Котов", "Зуев", "Панов", "Рыбаков", "Абрамов",
            "Воронов", "Мухин", "Архипов", "Трофимов", "Горшков", "Овчинников", "Панфилов", "Копылов", "Лобанов", "Лукин", "Беляков",
            "Потапов", "Некрасов", "Хохлов", "Жданов", "Наумов", "Шилов", "Воронцов", "Ермаков", "Дроздов", "Игнатьев", "Савин",
            "Логинов", "Сафонов", "Капустин", "Кириллов", "Моисеев", "Елисеев", "Кошелев", "Костин", "Горбачёв", "Орехов",
            "Ефремов", "Исаев", "Евдокимов", "Калашников", "Кабанов", "Носков", "Юдин", "Кулагин", "Лапин", "Прохоров", "Нестеров",
            "Харитонов", "Агафонов", "Муравьёв", "Ларионов", "Федосеев", "Зимин", "Пахомов", "Шубин", "Игнатов", "Филатов", "Крюков",
            "Рогов", "Кулаков", "Терентьев", "Молчанов", "Владимиров", "Артемьев", "Гурьев", "Зиновьев", "Гришин", "Кононов", "Дементьев",
            "Ситников", "Симонов", "Мишин", "Фадеев", "Комиссаров", "Мамонтов", "Носов", "Гуляев", "Шаров", "Устинов", "Вишняков",
            "Евсеев", "Лаврентьев", "Брагин", "Константинов", "Корнилов", "Авдеев", "Зыков", "Бирюков", "Шарапов", "Никонов", "Щукин",
            "Дьячков", "Одинцов", "Сазонов", "Якушев", "Красильников", "Гордеев", "Самойлов", "Князев", "Беспалов", "Уваров", "Шашков",
            "Бобылёв", "Доронин", "Белозёров", "Рожков", "Самсонов", "Мясников", "Лихачёв", "Буров", "Сысоев", "Фомичёв", "Русаков",
            "Стрелков", "Гущин", "Тетерин", "Колобов", "Субботин", "Фокин", "Блохин", "Селиверстов", "Пестов", "Кондратьев", "Силин",
            "Меркушев", "Лыткин", "Туров" };

        public static void Initialize(HotelContext db)
        {
            db.Database.EnsureCreated();
            
            // check only by this dbset
            if (db.Clients.Any())
            {
                return;  
            }

            int roomsCnt = 100;
            int clientsCnt = 70; // must be less than rooms cnt
            int employeesCnt = 100;
            int servicesCnt = 100;

            Random rand = new Random(DateTime.Now.Millisecond);

            string[] roomTypes = { "Люкс", "Стандарт", "Эконом" };
            string[] serviceTypes = { "Уборка", "Еда в номер", "Пополнение мини-бара" };
            double[] sTypesCosts = { 70, 50, 30 };  // same size as for serviceTypes
            string[] passSeries = { "AB", "BM", "HB", "KH", "MP", "MC", "KB", "PP", "ВМ" }; // + 7 digits

            // firstly, fill constants
            // fill room types
            foreach (var rt in roomTypes)
                db.RoomTypes.Add(new RoomType { Name = rt });
            db.SaveChanges();

            // fill serv types
            for (int i = 0; i < roomTypes.Length; i++)
                db.ServiceTypes.Add(new ServiceType { Name = serviceTypes[i],
                    Cost = sTypesCosts[i], Description = rand.Next() % 2 == 1 ? "SomeDescription" : null });
            db.SaveChanges();

            // fill rooms 
            var rTypes = db.RoomTypes.ToList();
            for (int i = 0; i < roomsCnt; i++)
            {
                var value = new Room
                {
                    RoomType = rTypes[rand.Next(roomTypes.Length)],
                    Capacity = 2 + rand.Next(3), // 2, 3, 4
                    Cost = 75 + rand.NextDouble() % 75,
                    RoomNo = $"{(i + 1)}",
                    CostDate = DateTime.Now - new TimeSpan(rand.Next() % 20, 0, 0, 0),
                    Description = rand.Next() % 2 == 1 ? "Room Description" : null
                };
                db.Rooms.Add(value);
            }
            db.SaveChanges();

            // fill clients
            var rooms = db.Rooms.ToList();
            for (int i = 0; i < clientsCnt && i < rooms.Count; i++)
            {
                db.Clients.Add(new Client
                {
                    Name = $"{lastnameusual[rand.Next(lastnameusual.Length)]} " +
                    $"{malenameusual[rand.Next(malenameusual.Length)]} " +
                    $"{patronimusual[rand.Next(patronimusual.Length)]}",
                    Passport = $"{passSeries[rand.Next() % passSeries.Length]}{rand.Next(10000000)}",
                    Room = rooms[i],
                    DepartureDate = DateTime.Now + new TimeSpan(2, 0, 0, 0),
                    OccupancyDate = DateTime.Now - new TimeSpan(7 * rand.Next(3), 0, 0, 0)
                });
            }
            db.SaveChanges();

            // fill employees
            for (int i = 0; i < employeesCnt; i++)
            {
                db.Employees.Add(new Employee
                {
                    Name = $"{lastnameusual[rand.Next(lastnameusual.Length)]} " +
                    $"{malenameusual[rand.Next(malenameusual.Length)]} " +
                    $"{patronimusual[rand.Next(patronimusual.Length)]}"
                });
            }
            db.SaveChanges();

            // fill services
            var sTypes = db.ServiceTypes.ToList();
            var employees = db.Employees.ToList();
            var clients = db.Clients.ToList();
            for (int i = 0; i < servicesCnt; i++)
            {
                db.Services.Add(new Service
                {
                    ServiceType = sTypes[rand.Next(db.ServiceTypes.Count())],
                    Employee = employees[rand.Next(db.Employees.Count())],
                    Client = clients[rand.Next(db.Clients.Count())]
                });
            }
            db.SaveChanges();
        }
    }
}
