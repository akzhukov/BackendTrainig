using Newtonsoft.Json;
using Shared.DTOs;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WpfApp.ViewModels;

namespace WpfApp.Services
{
    public class EventsService
    {
        private readonly string serviceUrl = "https://localhost:44377/";
        private readonly MainWindowViewModel mainWindowViewModel;

        public EventsService(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }

        public async Task<IList<EventDto>> GetEvents(int unitId, int take, int skip, string token)
        {
            using HttpClient httpClient = new HttpClient();
            var unitsIdUrl = $"{serviceUrl}api/events?unitId={unitId}&take={take}&skip={skip}";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseGet = await httpClient.GetAsync(unitsIdUrl);
            if (!responseGet.IsSuccessStatusCode)
            {
                mainWindowViewModel.NavigateToLoginView();
                return new List<EventDto>();
            }
            var result = await responseGet.Content.ReadAsStringAsync();
            var events = JsonConvert.DeserializeObject<IList<EventDto>>(result);
            return events;
        }

        public async Task UpdateEvent(EventDto eventDto, string token)
        {
            using HttpClient httpClient = new HttpClient();
            var url = $"{serviceUrl}api/events/{eventDto.Id}";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonConvert.SerializeObject(eventDto), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(url, content);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                mainWindowViewModel.NavigateToLoginView();
            }
        }
    }
}