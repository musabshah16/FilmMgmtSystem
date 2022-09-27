using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmMgmtSystem.MVC.Controllers
{
    public class BookFilm : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
