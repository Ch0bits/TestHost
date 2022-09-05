using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseDeveloperExceptionPage();
        app.UseRouting();
        app.UseEndpoints(route =>
        {
            route.MapControllers();
            route.MapFallbackToController("Index", "Default");
        });
    }
}

public static class Program
{
    public static void Main(string[] args)
    {
        if (!args.Any())
        {
            Console.WriteLine("Use port");
            return;
        }

        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .UseKestrel(options =>
                    {
                        options.ListenAnyIP(int.Parse(args[0]));
                    })
                    .UseStartup<Startup>();
            })
            .Build()
            .Run();
    }
}