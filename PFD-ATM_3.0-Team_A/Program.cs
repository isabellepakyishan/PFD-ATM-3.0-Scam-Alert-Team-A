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
            await Task.Run(() => FaceDepthDetector());
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
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(new FileStream("Static/depth.txt", FileMode.OpenOrCreate)))
                        {
                            sw.WriteLine(e.Data);
                        }
                    }
                    catch (IOException io) { }

                });

                start.Start();
                start.BeginOutputReadLine();
                start.WaitForExit();

            }
            catch(Exception e) { }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
