using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleTrader.WPF.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        //Using a DependencyProperty as the backing store for LoginCommand. this enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoginCommandProperty =
            DependencyProperty.Register("LoginCommand", typeof(ICommand), typeof(LoginView), new PropertyMetadata(null));
        public ICommand LoginCommand
        {
            get => (ICommand)GetValue(LoginCommandProperty);
            set => SetValue(LoginCommandProperty, value);
        }


        public LoginView()
        {
            InitializeComponent();
        }

        private void Login_CLick(object sender, RoutedEventArgs e)
        {
            string password = pbPassword.Password;
            if (LoginCommand != null)
            {
                LoginCommand.Execute(password);
            }
        }
    }
}
