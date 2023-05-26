using System.Net.Http;
using System;
using System.Windows;

namespace WPFLayer
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureServices();
            ConfigureNavigation();
        }

        private void ConfigureServices()
        {
            services.AddHttpClient();
        }

        private void ConfigureNavigation()
        {
            
        }
    }
}