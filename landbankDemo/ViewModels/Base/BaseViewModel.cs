using System;
using System.ComponentModel;
using Prism.Mvvm;
using Prism.Navigation;

namespace landbankDemo.ViewModels.Base
{
    public class BaseViewModel : BindableBase, INavigationAware, IDestructible
    {
        public virtual void Destroy()
        {
            
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            
        }
    }
}
