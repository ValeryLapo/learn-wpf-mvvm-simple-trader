using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Navigators;
using System.Windows.Input;
using SimpleTrader.WPF.State.Authenticators;

namespace SimpleTrader.WPF.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

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

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public ICommand RegisterCommand { get; }
        public ICommand ViewLoginCommand { get; }

        public MessageViewModel ErrorMessageViewModel { get; }

        public RegisterViewModel(IAuthenticator authenticator, IRenavigator loginRenavigator,
            IRenavigator registerRenavigator)
        {
            ErrorMessageViewModel = new MessageViewModel();

            RegisterCommand = new RegisterCommand(this, authenticator, registerRenavigator);
            ViewLoginCommand = new RenavigateCommand(loginRenavigator);
        }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

    }
}
