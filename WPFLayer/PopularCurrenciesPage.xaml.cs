using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFLayer.Models;

namespace WPFLayer
{
    public partial class PopularCurrenciesPage : Page
    {
        private readonly HttpClient _httpClient;

        public PopularCurrenciesPage(HttpClient httpClient)
        {
            InitializeComponent();
            _httpClient = httpClient;
            Loaded += PopularCurrenciesPage_Loaded;
        }

        public static readonly DependencyProperty PopularCurrenciesProperty =
            DependencyProperty.Register("PopularCurrencies", typeof(List<Currency>), typeof(PopularCurrenciesPage), new PropertyMetadata(null));

        public List<Currency> PopularCurrencies { get; set; }

        private async void PopularCurrenciesPage_Loaded(object sender, RoutedEventArgs e)
        {
            List<Currency> popularCurrencies = await GetPopularCurrencies();

            PopularCurrencies = popularCurrencies;
        }

        private async Task<List<Currency>> GetPopularCurrencies()
        {
            Uri requestUri = new Uri(_httpClient.BaseAddress, "/exchange_rates");
            HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                List<Currency> currencies = JsonConvert.DeserializeObject<List<Currency>>(responseContent);
                return currencies;
            }
            else
            {
                throw new ArgumentException("error with response");
            }
        }
    }
}