using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Acr.UserDialogs;
using landbankDemo.Tables;
using landbankDemo.ViewModels.Base;
using Prism.Commands;
using Prism.Common;
using Prism.Navigation;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace landbankDemo.ViewModels
{

    public class HomePageViewModel : BaseNavigationViewModel
    {
        private Location mylocation;
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            FnameCommand = parameters.GetValue<string>("User");
            ScanNumber = parameters.GetValue<string>("ScanNum");
        }
        private readonly IUserDialogs _userDialogs;
        public HomePageViewModel(INavigationService navigationService, IUserDialogs userDialogs) : base(navigationService)
        {
            _userDialogs = userDialogs;
            DateNowCommand = DateTime.Now;
            daytodayDisplay = DateNowCommand.ToString("dddd") + " " + DateTime.Now.ToString("MM dd,yyyy");
            Geoloc();
            placemark();
            displayTime = DateTime.Now;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                displayTime = DateTime.Now;
                return true;
            });

            CheckInCommand = new DelegateCommand(Checkin);
            LunchOutCommand = new DelegateCommand(LunchOut);
            LunchInCommand = new DelegateCommand(Lunchin);
            CheckOutCommand = new DelegateCommand(Checkout);
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

        
        private DateTime _displayTime;
        public DateTime displayTime
        {
            get { return _displayTime; }
            
                set { SetProperty(ref _displayTime, value); }
        }
        private string _daytodayDisplay;
        public string daytodayDisplay
        {
            get => _daytodayDisplay;
            set => SetProperty(ref _daytodayDisplay, value);
        }
        private string _fname;
        public string FnameCommand
        {
            get => _fname;
            set => SetProperty(ref _fname, value);
        }
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
        private double _loc;
        public double Loc
        {
            get => _loc;
            set => SetProperty(ref _loc, value);
        }
        private string _geoaddress;
        public string geoaddress
        {
            get => _geoaddress;
            set => SetProperty(ref _geoaddress, value);

        }
        private string _geolocator;
        public string GeoLocator
        {
            get => _geolocator;
            set => SetProperty(ref _geolocator, value);
        }
        public string Geolocator { get; private set; }

        #endregion
        #region Method
        
        
        public void Checkin()
        {
            if (geoaddress != "Acme")
            {
                _userDialogs.Alert("invalid location! " + geoaddress);
                return;
            }
            else if (duplicateInput("CheckIn").Equals(false))
            {
                _userDialogs.Alert("Already CheckIn! ");
                return;
            }
            else
            {
                timelogs("CheckIn");
                _userDialogs.Alert("Succesfully TimeIn ! ");
                return;
            }
        }
        public void LunchOut()
        {
            if (geoaddress != "Acme")
            {
                _userDialogs.Alert("invalid location! " + geoaddress);
                return;
            }
            else if (duplicateInput("LunchOut").Equals(false))
            {
                _userDialogs.Alert("Already LunchOut! ");
                return;
            }
            else if(duplicateInput("LunchOut").Equals(true))
            {
                timelogs("LunchOut");
                _userDialogs.Alert("Succesfully LunchOut ! ");
                return;
            }

        }
        public void Lunchin()
        {
            if (geoaddress != "Acme")
            {
                _userDialogs.Alert("invalid location! " + geoaddress);
                return;
            }
            else if (duplicateInput("LunchIn").Equals(false))
            {
                _userDialogs.Alert("Already LunchIn! ");
                return;
            }
            else if(duplicateInput("LunchOut").Equals(true))
            {
                timelogs("LunchIn");
                _userDialogs.Alert("Succesfully BreakIn ! ");
                return;
            }
        }
        public void Checkout()
        {
            if (geoaddress != "Acme")
            {
                _userDialogs.Alert("invalid location! " + geoaddress);
                return;
            }
            else if(duplicateInput("CheckOut").Equals(false))
            {
                _userDialogs.Alert("Already checkout! ");
                return;
            }
            else if (duplicateInput("LunchOut").Equals(true))
            {
                timelogs("CheckOut");
                _userDialogs.Alert("Succesfully TimeOut ! ");
                return;
            }
        }
        
        
        public void timelogs(string checktype)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            db.CreateTable<TimeUserTable>();

            DateTime ngayon = DateTime.Now;
            var item = new TimeUserTable()
            {
                FullName = FnameCommand,
                Checklog = checktype,
                Datelog = DateNowCommand,
                Location = GeoLocator,
                ScanNumber = ScanNumber
            };
            db.Insert(item);

        }

        public bool duplicateInput(string checktype)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myquery = db.Table<TimeUserTable>().Where(u => u.ScanNumber.Equals(ScanNumber) && u.Checklog.Equals(checktype)).FirstOrDefault();
            if (myquery == null)
            {
                return true;
            }
            else
            {
                if (myquery.Datelog.ToString("MMddyyyy").Equals(DateNowCommand.ToString("MMddyyyy")) && myquery.Checklog.Equals(checktype))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            
        }

        #region Maps API

        public async void placemark()
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(mylocation.Latitude, mylocation.Longitude);

                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    var geocodeAddress =
                      //  "sublocaly " + placemark.SubLocality +
                      // "subtorofare  " + placemark.SubThoroughfare +
                      //"Thoroughfare " +
                      placemark.Thoroughfare;
                    geoaddress = geocodeAddress;
                }
            }
            catch (Exception exception)
            {

            }
        }
        public async void Geoloc()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");

                    GeoLocator =  location.Latitude + ", " + location.Longitude;
                    mylocation = location;

                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
            #endregion
        }

        #endregion

    }
}