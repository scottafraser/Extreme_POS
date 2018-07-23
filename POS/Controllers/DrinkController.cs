using System;
using Microsoft.AspNetCore.Mvc;
using POS.Models;

namespace POS.Controllers
{
    public class DrinkController
    {
        [HttpGet("/drink/add")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet("/drinks")]
        public ActionResult ViewAll()
        {
            return View(Drink.GetAll());
        }

        [HttpPost("/drinks")]
        public ActionResult ViewAllPost()
        {
            string name = Request.Form["name"];

            Drink newDrink = new Drink(name);
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

