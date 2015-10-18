using BevTra.Core.DeviceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

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

        public Task<T> GetKVPValueAsync<T>(string key, T defaultValue) where T : class
        {
            var tcs = new TaskCompletionSource<T>();
            Task.Run(() =>
            {
                if (SettingsValues.ContainsKey(key))
                {
                    var value = SettingsValues[key];

                    tcs.SetResult(Convert.ChangeType(value, typeof(T)) as T);
                }
                else
                    tcs.SetResult(defaultValue);
            });

            return tcs.Task;
        }

        public async Task PutKVPValueAsync<T>(string key, T value)where T : class
        {
            await Task.Run(() =>
            {
                if (SettingsValues.ContainsKey(key))
                    SettingsValues.Remove(key);

                SettingsValues.Add(key, value);
            });
        }

        public Task ShowPopupAsync(string title, string message)
        {
            throw new NotImplementedException();
        }
        
    }
}
