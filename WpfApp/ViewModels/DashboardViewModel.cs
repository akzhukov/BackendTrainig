using Shared.DTOs;
using Shared.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp.Helpers;
using WpfApp.Services;

namespace WpfApp.ViewModels
{

    public class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly EventsService dataService;
        private readonly int recordsOnPage = 100;
        private readonly AutorizeService autorizeService;
        private int currentPageNum = 1;
        public int CurrentPageNum
        {
            get
            {
                return currentPageNum;
            }
            set
            {
                currentPageNum = value;
                OnPropertyChanged(nameof(CurrentPageNum));
            }
        }

        public DashboardViewModel(AutorizeService autorizeService, EventsService dataService)
        {
            this.autorizeService = autorizeService;
            this.dataService = dataService;
        }

        #region fields

        private ObservableCollection<EventDto> events = new ObservableCollection<EventDto>();

        public ObservableCollection<EventDto> Events
        {
            get => events;
            set
            {
                events = value;
                OnPropertyChanged(nameof(Events));
            }
        }

        #endregion

        #region commands

        private int GetSkipNum() => ((CurrentPageNum - 1) * recordsOnPage);

        public ICommand ShowEventsCommand => new RelayCommand(async obj =>
            await ShowEventsCommandExecuted(1, recordsOnPage, GetSkipNum(), autorizeService.Token));
        public ICommand ShowNextPageEventsCommand => new RelayCommand(async obj =>
        {
            CurrentPageNum++;
            var result = await ShowEventsCommandExecuted(1, recordsOnPage, GetSkipNum(), autorizeService.Token);
            if (result == 0)
            {
                CurrentPageNum--;
            }
        });
        public ICommand ShowPrevPageEventsEventsCommand => new RelayCommand(async obj =>
        {
            if (CurrentPageNum == 1)
            {
                return;
            }
            CurrentPageNum--;
            await ShowEventsCommandExecuted(1, recordsOnPage, GetSkipNum(), autorizeService.Token);
        });

        public ICommand BreakUiCommand => new RelayCommand(BreakUiExecuted);



        private void BreakUiExecuted(object obj)
        {
            Task.Delay(TimeSpan.FromSeconds(3)).Wait();
        }

        private async Task<int> ShowEventsCommandExecuted(int unitID, int take, int skip, string token)
        {

            var collection = await dataService.GetEvents(unitID, take, skip, token);
            if (collection.Count == 0)
            {
                return 0;
            }
            Events = new ObservableCollection<EventDto>(collection);
            return collection.Count;

        }

        #endregion

        #region OnPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


    }
}