using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Controllers
{
    public class VisualizationController : Controller
    {
        [HttpPost]
        public JsonResult Index()
        {
            JsonResult jsonResult = new JsonResult(new { id = 1, name = "John" });
            return jsonResult ;
        }
        public ViewResult Graph()
        {
            return View("VisualInfo");
        }
    }
}
