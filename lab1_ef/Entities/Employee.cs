using System.Collections.Generic;

namespace lab1_ef
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
