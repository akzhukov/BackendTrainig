using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfApp.ViewModels;

namespace WpfApp.Services
{
    public class AutorizeService
    {
        private readonly string serviceUrl = "https://localhost:44377/";
        private readonly MainWindowViewModel mainWindowViewModel;

        public AutorizeService(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }

        public string Login { get; set; }
        public string Token { get; set; }
        public async Task<bool> IsAuthorized()
        {
            using HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");

            var response = await httpClient.GetAsync($"{serviceUrl}api/User/current");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;

        }

        public async Task<bool> Authorize(string login, string password)
        {
            try
            {
                Login = login;
                using HttpClient httpClient = new HttpClient();
                var loginDataJson = JsonConvert.SerializeObject(new
                {
                    login = login,
                    password = password
                });
                var content = new StringContent(loginDataJson, Encoding.UTF8, "application/json");
                var responce = await httpClient.PostAsync($"{serviceUrl}api/User/auth", content);
                if (responce.IsSuccessStatusCode)
                {
                    Token = await responce.Content.ReadAsStringAsync();
                    mainWindowViewModel.NavigateToDashboardView();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
