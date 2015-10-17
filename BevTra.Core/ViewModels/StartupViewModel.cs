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
        }


        public async void DoLogin()
        {

            try
            {
                var token = new JObject();
                //token.Add("access_token", "2a6b77abf62af84bf2029ac303d4807a");
                var user = await DataContext.MobileServices.LoginAsync(Microsoft.WindowsAzure.MobileServices.MobileServiceAuthenticationProvider.MicrosoftAccount, token);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        public RelayCommand DoLoginCommand { get; private set; }
        public RelayCommand AboutCommand { get; private set; }
    }
}
