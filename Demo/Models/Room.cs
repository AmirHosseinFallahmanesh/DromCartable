using System.Collections.Generic;

namespace Demo.Models
{
    public class Room
    {
        public int RoomId { get; set; }

        public string Number { get; set; }
        public int BedCount { get; set; }

        //اجاره داده شده
        public int ActiveCount { get; set; }

        public List<Booking> Bookings { get; set; }

        public int DormId { get; set; }
        public Dorm Dorm { get; set; }

    }
}