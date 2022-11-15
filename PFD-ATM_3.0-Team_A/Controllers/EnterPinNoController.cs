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
        public ActionResult StorePinNo(IFormCollection form)
        {
            string pin1 = form["pin1"];
            string pin2 = form["pin2"];
            string pin3 = form["pin3"];
            string pin4 = form["pin4"];
            string pin5 = form["pin5"];
            string pin6 = form["pin6"];
            string pin7 = form["pin7"];

            string pinNo = pin1 + pin2 + pin3 + pin4 + pin5 + pin6 + pin7;

            return RedirectToAction("Index", "MainMenu");
        }
    }
}
