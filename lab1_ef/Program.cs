using System;
using System.Collections.Generic;
using System.Linq;

namespace lab1_ef
{
    class Program
    {
        static void Main(string[] args)
        {
            HotelContext db = new HotelContext();
            Initializer.Initialize(db);
            bool stay = true;
            while (stay)
            {
                Console.Clear();
                Console.WriteLine("======================================");
                Console.WriteLine("1 > Получить все виды услуг");
                Console.WriteLine("2 > Получить все виды услуг + фильтр");
                Console.WriteLine("3 > Получить комнаты + группировка по полю + вывод результата");
                Console.WriteLine("4 > Выборка данных из двух полей двух таблиц, связанных отношением «один-ко-многим»{0}"+
                                  "5 > Выборка данных из двух таблиц, связанных отношением «один-ко-многим» + фильтр{0}" +
                                  "6 > Вставка данных в таблицы на стороне отношения «Один»{0}" +
                                  "7 > Вставка данных в таблицы на стороне отношения «Многие»{0}" +
                                  "8 > Удаление данных из таблицы на стороне отношения «Один»{0}" +
                                  "9 > Удаление данных из таблицы на стороне отношения «Многие»{0}" +
                                  "0 > Обновление удовлетворяющих условию записей в любой из таблиц базы данных{0}", Environment.NewLine);
                Console.WriteLine("{0}ESCAPE > Выход", Environment.NewLine);

                switch(Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        OutputServiceTypes(db.ServiceTypes.ToList());
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        OutputServiceTypes(FilterServices(db.ServiceTypes).ToList());
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        OutputRooms(GroupRoomsByCapacity(db.Rooms));
                        break;
                    case ConsoleKey.D4:
                        Console.Clear();
                        JoinTwoTables(db.Services.ToList(), db.ServiceTypes.ToList());
                        break;
                    case ConsoleKey.D5:
                        Console.Clear();
                        Console.WriteLine("COST < 60 {0}", Environment.NewLine);
                        JoinFilterTwoTables(db.Services.ToList(), db.ServiceTypes.ToList());
                        break;
                    case ConsoleKey.D6:
                        Console.Clear();
                        OutputServiceTypes(db.ServiceTypes.ToList());
                        Console.WriteLine("{0}{0}", Environment.NewLine);
                        AddServiceType(db, new ServiceType { Name = $"TestType {DateTime.Now}", Cost = 500 });
                        Console.WriteLine("{0}{0}", Environment.NewLine);
                        OutputServiceTypes(db.ServiceTypes.ToList());
                        break;
                    case ConsoleKey.D7:
                        Console.Clear();
                        JoinTwoTables(db.Services.ToList(), db.ServiceTypes.ToList());
                        Console.WriteLine("{0}{0}", Environment.NewLine);
                        AddService(db, new Service { ServiceType = db.ServiceTypes.FirstOrDefault
                            (st => st.Name.Contains("TestType"))});
                        Console.WriteLine("{0}{0}", Environment.NewLine);
                        JoinTwoTables(db.Services.ToList(), db.ServiceTypes.ToList());
                        break;
                    case ConsoleKey.D8:
                        {
                            var value = db.ServiceTypes.FirstOrDefault(st => st.Name.Contains("TestType"));
                            if (value != null)
                            {
                                Console.Clear();
                                Console.WriteLine("{0}{0}", Environment.NewLine);
                                OutputServiceTypes(db.ServiceTypes.ToList());
                                DeleteServiceType(db, value);
                                Console.WriteLine("{0}{0}", Environment.NewLine);
                                OutputServiceTypes(db.ServiceTypes.ToList());
                            }
                            else
                                Console.WriteLine("Нет данных для удаления");
                        }
                        break;
                    case ConsoleKey.D9:
                        {
                            var value = db.Services.FirstOrDefault(st => st.ServiceType.Name.Contains("TestType"));
                            if (value != null)
                            {
                                Console.Clear();
                                Console.WriteLine("{0}{0}", Environment.NewLine);
                                JoinTwoTables(db.Services.ToList(), db.ServiceTypes.ToList());
                                DeleteService(db, value);
                                Console.WriteLine("{0}{0}", Environment.NewLine);
                                JoinTwoTables(db.Services.ToList(), db.ServiceTypes.ToList());
                            }
                            else
                                Console.WriteLine("Нет данных для удаления");
                        }
                        break;
                    case ConsoleKey.D0:
                        Console.Clear();
                        var client = new Client { Name = "TESTCLIENT", Passport = "AAA" };
                        Console.WriteLine($"{client.Name} ({client.Passport})");
                        AddClient(db, client);
                        EditClient(db, client);
                        Console.WriteLine($"{client.Name} ({client.Passport})");
                        break;

                    case ConsoleKey.Escape:
                        stay = false;
                        break;

                    default:
                        continue;
                }
                Console.ReadKey();
            }            
        }

        static void OutputServiceTypes(List<ServiceType> data)
        {
            foreach(var e in data)
                Console.WriteLine($"{e.Name} [{e.Cost}] " +
                    $"{(string.IsNullOrWhiteSpace(e.Description) ? "" : $"{Environment.NewLine}   {e.Description}")}");
        }
        
        static void OutputRooms(List<Room> data)
        {
            foreach (var e in data)
                Console.WriteLine($"[{e.RoomNo}] {e.RoomType} ({e.Capacity} чел)");
        }

        static void OutputRooms(IQueryable<IGrouping<int, Room>> data)
        {
            foreach (var group in data)
            {
                Console.WriteLine("{0} {1} {0}", new string('*', 15), group.Key);
                OutputRooms(group.ToList());
            }
        }

        static IQueryable<ServiceType> FilterServices(IQueryable<ServiceType> data)
        {
            bool stay = true;
            IQueryable<ServiceType> result = data;
            
            while (stay)
            {
                Console.WriteLine("Фильтр по:");
                Console.WriteLine("1 > Description");
                Console.WriteLine("2 > Cost");
                Console.WriteLine();
                Console.WriteLine("4 > Сброс");
                Console.WriteLine("5 > Стоп");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:
                        {
                            Console.WriteLine("1 > Пуст");
                            Console.WriteLine("2 > Не пуст");

                            var k = Console.ReadKey(true).Key;

                            if (k == ConsoleKey.D1)
                                result = result.Where(o => string.IsNullOrWhiteSpace(o.Description));
                            else if (k == ConsoleKey.D2)
                                result = result.Where(o => !string.IsNullOrWhiteSpace(o.Description));
                            else
                                Console.WriteLine("Выбран неверный пункт");
                        }
                        break;

                    case ConsoleKey.D2:
                        {
                            Console.WriteLine("1 > Меньше");
                            Console.WriteLine("2 > Больше");

                            var k = Console.ReadKey(true).Key;

                            if (k == ConsoleKey.D1 || k == ConsoleKey.D2)
                            {
                                Console.WriteLine("Значение: ");
                                if (double.TryParse(Console.ReadLine(), out double value))
                                {
                                    if (k == ConsoleKey.D1)
                                        result = result.Where(o => o.Cost <= value);
                                    else
                                        result = result.Where(o => o.Cost >= value);
                                }
                                else
                                    Console.WriteLine("Ошибка при преобразовании значения");
                            }
                            else
                                Console.WriteLine("Выбран неверный пункт");
                        }
                        break;

                    case ConsoleKey.D4:
                        result = data;
                        break;

                    case ConsoleKey.D5:
                        stay = false;
                        break;
                }
                Console.Clear();
            }

            return result;
        }

        static IQueryable<IGrouping<int, Room>> GroupRoomsByCapacity(IQueryable<Room> data)
        {
            var value = from item in data
                group item by item.Capacity;
            return value;
        }

        static void JoinTwoTables(List<Service> services, List<ServiceType> serviceType)
        {
            var result = services.Join(serviceType, 
                p => p.ServiceTypeId, q => q.ServiceTypeId,
                (p, q) => new
                {
                    ServiceName = q.Name,
                    ClientName = p.Client?.Name,
                    p.Client?.Room?.RoomNo,
                    q.Cost
                });

            // вывод тут же - работать с анонимными типами между методами тот еще гемор
            foreach (var item in result)
                Console.WriteLine($"{item.ServiceName} для {item.ClientName}(комната #{item.RoomNo}) [{item.Cost}]");
        }
        
        static void JoinFilterTwoTables(List<Service> services, List<ServiceType> serviceType)
        {
            var result = services.Join(serviceType,
                p => p.ServiceTypeId, q => q.ServiceTypeId,
                (p, q) => new
                {
                    ServiceName = q.Name,
                    ClientName = p.Client?.Name,
                    p.Client?.Room?.RoomNo,
                    q.Cost
                });

            result = result.Where(r => r.Cost < 60);

            // вывод тут же - работать с анонимными типами между методами тот еще гемор
            foreach (var item in result)
                Console.WriteLine($"{item.ServiceName} для {item.ClientName}(комната #{item.RoomNo}) [{item.Cost}]");
        }
        
        static void AddServiceType(HotelContext db, ServiceType serviceType)
        {
            db.ServiceTypes.Add(serviceType);
            db.SaveChanges();
        }

        static void AddService(HotelContext db, Service service)
        {
            db.Services.Add(service);
            db.SaveChanges();
        }

        static void DeleteServiceType(HotelContext db, ServiceType serviceType)
        {
            var value = db.ServiceTypes.Where(st => st.ServiceTypeId == serviceType.ServiceTypeId);

            db.ServiceTypes.RemoveRange(value);
            db.SaveChanges();
        }

        static void DeleteService(HotelContext db, Service service)
        {
            var value = db.Services.Where(sv => sv.ServiceId == service.ServiceId).ToList();

            db.Services.RemoveRange(value?.ToList());
            db.SaveChanges();
        }

        static void AddClient(HotelContext db, Client client)
        {
            db.Clients.Add(client);
            db.SaveChanges();
        }

        static void EditClient(HotelContext db, Client client)
        {
            Client value = db.Clients.Where(c => c.ClientId == client.ClientId).First();

            value.Name = "Соловьев Дмитрий Сергеевич";
            value.Passport = "HB1234567";
            value.OccupancyDate = DateTime.Now;
            value.OccupancyDate = value.OccupancyDate + new TimeSpan(7,0,0,0);

            db.SaveChanges();
        }        
    }
}
