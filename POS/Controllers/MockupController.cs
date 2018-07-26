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

            return View("Index", newOrder);
        }

        [HttpGet("/food-get")]
        public IActionResult FoodFlood()
        {
            OrderInfo newOrder = new OrderInfo();
            return PartialView("Food", newOrder );
        }

        [HttpPost("/food-add")]
        public IActionResult FoodAdd(string name, int id, int ticket)
        {
            OrderInfo newOrder = new OrderInfo();
            newOrder.FindFood(id);
            Orders order = new Orders(ticket, id, 0);
            order.Create();

            return PartialView("FoodAdd", newOrder.FoundFood);
        }

        [HttpGet("/drinks-get")]
        public IActionResult DrinkFlood()
        {
            OrderInfo newOrder = new OrderInfo();
            return PartialView("Drinks", newOrder);
        }

        [HttpPost("/drinks-add")]
        public IActionResult DrinkAdd(int id, int ticket)
        {
            OrderInfo newOrder = new OrderInfo();
            newOrder.FindDrink(id);
            Orders order = new Orders(ticket, 0, id);
            order.Create();

            return PartialView("DrinksAdd", newOrder.FoundDrink);
        }

        [HttpPost("/tickets/{table_id}/{user_id}/new")]
        public ActionResult CreateTicket(int table_id, int user_id)
        {
            Ticket newTicket = new Ticket(Ticket.GenerateTicketNumber());
            newTicket.Save();
            newTicket.AddUser(Models.User.Find(user_id));
            newTicket.AddTable(table_id);



            return PartialView("Ticket", newTicket);
        }

        [HttpGet("/ticket-update")]
        public ActionResult TicketUpdate(int id)
        {
            OrderInfo newOrder = new OrderInfo();
            Ticket newTicket = new Ticket(id);

            newOrder.AllFood = newTicket.GetFoodOrder();
            newOrder.AllDrink = newTicket.GetDrinkOrder();

            return PartialView(newOrder);
        }
         

    }
}
