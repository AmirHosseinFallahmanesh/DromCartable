using System;

namespace Demo.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public ReserveState ReserveState { get; set; }


    }
}