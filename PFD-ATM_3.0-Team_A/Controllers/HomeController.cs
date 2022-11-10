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

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Output();

            return View();
        }

/*        protected ActionResult Warning()
        {
            string depth = "";
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                using (StringReader sr = new StringReader(sw.ToString()))
                {
                    Console.WriteLine(sr.ReadLine());
                }
            }

            return PartialView("_CV", depth);
        }*/

        protected async void Output()
        {
            await Task.Run(() => FaceDepthDetector());
            /*await Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(new FileStream("depth.txt", FileMode.OpenOrCreate)))
                        {
                            string depth = sr.ReadToEnd();
                            ViewData["Warning"] = depth;
                        }
                    }
                    catch (IOException e)
                    {  }
                    
                }
            });*/
        }
        protected void FaceDepthDetector()
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = @"C:\Users\chiam\AppData\Local\Programs\Python\Python39\python.exe",
                    Arguments = @".\python\FaceDepthMeasurement.py",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                Process start = new Process
                {
                    StartInfo = startInfo
                };
                start.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    using (StreamWriter sw = new StreamWriter(new FileStream("Static/depth.txt", FileMode.OpenOrCreate)))
                    {
                        sw.WriteLine(e.Data);
                    }
                });

                start.Start();
                start.BeginOutputReadLine();
                start.WaitForExit();

            }
            catch(Exception e) { }
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
