using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BevTra.Core.ViewModels
{
    public class HomeViewModel : BevTraViewModel
    {

        public HomeViewModel()
        {
            AddNewCommand = new RelayCommand(() => AddNew());

            TwentyFivePercentCommand = new RelayCommand(() => UpdateBeverageStatus(25));
            FiftyPercentCommand = new RelayCommand(() => UpdateBeverageStatus(50));
            SeventyFivePercentCommand = new RelayCommand(() => UpdateBeverageStatus(75));
            FinishFluidCommand = new RelayCommand(() => UpdateBeverageStatus(100));
            CancelFluidCommand = new RelayCommand(() => CancelBeverage());
            Init();
        }

        private async void Init()
        {
            var currentFluidJSON = await DeviceServices.PlatformServices.Current.GetKVPValueAsync("CURRENT_FLUID", String.Empty);
            if (!String.IsNullOrEmpty(currentFluidJSON))
                DataContext.CurrentFluid = JsonConvert.DeserializeObject<Models.Fluid>(currentFluidJSON);
            else
                DataContext.CurrentFluid = null;

            RaisePropertyChanged(() => CurrentFluid);
            RaisePropertyChanged(() => IsAddNewVisible);
            RaisePropertyChanged(() => IsUpdateExistingVisible);


            DataContext.CurrentUser = await DataContext.Get(DataContext.CurrentUser.AccountId);
        }

        public void AddNew()
        {
            Navigation.NavigateTo(Views.AddNew);
        }

        public Models.Fluid CurrentFluid
        {
            get { return DataContext.CurrentFluid; }
        }

        public bool IsAddNewVisible
        {
            get { return DataContext.CurrentFluid == null; }
        }

        public bool IsUpdateExistingVisible
        {
            get { return DataContext.CurrentFluid != null; }
        }

        private async void UpdateBeverageStatus(int percentFinished)
        {
            await PerformNetworkAction(async () =>
            {
                DataContext.CurrentFluid.PercentDrank = percentFinished;
                if (percentFinished == 100)
                {
                    DataContext.CurrentFluid.Status = "Finished";
                    DataContext.CurrentFluid.End = DateTime.Now;
                    await DataContext.UpdateFluid(DataContext.CurrentFluid);
                    DataContext.CurrentFluid = null;
                    await DeviceServices.PlatformServices.Current.PutKVPValueAsync("CURRENT_FLUID", String.Empty);
                }
                else
                {
                    await DataContext.UpdateFluid(DataContext.CurrentFluid);
                    var currentFluidJSON = JsonConvert.SerializeObject(DataContext.CurrentFluid);
                    await DeviceServices.PlatformServices.Current.PutKVPValueAsync("CURRENT_FLUID", currentFluidJSON);
                }

            });

            RaisePropertyChanged(() => IsAddNewVisible);
            RaisePropertyChanged(() => IsUpdateExistingVisible);
            RaisePropertyChanged(() => CurrentFluid);
        }

        private async void CancelBeverage()
        {
            await PerformNetworkAction(async () =>
            {
                DataContext.CurrentFluid.Status = "Not Finished";
                DataContext.CurrentFluid.End = DateTime.Now;
                await DataContext.UpdateFluid(DataContext.CurrentFluid);
                DataContext.CurrentFluid = null;
            });

            RaisePropertyChanged(() => IsAddNewVisible);
            RaisePropertyChanged(() => IsUpdateExistingVisible);

        }

        private int _ouncesToday;
        public int OuncesToday
        {
            get { return _ouncesToday; }
            set { Set(ref _ouncesToday, value); }
        }

        public RelayCommand TwentyFivePercentCommand { get; private set; }
        public RelayCommand FiftyPercentCommand { get; private set; }
        public RelayCommand SeventyFivePercentCommand { get; private set; }
        public RelayCommand FinishFluidCommand { get; private set; }
        public RelayCommand CancelFluidCommand { get; private set; }
        public RelayCommand AddNewCommand { get; private set; }
    }
}
