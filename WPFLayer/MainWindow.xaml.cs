using System;
using System.Net.Http;
using System.Windows;
using WPFLayer.WebApi;

namespace WPFLayer
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient;

        public MainWindow()
        {
            InitializeComponent();
            _httpClient = HttpClientSingleton.Instance;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new PopularCurrenciesPage(_httpClient);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new FindCurrency(_httpClient);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new DetailCurrencyInfoPage(_httpClient);
        }
    }
}