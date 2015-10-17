﻿using BevTra.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BevTra
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartupView : Page
    {
        public StartupView()
        {
            this.InitializeComponent();
        }

        public void Login()
        {
            var client = new Microsoft.WindowsAzure.MobileServices.MobileServiceClient(Constants.AccountUrl, Constants.AccountKey);
            client.LoginAsync(MobileServiceAuthenticationProvider.Facebook); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }
    }
}
