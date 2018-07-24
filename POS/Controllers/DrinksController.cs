using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POS.Models;

namespace POS.Controllers
{
    public class DrinksController : Controller
    {
        [HttpGet("/drink/add")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet("/drinks")]
        public ActionResult Index()
        {
            return View(Drink.GetAll());
        }

        [HttpPost("/drinks")]
        public ActionResult ViewAllPost(string name, int price, string category)
        {

            Drink newDrink = new Drink(name, price, category);
            newDrink.Save();

            return RedirectToAction("Index");
        }

        [HttpGet("/drinks/{id}/details")]
        public ActionResult Details(int id)
        {
            Drink drinkDetails = Drink.Find(id);

            return View(drinkDetails);
        }

        [HttpGet("/drinks/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Drink deleteDrink = Drink.Find(id);
            deleteDrink.Delete();

            return RedirectToAction("Index");
        }

        [HttpGet("/drink/confirm")]
        public ActionResult Confirm()
        {
            return View();
        }

        [HttpGet("/drinks/delete")]
        public ActionResult DeleteAll()
        {
            Drink.DeleteAll();

            return RedirectToAction("Index");
        }
    }
}

