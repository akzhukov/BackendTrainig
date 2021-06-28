using Shared.DTOs;
using Shared.Models;
using System.Windows.Controls;
using WpfApp.Services;

namespace WpfApp.ModelViews
{
    public partial class DashboardView : UserControl
    {
        private readonly AutorizeService autorizeService;
        private readonly EventsService eventsService;

        public DashboardView(AutorizeService autorizeService, EventsService eventsService)
        {
            InitializeComponent();
            this.autorizeService = autorizeService;
            this.eventsService = eventsService;
        }
        private async void OnCellsChanged(object sender, DataGridRowEditEndingEventArgs e)
        {
            var updatedEvent = e.Row.Item as EventDto;
            await eventsService.UpdateEvent(updatedEvent, autorizeService.Token);
        }
    }
}