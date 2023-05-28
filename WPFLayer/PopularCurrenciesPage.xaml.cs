using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private async void PopularCurrenciesPage_Loaded(object sender, RoutedEventArgs e)
        {
            List<CurrencyModel> popularCurrencies = await GetPopularCurrencies();

            DataContext = popularCurrencies;
        }

        private async Task<List<CurrencyModel>> GetPopularCurrencies()
        {
            var response = await _httpClient.GetAsync("simple/supported_vs_currencies");
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                List<string> currencyCodes = JsonConvert.DeserializeObject<List<string>>(responseContent);
                List<CurrencyModel> currencies = currencyCodes.Take(10).Select(code => new CurrencyModel { Name = code }).ToList();
                return currencies;
            }
            else
            {
                MessageBox.Show("Error with response");
                return null;
            }
        }
    }
}