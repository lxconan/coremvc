﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using WebApp.Logging;

namespace WebApp
{
    static class Program
    {
        public static void Main(string[] args)
        { 
            WebAppLogger.Initialize();
            CreateWebHostBuilder(args).Build().Run();
        }

        static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            // The project template will create this method automatically for you. This method is
            // the entry point of a web application. The default implementation of
            // WebApplicationFactory class (in your unit test) requires this method. So if you
            // inline this method to Main, then your unit test is going to crash.
            // 
            // If you want to change the default behavior, you can derive form WebApplicationFactory
            // class and override the CreateWebHostBuilder() method.
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(
                    (context, logging) =>
                    {
                        // The default web host builder will add at least 3 logging sources:
                        // Console, Debug and EventSource. This configuration may not meet your
                        // requirement. So it is better to re-config by yourself.
                        logging.ClearProviders();
                        
                        // In a unit test environment. Since the Log.Logger is not initialized (
                        // the Main method will not be called), so a SilentLogger(NullLogger) will
                        // be used for unit test.
                        logging.AddSerilog(Log.Logger, true);
                    });
        }
    }
}