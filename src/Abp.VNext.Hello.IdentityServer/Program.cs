﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Abp.VNext.Hello;

public class Program
{
    private static IConfiguration GetConfiguration()
    {
       
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.Development.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        return builder.Build();
    }
    public async static Task<int> Main(string[] args)
    {
        IConfiguration configuration = GetConfiguration();
        string seqServerUrl = configuration["Seq:Url"];
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Seq(seqServerUrl)
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        try
        {
            Log.Information("Starting Abp.VNext.Hello.IdentityServer.");
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson(true)
                .UseAutofac()
                .UseSerilog();
            await builder.AddApplicationAsync<HelloIdentityServerModule>();
            WebApplication app = builder.Build();
            await app.InitializeApplicationAsync();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Abp.VNext.Hello.IdentityServer terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
