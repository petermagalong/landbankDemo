using System;
using System.Windows.Input;
using Acr.UserDialogs;
using landbankDemo.ViewModels.Base;
using landbankDemo.Views;
using Prism.Commands;
using Prism.Navigation;

namespace landbankDemo.ViewModels
{
    public class LoginPageViewModel : BaseNavigationViewModel
    {
        private readonly IUserDialogs _userDialogs;
        public LoginPageViewModel(INavigationService navigationService, IUserDialogs userDialogs) : base(navigationService)
        {
            _userDialogs = userDialogs;
            LoginCommand = new DelegateCommand(Login);
            RegisterCommand = new DelegateCommand(Register);
        }
        #region Bindable Commands
        public ICommand LoginCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }
        public ICommand FingerprintCommand { get; set; }
        #endregion

        #region Bindable Properties

        private string _user;
        public string User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        #endregion

        #region Method
        public void Login()
        {
            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(User))
            {
                _userDialogs.Alert("Please insert username or password ");
                return;
            }
            else if (User == "admin" && Password == "admin")
            {
                NavigationService.NavigateAsync(nameof(HomePage));
            }
        }
        public void Register()
        {
            NavigationService.NavigateAsync(nameof(RegisterPage));
        }


        #endregion
    }
}