using System;
using Prism.Mvvm;
using Prism.Navigation;

namespace landbankDemo.ViewModels.Base
{
    public class BaseViewModel : BindableBase, INavigationAware, IDestructible
    {
        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
