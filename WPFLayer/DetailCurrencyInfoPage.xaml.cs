using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using WPFLayer.Models;

namespace WPFLayer
{
    public partial class DetailCurrencyInfoPage : Page
    {
        private readonly HttpClient _httpClient;
        public DetailCurrencyInfoPage(HttpClient httpClient)
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
                MarketLinks.Inlines.Clear(); // Очистка содержимого TextBlock

                foreach (string link in currencyDetails.MarketLinks)
                {
                    if (!string.IsNullOrWhiteSpace(link))
                    {
                        Hyperlink hyperlink = new Hyperlink();
                        hyperlink.NavigateUri = new Uri(link);
                        hyperlink.Inlines.Add(link);
                        hyperlink.RequestNavigate += Hyperlink_RequestNavigate;
                        MarketLinks.Inlines.Add(hyperlink);
                        MarketLinks.Inlines.Add(new Run("   ")); // Добавить пробел между ссылками
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("invalid data");
                throw new ArgumentException("invalid data");
            }
        }

        private async Task<CurrencyDetailsModel> GetCurrencyDetails(string currencySymbol)
        {
            var response = await _httpClient.GetAsync($"coins/{currencySymbol}");
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                JObject responseData = JObject.Parse(responseContent);

                CurrencyDetailsModel currencyDetails = new CurrencyDetailsModel
                {
                    Name = responseData["name"].ToString(),
                    Symbol = responseData["symbol"].ToString(),
                    PriceInUSD = decimal.Parse(responseData["market_data"]["current_price"]["usd"].ToString()),
                    MarketLinks = new List<string>()
                };

                JArray blockchainSiteArray = responseData["links"]["blockchain_site"] as JArray;
                if (blockchainSiteArray != null)
                {
                    foreach (var link in blockchainSiteArray)
                    {
                        string linkString = link.ToString();
                        if (!string.IsNullOrEmpty(linkString))
                        {
                            currencyDetails.MarketLinks.Add(linkString);
                        }
                    }
                }

                return currencyDetails;
            }
            else
            {
                MessageBox.Show("Incorrect name of currency, try again later");
                return null;
            }
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            string url = e.Uri.AbsoluteUri;
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
            e.Handled = true;
        }
    }
}