﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.IO;

namespace EnjoyCodes.OcelotPractise.APIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                    .AddJsonFile("ocelot.json", false, false)
                    .AddEnvironmentVariables();
                })
                .ConfigureServices(s =>
                {
                    s.AddOcelot();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    //add your logging
                })
                .UseIISIntegration()
                .Configure(app =>
                {
                    app.UseOcelot().Wait();
                })
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
