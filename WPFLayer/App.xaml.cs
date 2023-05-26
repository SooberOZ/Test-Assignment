using System.Net.Http;
using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace WPFLayer
{
    public partial class App : Application
    {
        private IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureServices();
            ConfigureHttpClient();
            ConfigureNavigation();
        }

        private void ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHttpClient();
            // Другие регистрации служб, если есть
            _httpClientFactory = services.BuildServiceProvider().GetRequiredService<IHttpClientFactory>();
        }

        private void ConfigureHttpClient()
        {
            _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://api.coingecko.com/api/v3");
        }

        private void ConfigureNavigation()
        {
            var mainWindow = new MainWindow(_httpClient);
            Current.MainWindow = mainWindow;
            mainWindow.Show();
        }
    }
}