using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lab1_ef
{
    public class Room
    {
        public int RoomId { get; set; }

        public int? RoomTypeId { get; set; }

        public string RoomNo { get; set; }

        public int Capacity { get; set; }

        public string Description { get; set; }

        public double Cost { get; set; }

        public DateTime CostDate { get; set; }

        public virtual RoomType RoomType { get; set; }
        
        public virtual ICollection<Client> Clients { get; set; }
    }
}

