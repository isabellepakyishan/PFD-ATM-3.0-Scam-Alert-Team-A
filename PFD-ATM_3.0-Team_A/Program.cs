using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Collections;

namespace PFD_ATM_3._0_Team_A
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var prog = new Program();
            prog.Output();

            CreateHostBuilder(args).Build().Run();
        }

        protected async void Output()
        {
            await Task.Run(() => FaceDepthAndExpressionRecognition());
        }
        protected void FaceDepthAndExpressionRecognition()
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = GetPythonPath(),
                    Arguments = @".\python\FaceDepthAndExpressionRecognition.py",
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
                    try
                    {
                        if (e.Data != null && e.Data.StartsWith("fd"))
                        {
                            using (StreamWriter sw = new StreamWriter(new FileStream("Static/depth.txt", FileMode.OpenOrCreate)))
                            {
                                sw.WriteLine(e.Data[2..]);
                            }
                        }
                        else if (e.Data != null && e.Data.StartsWith("fer"))
                        {
                            using (StreamWriter sw = new StreamWriter(new FileStream("Static/fear.txt", FileMode.OpenOrCreate)))
                            {
                                sw.WriteLine(e.Data[3..]);
                            }
                        }
                    }
                    catch (Exception p) { }

                });

                start.Start();
                start.BeginOutputReadLine();
                start.WaitForExit();

            }
            catch(Exception e) { }
        }

        //get python path from environtment variable
        protected string GetPythonPath()
        {
            string environmentVariable = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User);
            Debug.WriteLine(environmentVariable);
            if (environmentVariable != null)
            {
                string[] allPaths = environmentVariable.Split(';');
                foreach (var path in allPaths)
                {
                    string pythonPathFromEnv = path + "\\python.exe";
                    if (File.Exists(pythonPathFromEnv))
                        return pythonPathFromEnv;
                }
            }
            return "";
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
