using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Controllers
{
    public class ProfileController : Controller
    {
        private readonly MyContext myContext;

        public ProfileController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public IActionResult Index()
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var user = myContext.Users.Include(a=>a.Bookings).Single(a=>a.UserId==userId);

            int roomId=user.Bookings.OrderByDescending(a => a.DateTime).First(a => a.ReserveState == ReserveState.Accept).RoomId;

            ViewBag.room = myContext.Rooms.Find(roomId);

            return View(user);
        }


        [HttpPost]
        public IActionResult Reserve(string description)
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));
            Room room= myContext.Rooms.First(a => a.ActiveCount < a.BedCount && a.DormId == 1);
            Booking booking = new Booking()
            {
                DateTime = DateTime.Now,
                Description = description,
                UserID = userId,
                Room = room,
                ReserveState = ReserveState.Wait
            };
            myContext.Bookings.Add(booking);
            myContext.SaveChanges();

            myContext.SaveChanges();
           

            return RedirectToAction("index");

        }
    }
}