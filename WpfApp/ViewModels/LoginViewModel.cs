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
    public class LoginViewModel
    {
        private string login;
        private readonly AutorizeService autorizeService;

        public LoginViewModel(AutorizeService autorizeService)
        {
            this.autorizeService = autorizeService;
        }

        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand AuthorizeCommand => new RelayCommand(async obj =>
             await autorizeService.Authorize(Login, Password));

        #region OnPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
