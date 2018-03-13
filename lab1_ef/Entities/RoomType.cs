using System.Collections.Generic;

namespace lab1_ef
{
    public class RoomType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
