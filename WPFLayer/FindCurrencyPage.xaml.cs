using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFLayer.Models;

namespace WPFLayer
{
    public partial class FindCurrency : Page
    {
        private readonly HttpClient _httpClient;

        public FindCurrency(HttpClient httpClient)
        {
            InitializeComponent();
            _httpClient = httpClient;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var currencyName = CurrencySymbolTextBox.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(currencyName))
            {
                await LoadCurrencyDetails(currencyName);
            }
        }

        private async Task LoadCurrencyDetails(string currencySymbol)
        {
            try
            {
                CurrencyDetailsModel currencyDetails = await GetCurrencyDetails(currencySymbol);

                NameLabel.Text = currencyDetails.Name;
                SymbolLabel.Text = currencyDetails.Symbol;
                PriceLabel.Text = $"${currencyDetails.PriceInUSD}";
                VolumeLabel.Text = $"${currencyDetails.TotalVolume}";
                PriceChangeLabel.Text = $"${currencyDetails.PriceChange}";
                DateLabel.Text = DateTime.Today.ToString("dd-MM-yyyy");
            }
            catch (Exception ex)
            {
                MessageBox.Show("invalid data");
                throw new ArgumentException("invalid data");
            }
        }

        private async Task<CurrencyDetailsModel> GetCurrencyDetails(string currencySymbol)
        {
            var response = await _httpClient.GetAsync($"coins/{currencySymbol}/history?date={DateTime.Today.ToString("dd-MM-yyyy")}");

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                JObject responseData = JObject.Parse(responseContent);

                CurrencyDetailsModel currencyDetails = new CurrencyDetailsModel
                {
                    Name = responseData["name"].ToString(),
                    Symbol = responseData["symbol"].ToString(),
                    PriceInUSD = decimal.Parse(responseData["market_data"]["current_price"]["usd"].ToString()),
                    TotalVolume = decimal.Parse(responseData["market_data"]["total_volume"]["usd"].ToString()),
                    PriceChange = decimal.Parse(responseData["market_data"]["market_cap"]["usd"].ToString())
                };

                return currencyDetails;
            }
            else
            {
                MessageBox.Show("Incorrect name of currency, try again later");
                return null;
            }
        }
    }
}