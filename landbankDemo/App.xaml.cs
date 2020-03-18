using System;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;
using landbankDemo.Views;
using Plugin.Fingerprint;

namespace landbankDemo
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }


        public App(IPlatformInitializer platformInitializer) : base(platformInitializer){ }

        public T Resolve<T>() => Container.Resolve<T>();

        public INavigationService NavigationSvc => NavigationService;

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationSvc.NavigateAsync($"app:///{nameof(LoginPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(CrossFingerprint.Current);
            containerRegistry.RegisterInstance(UserDialogs.Instance);
            containerRegistry.RegisterForNavigation<RegisterPage>();
            containerRegistry.RegisterForNavigation<LoginPage>();
            containerRegistry.RegisterForNavigation<HomePage>();
        }
    }
}
