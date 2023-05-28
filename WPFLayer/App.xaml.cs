using System.Net.Http;
using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace WPFLayer
{
    public partial class App : Application
    {
        //private ServiceProvider _serviceProvider;

        //public App()
        //{
        //    // Set up the DI container
        //    var services = new ServiceCollection();
        //    services.AddHttpClient("CoingeckoClient", client =>
        //    {
        //        client.BaseAddress = new Uri("https://api.coingecko.com/api/v3/");
        //    });
        //    services.AddSingleton<MainWindow>();
        //    services.AddSingleton<PopularCurrenciesPage>();

        //    // Register the WindowFactory
        //    services.AddSingleton<Func<MainWindow>>(sp =>
        //    {
        //        var provider = sp.GetRequiredService<IServiceProvider>();
        //        return () => provider.GetRequiredService<MainWindow>();
        //    });

        //    // Build the service provider
        //    _serviceProvider = services.BuildServiceProvider();
        //}

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);

        //    // Create the main window using WindowFactory
        //    var windowFactory = _serviceProvider.GetRequiredService<Func<MainWindow>>();
        //    var window = windowFactory();
        //    window.Show();
        //}
    }
}