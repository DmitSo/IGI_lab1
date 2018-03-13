namespace lab1_ef
{
    public class Service
    {
        public int ServiceId { get; set; }

        public int? ClientId { get; set; }

        public int? EmployeeId { get; set; }

        public int? ServiceTypeId { get; set; }

        public virtual Client Client { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ServiceType ServiceType { get; set; }
    }
}
