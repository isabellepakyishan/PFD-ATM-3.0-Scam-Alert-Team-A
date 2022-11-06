using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PFD_ATM_3._0_Team_A.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string FaceDepthDetector()
        {
            string warning;

            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = @"C:\Users\chiam\AppData\Local\Programs\Python\Python39\python.exe",
                Arguments = @".\python\FaceDepthMeasurement.py",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };
            using (Process p = Process.Start(start))
            {
                using (StreamReader reader = p.StandardOutput)
                {
                    warning = reader.ReadToEnd();
                }
            }

            return warning;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
