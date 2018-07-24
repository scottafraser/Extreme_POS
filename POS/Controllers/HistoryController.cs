using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using POS.Models;

namespace POS.Controllers
{
    public class HistoryController : Controller
    {
        [HttpGet("/history")]
        public ActionResult ViewAll()
        {
            return View(History.GetAll());
        }
    }
}