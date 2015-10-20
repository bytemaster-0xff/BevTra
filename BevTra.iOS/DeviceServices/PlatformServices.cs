using BevTra.Core.DeviceServices;
using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BevTra.iOS.DeviceServices
{
    public class PlatformServices : IPlatformServices
    {
        public Task ConfirmAsync(string title, string message)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetKVPValueAsync<T>(string key, T defaultValue) 
        {
            var tcs = new TaskCompletionSource<T>();

            Task.Run(() =>
            {
                var store = NSUbiquitousKeyValueStore.DefaultStore;
                store.Synchronize();

                var value = store.ValueForKey(new NSString(key));
                if (!store.ToDictionary().ContainsKey(NSObject.FromObject(key)))
                    tcs.SetResult(defaultValue);
                else
                    switch (Type.GetTypeCode(typeof(T)))
                    {
                        case TypeCode.String:
                            tcs.SetResult((T)(Convert.ChangeType(store.GetString(key), typeof(T)))); break;
                            break;
                        case TypeCode.Boolean:
                            tcs.SetResult((T)(Convert.ChangeType(store.GetBool(key), typeof(T)))); break;
                            break;
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                            tcs.SetResult((T)(Convert.ChangeType(store.GetLong(key), typeof(T)))); break;

                        case TypeCode.UInt16:
                        case TypeCode.UInt32:
                        case TypeCode.UInt64:
                            tcs.SetResult((T)(Convert.ChangeType(store.GetLong(key), typeof(T)))); break;

                        case TypeCode.Single:
                        case TypeCode.Double:
                            tcs.SetResult((T)(Convert.ChangeType(store.GetDouble(key), typeof(T)))); break;

                        case TypeCode.DateTime:
                            {
                                var ticks = store.GetLong(key);
                                tcs.SetResult((T)Convert.ChangeType(new DateTime(ticks), typeof(T)));
                            }
                            break;
                    }
            });

            return tcs.Task;
        }

        public async Task PutKVPValueAsync<T>(string key, T value) 
        {
            await Task.Run(() =>
            {
                var store = NSUbiquitousKeyValueStore.DefaultStore;
                store.Synchronize();

                if (!store.ToDictionary().ContainsKey(NSObject.FromObject(key)))
                    store.Remove(key);

                switch (Type.GetTypeCode(typeof(T)))
                {
                    case TypeCode.String: store.SetString(key, Convert.ToString(value)); break;
                    case TypeCode.Boolean: store.SetBool(key, Convert.ToBoolean(value)); break;
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                        store.SetLong(key,Convert.ToInt64(value)); break;

                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                        store.SetLong(key, Convert.ToInt64(value)); break;

                    case TypeCode.Single:
                    case TypeCode.Double:
                        store.SetDouble(key, Convert.ToDouble(value)); break;

                    case TypeCode.DateTime:
                        {
                            var ticks = Convert.ToDateTime(value).Ticks;
                            store.SetLong(key, ticks);
                        }
                        break;
                }

            });
        }

        public void RunOnMainThread(Action action)
        {
            
        }

        public Task ShowPopupAsync(string title, string message)
        {
            throw new NotImplementedException();
        }
    }
}
