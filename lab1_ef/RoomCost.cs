using System;
using System.Collections.Generic;
using System.Text;

namespace lab1_ef
{
    class RoomCost
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public double Cost { get; set; }
        public DateTime Date { get; set; }
    }
}
