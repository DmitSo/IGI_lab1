using System.Collections.Generic;

namespace lab1_ef
{
    public class ServiceType
    {
        public int ServiceTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Cost { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
