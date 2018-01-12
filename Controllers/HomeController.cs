using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFDC.Models;
using EFDC.Services;
using EFDC.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EFDC.Controllers
{
    public class HomeController : Controller
    {
      
        // GET: /<controller>/
        public IActionResult Index()
        {          

            return View();
        }



    }
}
