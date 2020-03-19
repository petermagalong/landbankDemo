using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using landbankDemo.Tables;
using landbankDemo.ViewModels.Base;
using landbankDemo.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using SQLite;
using Xamarin.Forms;

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
        public string UserName
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

        private string _scanNumber;
        public string ScanNumber
        {
            get => _scanNumber;
            set => SetProperty(ref _scanNumber, value);
        }
        private string _employeeNumber;
        public string EmployeeNumber
        {
            get => _employeeNumber;
            set => SetProperty(ref _employeeNumber, value);
        }
        private string _fisrtName;
        public string FirstName
        {
            get => _fisrtName;
            set => SetProperty(ref _fisrtName, value);
        }
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }
        private string _suffix;
        public string Suffix
        {
            get => _suffix;
            set => SetProperty(ref _suffix, value);
        }

        #endregion

        #region Method
        public void Register()
        {
            if (string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(Password) && string.IsNullOrEmpty(ConfirmPassword))
            {
                _userDialogs.Alert("Please input all fields");
                return;
            }
            else if (UserName != "" && Password != "" && ConfirmPassword != "")
            {
                //_userDialogs.Alert("Succesfully Registered");
                var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
                var db = new SQLiteConnection(dbpath);
                db.CreateTable<RegUserTable>();

                var item = new RegUserTable()
                {
                    UserName = UserName,
                    Password = Password,
                    ScanNumber = ScanNumber,
                    EmployeeNumber = EmployeeNumber,
                    FirstName = FirstName,
                    LastName = LastName,
                    Suffix = Suffix
                };
                db.Insert(item);
                Device.BeginInvokeOnMainThread(async () =>
                {

                    await NavigationService.NavigateAsync(nameof(LoginPage));
                });
                NavigationService.NavigateAsync(nameof(LoginPage));
            }
            

        }

        private Task DisplayAlert(string v1, string v2, string v3, string v4)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
