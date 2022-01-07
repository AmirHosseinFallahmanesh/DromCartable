using System.Collections.Generic;

namespace Demo.Models
{
    public class Dorm
    {
        public int DormId { get; set; }
        public string  Name { get; set; }
        public string  Type { get; set; }
        public int Capacity { get; set; }

        public List<Room> Rooms { get; set; }

    }
}