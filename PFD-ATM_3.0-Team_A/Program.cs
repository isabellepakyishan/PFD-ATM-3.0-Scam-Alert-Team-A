using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Python.Runtime;
using System.IO;


namespace PFD_ATM_3._0_Team_A
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*var pathToVirtualEnv = @"path\to\env";
            var path = Environment.GetEnvironmentVariable("PATH").TrimEnd(';');

            path = string.IsNullOrEmpty(path) ? pathToVirtualEnv : path + ";" + pathToVirtualEnv;
            Environment.SetEnvironmentVariable("PATH", path, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONHOME", pathToVirtualEnv, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONPATH", $"{pathToVirtualEnv}\\Lib\\site-packages;{pathToVirtualEnv}\\Lib", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", @"C:\PFD\PFD-ATM_3.0-Team_A\PFD-ATM_3.0-Team_A\python\Python\Python39\python39.dll");

            PythonEngine.PythonHome = pathToVirtualEnv;
            PythonEngine.PythonPath = Environment.GetEnvironmentVariable("PYTHONPATH", EnvironmentVariableTarget.Process);

            PythonEngine.Initialize();
            using (Py.GIL())
            {
                dynamic cv2 = Py.Import("cv2");
                dynamic cvzone = Py.Import("cvzone");
                dynamic fmDetector = cvzone.FaceMeshDetector.Import("FaceMeshDetector");
                string code = File.ReadAllText("python\\FaceDepthMeasurement.py");
                //var compliedScript = PythonEngine.Compile(code);
                PythonEngine.Exec(code);
            }*/

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
