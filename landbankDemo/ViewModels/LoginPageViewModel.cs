using System;
using System.Windows.Input;
using Acr.UserDialogs;
using landbankDemo.ViewModels.Base;
using landbankDemo.Views;
using Plugin.Fingerprint;
using Prism.Commands;
using Prism.Navigation;
using landbankDemo.Models;
using System.IO;
using SQLite;
using landbankDemo.Tables;
using Xamarin.Forms;

namespace landbankDemo.ViewModels
{
    public class LoginPageViewModel : BaseNavigationViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private Model model = new Model();
        public LoginPageViewModel(INavigationService navigationService, IUserDialogs userDialogs) : base(navigationService)
        {
            _userDialogs = userDialogs;
            LoginCommand = new DelegateCommand(Login);
            RegisterCommand = new DelegateCommand(Register);
            FingerprintCommand = new DelegateCommand(Fingerprint);
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

            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myquery = db.Table<RegUserTable>().Where(u => u.UserName.Equals(User) && u.Password.Equals(Password)).FirstOrDefault();
            if (myquery != null)
            {
                NavigationService.NavigateAsync(nameof(HomePage));
            }
            else
            {
                    _userDialogs.Alert("Wrong Username or Password ");
                return;
             }
        }
        public void Register()
        {
            NavigationService.NavigateAsync(nameof(RegisterPage));
        }
        public async void Fingerprint()
        {
            var cancellationToken = new System.Threading.CancellationToken();
            var scanResult = await CrossFingerprint.Current.AuthenticateAsync("Show what you have", cancellationToken);
            if(scanResult.Authenticated)
            {
                model.Result = "Authenticated via fingerprint";
                await NavigationService.NavigateAsync(nameof(HomePage));
            }
            else
            {
                model.Result = "Authentication failed";
                return;
            }
        }


        #endregion
    }
}