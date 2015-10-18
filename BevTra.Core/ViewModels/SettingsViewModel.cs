using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BevTra.Core.ViewModels
{
    public class SettingsViewModel : BevTraViewModel
    {
        public SettingsViewModel()
        {
            SaveUserInfoCommand = new RelayCommand(() => SaveUserInfo());
            if (DataContext.CurrentUser == null)
                DataContext.CurrentUser = new Models.User();

        }

        public async void SaveUserInfo()
        {
            var success = await PerformNetworkAction(async () =>
            {
                if (String.IsNullOrEmpty(DataContext.CurrentUser.Id))
                {
                    DataContext.CurrentUser.Id = Guid.NewGuid().ToString();
                    DataContext.CurrentUser.Created = DateTime.Now;
                    DataContext.CurrentUser.LastAcceessed = DateTime.Now;
                }

                var fbUserId = await DeviceServices.PlatformServices.Current.GetKVPValueAsync("USER_ID", String.Empty);
                DataContext.CurrentUser.AccountId = fbUserId;



                await DataContext.CreateUser(DataContext.CurrentUser);

                await DeviceServices.PlatformServices.Current.PutKVPValueAsync("IS_APP_READY", true);

                var userJSON = Newtonsoft.Json.JsonConvert.SerializeObject(DataContext.CurrentUser);

                await DeviceServices.PlatformServices.Current.PutKVPValueAsync("USER_JSON", userJSON);

                Navigation.NavigateTo(Views.Home);
            });
        }

        public RelayCommand SaveUserInfoCommand { get; private set; }
    }
}
