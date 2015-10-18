using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BevTra.Core
{
    public class BevTraViewModel : ViewModelBase
    {
        public IDataContext DataContext
        {
            get { return GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.GetInstance<IDataContext>(); }
        }

        public GalaSoft.MvvmLight.Views.INavigationService Navigation
        {
            get { return GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.GetInstance<GalaSoft.MvvmLight.Views.INavigationService>(); }
        }

        public bool IsBusy
        {
            get;
            set;
        }

        public Task<bool> PerformNetworkAction(Func<Task> action, String busyMessage = "")
        {
            var tcs = new TaskCompletionSource<bool>();
            Task.Run(async () =>
            {
               IsBusy = true;
               bool success = true;
               var errorMessage = String.Empty;
               try
               {
                   await action();
               }
               catch (Exception ex)
               {
                   errorMessage = ex.Message;
                   success = false;
               }
               finally
               {
                   IsBusy = false;
               }

                if (!success)
                    await DeviceServices.PlatformServices.Current.ShowPopupAsync(LanguageResources.AppName, errorMessage);

                tcs.SetResult(success);
            });

            return tcs.Task;
        }
    }
}
