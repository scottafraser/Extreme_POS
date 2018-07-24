using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using POS.Models;

namespace POS.Controllers
{
    public class TableController : Controller
    {
        [HttpGet("/table/map")]
        public ActionResult Map()
        {
            return View();
        }

        [HttpGet("/table/add")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet("/table")]
        public ActionResult ViewAll()
        {
            return View(Ticket.GetAll());
        }

        [HttpPost("/tables")]
        public ActionResult ViewAllPost(int tableNumber, int seats)
        {
            Table newTable = new Table(tableNumber, seats);
            newTable.Save();

            return RedirectToAction("ViewAll");
        }

        [HttpGet("/table/{id}/update")]
        public ActionResult Update(int id)
        {
            Table editTable = Table.Find(id);

            return View(editTable);
        }

        [HttpPost("/table/{id}/update")]
        public ActionResult UpdatePost(int id, int newNumber)
        {
            Table editTable = Table.Find(id);
            editTable.Edit(newNumber);

            return RedirectToAction("ViewAll");
        }

        [HttpGet("/table/{id}/details")]
        public ActionResult Details(int id)
        {
            Table tableDetails = Table.Find(id);

            return View(tableDetails);
        }

        [HttpGet("/table/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Table deleteTable = Table.Find(id);
            deleteTable.Delete();

            return RedirectToAction("ViewAll");
        }

        [HttpGet("/table/delete")]
        public ActionResult DeleteAll()
        {
            Table.DeleteAll();

            return RedirectToAction("ViewAll");
        }

        [HttpGet("/table/sell-table")]
        public ActionResult Sell(int id)
        {
            Table soldTable = Table.Find(id);
            soldTable.SellTable();

            return RedirectToAction("ViewAll", "History");
        }
    }
}