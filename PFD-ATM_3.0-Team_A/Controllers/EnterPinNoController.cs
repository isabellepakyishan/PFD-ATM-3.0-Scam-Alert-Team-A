using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PFD_ATM_3._0_Team_A.Models;
using System;
using System.Web;
using System.Net.Http;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using PFD_ATM_3._0_Team_A.Models;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class EnterPinNoController : Controller
    {
        public IActionResult Index()
        {
            return View();  
        }
        
        [HttpPost]
        public ActionResult Index(IFormCollection formData)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
