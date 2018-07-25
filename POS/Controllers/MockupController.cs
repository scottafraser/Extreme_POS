using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POS.Models;
using POS.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace POS.Controllers
{
    public class MockupController : Controller
    {
        [HttpGet("/home")]
        public IActionResult Index(int id)
        {
            Models.User existingUser = Models.User.Find(id);
            OrderInfo newOrder = new OrderInfo();
            newOrder.FoundUser = existingUser;

            return View(newOrder);
        }



        public IActionResult TestPull()
        {
            return View(Food.GetAll());
        }

    }
}
