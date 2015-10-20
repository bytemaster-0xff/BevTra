using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Helpers;
using System.Diagnostics;

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

        public GalaSoft.MvvmLight.Views.IDialogService Dialogs
        {
            get { return GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.GetInstance<GalaSoft.MvvmLight.Views.IDialogService>(); }
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
                    Debug.WriteLine(ex.Message);
                   success = false;
               }
               finally
               {
                   IsBusy = false;
               }

                if (!success)
                {
                    DeviceServices.PlatformServices.Current.RunOnMainThread(async () =>
                    {
                        await Dialogs.ShowMessage(LanguageResources.AppName, errorMessage);
                    });
                }

                tcs.SetResult(success);
            });

            return tcs.Task;
        }
    }
}
