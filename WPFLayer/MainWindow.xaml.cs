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
                BaseAddress = new Uri("https://api.coingecko.com/api/v3")
            };
        }
        public MainWindow(HttpClient httpClient)
        {
            InitializeComponent();
            _httpClient = httpClient;
        }

        private void GoToPopularCurrenciesPage()
        {
            var popularCurrenciesPage = new PopularCurrenciesPage(_httpClient);
            MainFrame.Navigate(popularCurrenciesPage);
        }

        private void GoToCurrencyDetailsPage()
        {
            MainFrame.Navigate(new CurrencyDetailsPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GoToPopularCurrenciesPage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GoToCurrencyDetailsPage();
        }
    }
}