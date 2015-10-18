using BevTra.Core;
using BevTra.Core.DeviceServices;
using BevTra.Core.ViewModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BevTra
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var nav = new NavigationService();
            nav.Configure(Views.About, typeof(AboutView));
            nav.Configure(Views.AddNew, typeof(AddNewView));
            nav.Configure(Views.History, typeof(HistoryView));
            nav.Configure(Views.Home, typeof(HomeView));
            nav.Configure(Views.Settings, typeof(SettingsView));
            nav.Configure(Views.Startup, typeof(StartupView));

            SimpleIoc.Default.Register<INavigationService>(() => nav);

            SimpleIoc.Default.Register<IPlatformServices, DeviceServices.PlatformServices>();

            SimpleIoc.Default.Register<IDialogService, DialogService>();

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataContext>(() =>  new DesignDataContext());
            }
            else
            {
                SimpleIoc.Default.Register<IDataContext>(() => new DataContext());
            }

            SimpleIoc.Default.GetInstance<IDataContext>().Init();

            SimpleIoc.Default.Register<AboutViewModel>();
            SimpleIoc.Default.Register<AddNewViewModel>();
            SimpleIoc.Default.Register<HistoryView>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<StartupViewModel>();
        }

        public AboutViewModel AboutVM { get { return ServiceLocator.Current.GetInstance<AboutViewModel>(); } }
        public AddNewViewModel AddNewVM { get { return ServiceLocator.Current.GetInstance<AddNewViewModel>(); } }
        public HistoryViewModel HistoryVM { get { return ServiceLocator.Current.GetInstance<HistoryViewModel>(); } }
        public HomeViewModel HomeVM { get { return ServiceLocator.Current.GetInstance<HomeViewModel>(); } }
        public SettingsViewModel SettingsVM { get { return ServiceLocator.Current.GetInstance<SettingsViewModel>(); } }
        public StartupViewModel StartupVM { get { return ServiceLocator.Current.GetInstance<StartupViewModel>(); } }
    }
}
