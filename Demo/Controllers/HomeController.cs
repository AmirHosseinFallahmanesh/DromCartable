using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo.Models;
using Microsoft.AspNetCore.Http;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyContext myContext;

        public HomeController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string username,string password)
        {
            if (username=="admin" && password=="admin")
            {
                return RedirectToAction("index", "admin");
            }


           User user= myContext.Users.
                FirstOrDefault(a => a.Username == username && a.Password == password);
            if (user!=null)
            {
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                return RedirectToAction("index","Profile");
            }

            return View();


        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
