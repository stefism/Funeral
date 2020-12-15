using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Funeral.Web.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult Error()
        {           
            return View();
        }
    }
}
