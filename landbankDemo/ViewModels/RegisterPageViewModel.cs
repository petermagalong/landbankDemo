using System;
using System.Windows.Input;
using Acr.UserDialogs;
using landbankDemo.ViewModels.Base;
using landbankDemo.Views;
using Prism.Commands;
using Prism.Navigation;

namespace landbankDemo.ViewModels
{
    public class RegisterPageViewModel : BaseNavigationViewModel
    {
        private readonly IUserDialogs _userDialogs;
        public RegisterPageViewModel(INavigationService navigationService, IUserDialogs userDialogs) : base(navigationService)
        {
            _userDialogs = userDialogs;
            RegisterCommand = new DelegateCommand(Register);
        }
        #region Bindable Command
        public ICommand RegisterCommand { get; private set; }
        #endregion

        #region Bindable Propperties
        private string _username;
        public string User
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }
        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }
        #endregion

        #region Method
        public void Register()
        {
            if (string.IsNullOrEmpty(User) && string.IsNullOrEmpty(Password) && string.IsNullOrEmpty(ConfirmPassword))
            {
                _userDialogs.Alert("Please input all fields");
                return;
            }
            else if (User != "" && Password != "" && ConfirmPassword != "")
            {
                //_userDialogs.Alert("Succesfully Registered");
                NavigationService.NavigateAsync(nameof(LoginPage));
            }

        }

        #endregion
    }
}
