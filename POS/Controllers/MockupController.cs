using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POS.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace POS.Controllers
{
    public class MockupController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult TestPull()
        {
            return View(Food.GetAll());
        }
    }
}
