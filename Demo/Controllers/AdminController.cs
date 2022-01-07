using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Controllers
{
    public class AdminController : Controller
    {
        private readonly MyContext myContext;

        public AdminController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public IActionResult Index()
        {
            List<Booking> bookings= myContext.Bookings.Include(a=>a.Room)
                .Include(a=>a.User).Where(a=>a.ReserveState==ReserveState.Wait).ToList();
            return View(bookings);
        }

        public IActionResult Accept(int id)
        {

            var booking= myContext.Bookings.Include(a => a.Room).First(a => a.BookingId == id);
            booking.ReserveState = ReserveState.Accept;

            Room room = myContext.Rooms.First(a => a.ActiveCount < a.BedCount);
            booking.Room = room;

            room.ActiveCount += 1;

            myContext.SaveChanges();


            return RedirectToAction("index");
        }

        public IActionResult Deny(int id)
        {
            var booking = myContext.Bookings.Find(id);
            booking.ReserveState = ReserveState.Deny;
            myContext.SaveChanges();
            return RedirectToAction("index");
        }
    }
}