using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace exCoreMvc.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        {
            ViewData["message"] = "Kağan";
            ViewData["count"] = 5;
            return View();
        }

        public string Welcome(string name,int age=18)
        {
            return $"this is welcome page.Welcome {name} , {age}";
        }
    }
}