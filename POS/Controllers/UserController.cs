using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using POS.Models;

namespace POS.Controllers
{
    public class UserController : Controller
    {
        [HttpGet("/user/add")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet("/")]
        public ActionResult Index()
        {
            return View(Models.User.GetAll());
        }

        [HttpPost("/users/{id}")]
        public ActionResult ViewAllPost(int id)
        {

            return RedirectToAction("Index", "Mockup", new { id = id });
        }

        [HttpGet("/user/{id}/update")]
        public ActionResult Update(int id)
        {
            Models.User editUser = Models.User.Find(id);

            return View(editUser);
        }

        [HttpPost("/user/{id}/update")]
        public ActionResult UpdatePost(int id, string name, bool admin)
        {
            Models.User editUser = Models.User.Find(id);
            editUser.Edit(name, admin);

            return RedirectToAction("ViewAll");
        }

        [HttpGet("/user/{id}/details")]
        public ActionResult Details(int id)
        {
            Models.User userDetails = Models.User.Find(id);

            return View(userDetails);
        }

        [HttpGet("/user/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Models.User deleteUser = Models.User.Find(id);
            deleteUser.Delete();

            return RedirectToAction("ViewAll");
        }

        [HttpGet("/user/delete")]
        public ActionResult DeleteAll()
        {
            Models.User.DeleteAll();

            return RedirectToAction("ViewAll");
        }
    }
}