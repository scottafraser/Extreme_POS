using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POS.Models;

namespace POS.Controllers
{
    public class FoodController : Controller
    {
        [HttpGet("/food/add")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet("/foods")]
        public ActionResult ViewAll()
        {
            return View(Food.GetAll());
        }

        [HttpPost("/foods")]
        public ActionResult ViewAllPost(string name, int price, string category)
        {

            Food newFood = new Food(name, price, category);
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

