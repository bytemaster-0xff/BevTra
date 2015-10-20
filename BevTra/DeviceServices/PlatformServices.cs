using BevTra.Core.DeviceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace BevTra.DeviceServices
{
    public class PlatformServices : IPlatformServices
    {
        public Task ConfirmAsync(string title, string message)
        {
            throw new NotImplementedException();
        }

        private IPropertySet SettingsValues
        {
            get { return Windows.Storage.ApplicationData.Current.RoamingSettings.Values; }
        }

        public Task<T> GetKVPValueAsync<T>(string key, T defaultValue)
        {
            var tcs = new TaskCompletionSource<T>();
            Task.Run(() =>
            {
                if (SettingsValues.ContainsKey(key))
                {
                    var value = SettingsValues[key];

                    tcs.SetResult((T)(Convert.ChangeType(value, typeof(T))));
                }
                else
                    tcs.SetResult(defaultValue);
            });

            return tcs.Task;
        }

        public async Task PutKVPValueAsync<T>(string key, T value)
        {
            await Task.Run(() =>
            {
                if (SettingsValues.ContainsKey(key))
                    SettingsValues.Remove(key);

                SettingsValues.Add(key, value);
            });
        }

        public async Task ShowPopupAsync(string title, string message)
        {
            var dlg = new MessageDialog(message, title);
            dlg.Commands.Add(new UICommand("OK"));
            await dlg.ShowAsync();
        }

        public async void RunOnMainThread(Action action)
        {
            await BevTra.App.TheApp.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
               {
                   action();
               });
        }
    }
}
