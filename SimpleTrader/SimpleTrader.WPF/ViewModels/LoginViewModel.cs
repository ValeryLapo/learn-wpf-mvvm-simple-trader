﻿using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand ViewRegisterCommand { get; }

        public LoginViewModel(IAuthenticator authentication, IRenavigator loginRenavigator, IRenavigator registerRenavigator)
        {
            LoginCommand = new LoginCommand(this, authentication, loginRenavigator);
            ErrorMessageViewModel = new MessageViewModel();
            ViewRegisterCommand = new RenavigateCommand(registerRenavigator);

        }

        public MessageViewModel ErrorMessageViewModel { get; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }


    }
}
