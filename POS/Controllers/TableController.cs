using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace POS.Controllers
{
    public class TableController : Controller
    {

        [HttpGet("/table/map")]
        public ActionResult Map()
        {
            //List<Table> tables = TableController.GetAll();
            return View();
        }
    }
}
