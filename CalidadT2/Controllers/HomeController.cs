using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CalidadT2.Models;
using Microsoft.EntityFrameworkCore;

namespace CalidadT2.Controllers
{
    public class HomeController : Controller
    {
        private AppBibliotecaContext app;
        public HomeController(AppBibliotecaContext app)
        {
            this.app = app;
        }

        [HttpGet]
        public IActionResult Index()
        {            
            var model = app.Libros.Include(o => o.Autor).ToList();
            return View(model);
        }
    }
}
