using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using POS.Models;
using POS.ViewModels;

namespace POS.Controllers
{
    public class TicketController : Controller
    {
        [HttpGet("/ticket/add")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet("/tickets")]
        public ActionResult Index()
        {
            OrderInfo newOrder = new OrderInfo();

            return View(newOrder);
        }

        [HttpPost("/tickets")]
        public ActionResult ViewAllPost(int ticketNumber)
        {
            Ticket newTicket = new Ticket(ticketNumber);
            newTicket.Save();

            return RedirectToAction("ViewAll", newTicket);
        }

        [HttpGet("/ticket/{id}/update")]
        public ActionResult Update(int id)
        {
            Ticket editTicket = Ticket.Find(id);

            return View(editTicket);
        }

        [HttpPost("/ticket/{id}/update")]
        public ActionResult UpdatePost(int id, int newSeat, int newFoodId, int newDrinkId, int newUserId, int newTableId)
        {
            Ticket editTicket = Ticket.Find(id);
            editTicket.Edit(newSeat, newFoodId, newDrinkId, newUserId, newTableId);
            
            return RedirectToAction("ViewAll");
        }

        [HttpGet("/ticket/{id}/details")]
        public ActionResult Details(int id)
        {
            Ticket ticketDetails = Ticket.Find(id);

            return View(ticketDetails);
        }

        [HttpGet("/ticket/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Ticket deleteTicket = Ticket.Find(id);
            deleteTicket.Delete();

            return RedirectToAction("ViewAll");
        }

        [HttpGet("/ticket/delete")]
        public ActionResult DeleteAll()
        {
            Ticket.DeleteAll();

            return RedirectToAction("ViewAll");
        }

        [HttpGet("/ticket/close-ticket")]
        public ActionResult CloseTicket(int id)
        {
            Ticket closedTicket = Ticket.Find(id);
            Ticket.CloseTicket(closedTicket);

            return RedirectToAction("ViewAll");
        }
    }
}