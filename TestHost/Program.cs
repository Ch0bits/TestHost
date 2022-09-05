namespace TestHost;

using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

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