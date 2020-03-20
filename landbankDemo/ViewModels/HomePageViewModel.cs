using System;
using System.IO;
using System.Windows.Input;
using Acr.UserDialogs;
using landbankDemo.Tables;
using landbankDemo.ViewModels.Base;
using Prism.Commands;
using Prism.Common;
using Prism.Navigation;
using SQLite;

namespace landbankDemo.ViewModels
{
    public class HomePageViewModel : BaseNavigationViewModel 
    {
        public HomePageViewModel(INavigationService navigationService, IUserDialogs userDialogs) : base(navigationService)
        {
            CheckInCommand = new DelegateCommand(Checkin);
            DateNowCommand = DateTime.Now;
        }
         
        
        
        #region BindableCommands
        public ICommand LunchInCommand { get; private set; }
        public ICommand LunchOutCommand { get; private set; }
        public ICommand CheckOutCommand { get; private set; }
        public ICommand CheckInCommand { get; private set; }
        public DateTime DateNowCommand { get; }

        //DateNowCommand
        #endregion

        #region bindableProperties
        private string _scanNumber;
        public string ScanNumber
        {
            get => _scanNumber;
            set => SetProperty(ref _scanNumber, value);
        }
        private string _location;
        public string Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }
        private DateTime _dateLogs;
        public DateTime DateLogs
        {
            get => _dateLogs;
            set => SetProperty(ref _dateLogs, value);
        }
        private string _fullName;
        public string FullNamelog
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }
        #endregion
        #region Method
        public void Checkin()
        {
            timelogs("CheckIn");
        }
        public void Checkout()
        {
            timelogs("CheckOut");
        }
        public void Lunchin()
        {
            timelogs("LunchIn");
        }
        public void LunchOut()
        {
            timelogs("LunchOut");
        }
        public void timelogs(string checktype)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            db.CreateTable<TimeUserTable>();

            DateTime ngayon = DateTime.Now;
            var item = new TimeUserTable()
            {

                FullName = "tian",
                Checklog = checktype,
                Datelog = ngayon,
                Location = Location,
                ScanNumber = ScanNumber
            };
            db.Insert(item);
        }
        #endregion

    }
}
