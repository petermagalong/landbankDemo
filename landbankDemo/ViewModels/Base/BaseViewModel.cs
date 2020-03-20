using System;
using Prism.Mvvm;
using Prism.Navigation;

namespace landbankDemo.ViewModels.Base
{
    public class BaseViewModel : BindableBase, INavigationAware, IDestructible
    {
        public void Destroy()
        {
            
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            
        }
    }
}
