using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
using System.Globalization;
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
        //private string _currencySymbol;
        //private string _date;

        public FindCurrency(HttpClient httpClient)
        {
            InitializeComponent();
            _httpClient = httpClient;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var currencyName = CurrencySymbolTextBox.Text.Trim();
            var date = DateTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(currencyName) && !string.IsNullOrEmpty(date))
            {
                await LoadCurrencyDetails(currencyName, date);
            }
        }

        private async Task LoadCurrencyDetails(string currencySymbol, string date)
        {
            try
            {
                CurrencyDetailsModel currencyDetails = await GetCurrencyDetails(currencySymbol, date);

                NameLabel.Text = currencyDetails.Name;
                SymbolLabel.Text = currencyDetails.Symbol;
                PriceLabel.Text = $"${currencyDetails.PriceInUSD}";
                VolumeLabel.Text = $"${currencyDetails.TotalVolume}";
                PriceChangeLabel.Text = $"${currencyDetails.PriceChange}";
                DateLabel.Text = date;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("invalid data");
            }
        }

        private async Task<CurrencyDetailsModel> GetCurrencyDetails(string currencySymbol, string date)
        {
            var url = $"coins/{currencySymbol}/history?date={date}";
            var response = await _httpClient.GetAsync(url);

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
                 throw new ArgumentException("Failed response");
            }
        }
    }
}