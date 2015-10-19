using BevTra.Core.ViewModels;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace BevTra.iOS
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            SimpleIoc.Default.Register<AboutViewModel>();
            SimpleIoc.Default.Register<AddNewViewModel>();
            SimpleIoc.Default.Register<HistoryViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<StartupViewModel>();
        }

        public AboutViewModel About { get { return SimpleIoc.Default.GetInstance<AboutViewModel>(); } }
        public AddNewViewModel AddNew { get { return SimpleIoc.Default.GetInstance<AddNewViewModel>(); } }
        public HomeViewModel Home { get { return SimpleIoc.Default.GetInstance<HomeViewModel>(); } }
        public SettingsViewModel Settings { get { return SimpleIoc.Default.GetInstance<SettingsViewModel>(); } }
        public StartupViewModel Startup { get { return SimpleIoc.Default.GetInstance<StartupViewModel>();  } }
        
    }
}
