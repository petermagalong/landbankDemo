using System;
using Prism.Navigation;

namespace landbankDemo.ViewModels.Base
{
    
    public class BaseNavigationViewModel : BaseViewModel
    {
            protected INavigationService NavigationService;
            public BaseNavigationViewModel(INavigationService navigationService)
            {
                NavigationService = navigationService;
            }

    }
 
}
