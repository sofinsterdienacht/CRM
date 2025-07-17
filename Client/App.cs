﻿using System.Configuration;
using System.IO;
using System.Net.Mime;
using System.Windows;
using Client.Options;
using Client.ViewModel;
using Client.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Client;

public partial class App : Application
{
    private readonly IHost _host;
    public static IConfiguration Configuration { get; private set; }
    public App()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Укажите путь к файлу appsettings.json
            .AddJsonFile("appsettingsClient.json").Build();
        
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<AuthorizeViewModel>();
                services.AddSingleton<MainWindow>();
                
                services.Configure<BackendOptions>(Configuration.GetSection("Backend"));
                services.AddHttpClient("HttpClient");
            })
            .Build();
    }
   
    protected override void OnStartup(StartupEventArgs e)
    {
        _host.Start();

        MainWindow = _host.Services.GetRequiredService<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);
    }
    protected override void OnExit(ExitEventArgs e)
    {
        _host.StopAsync();
        _host.Dispose();

        base.OnExit(e);
    }
}