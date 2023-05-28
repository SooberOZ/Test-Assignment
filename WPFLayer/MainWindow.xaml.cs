using System;
using System.Net.Http;
using System.Windows;

namespace WPFLayer
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient;
        public MainWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.coingecko.com/api/v3/")
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new PopularCurrenciesPage(_httpClient);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new FindCurrency(_httpClient);
        }
    }
}