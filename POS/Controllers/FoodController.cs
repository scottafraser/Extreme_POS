using System;
using POS.Models;

namespace POS.Controllers
{
    public class FoodController
    {
        [HttpGet("/food/add")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet("/foodss")]
        public ActionResult ViewAll()
        {
            return View(Stylist.GetAll());
        }

        [HttpPost("/foods")]
        public ActionResult ViewAllPost()
        {
            string name = Request.Form["name"];

            Food newFood = new Food(name);
            newFood.Save();

            return RedirectToAction("Index");
        }

        [HttpGet("/foods/{id}/details")]
        public ActionResult Details(int id)
        {
            Food foodDetails = Food.Find(id);

            return View(foodDetails);
        }

        [HttpGet("/foods/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Food deleteFood = Food.Find(id);
            deleteFood.Delete();

            return RedirectToAction("Index");
        }

        [HttpGet("/food/confirm")]
        public ActionResult Confirm()
        {
            return View();
        }

        [HttpGet("/foods/delete")]
        public ActionResult DeleteAll()
        {
            Food.DeleteAll();

            return RedirectToAction("Index");
        }
    }
}

