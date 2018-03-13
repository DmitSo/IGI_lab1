using System;
using System.Collections.Generic;
using System.Linq;

namespace lab1_ef
{
    public class Client
    {
        public int ClientId { get; set; }

        public int? RoomId { get; set; }

        public string Name { get; set; }

        public string Passport { get; set; }

        public DateTime OccupancyDate { get; set; }

        public DateTime DepartureDate { get; set; }

        public virtual Room Room { get; set; }

        public ICollection<Service> Services { get; set; }

        public ICollection<Employee> ServeEmployees { get
            { return Services?.Where(s => s.Client == this).Select(s => s.Employee).ToList(); } }
    }
}
