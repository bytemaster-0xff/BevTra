using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace BevTra.Core.ViewModels
{
    public class StartupViewModel : BevTraViewModel
    {
        public StartupViewModel()
        {
            DoLoginCommand = new RelayCommand(() => DoLogin());
            ShowAboutScreenCommand = new RelayCommand(() => ShowAboutScreen());
            
            Init();
        }

        public async void Init()
        {
            var fbUserId = await DeviceServices.PlatformServices.Current.GetKVPValueAsync("USER_ID", String.Empty);
            if (!String.IsNullOrEmpty(fbUserId))
            {
                var userJSON = await DeviceServices.PlatformServices.Current.GetKVPValueAsync("USER_JSON", String.Empty);
                if (!String.IsNullOrEmpty(userJSON))
                {
                    DataContext.CurrentUser = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.User>(userJSON);
                    Navigation.NavigateTo(Views.Home);
                }
                else
                    Navigation.NavigateTo(Views.Settings);
            }
            else
            {
                var appReady = await DeviceServices.PlatformServices.Current.GetKVPValueAsync<bool>("IS_APP_READY", false);
                LoginVisible = true;
            }


        }

        public async void DoLogin()
        {

            try
            {
                var token = new JObject();
                //token.Add("access_token", "2a6b77abf62af84bf2029ac303d4807a");
                var user = await DataContext.MobileServices.LoginAsync(Microsoft.WindowsAzure.MobileServices.MobileServiceAuthenticationProvider.MicrosoftAccount, token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async void LoginSuccss(String userId)
        {
            await DeviceServices.PlatformServices.Current.PutKVPValueAsync("USER_ID", userId);

            Navigation.NavigateTo(Views.Home);
        }

        public void ShowAboutScreen()
        {
            Navigation.NavigateTo(Views.About);
        }

        private bool _loginVisible;
        public bool LoginVisible
        {
            get { return _loginVisible; }
            set { Set(ref _loginVisible, value); }
        }

        

        public RelayCommand DoLoginCommand { get; private set; }
        public RelayCommand ShowAboutScreenCommand { get; private set; }
    }
}
