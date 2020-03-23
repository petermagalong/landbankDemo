using System;
using System.ComponentModel;
using Prism.Navigation;

namespace landbankDemo.ViewModels.Base
{
    
    public class BaseNavigationViewModel : BaseViewModel ,INavigationAware
    {
            protected INavigationService NavigationService;
            public BaseNavigationViewModel(INavigationService navigationService)
            {
                NavigationService = navigationService;
            }

    }
 
}
