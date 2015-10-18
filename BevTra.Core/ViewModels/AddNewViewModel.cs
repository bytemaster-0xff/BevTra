using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BevTra.Core.ViewModels
{
    public class AddNewViewModel : BevTraViewModel
    {
        public AddNewViewModel()
        {
            SaveCommand = new GalaSoft.MvvmLight.Command.RelayCommand(() => Save());
            CancelCommand = new GalaSoft.MvvmLight.Command.RelayCommand(() => Cancel());

            SetSmallCommand = new RelayCommand(() => Ounces = 8);
            SetMediumCommand = new RelayCommand(() => Ounces = 12);
            SetLargeCommand = new RelayCommand(() => Ounces = 16);

            SetWaterCommand = new RelayCommand(() => Contents = "Water");
            SetBlendedCommand = new RelayCommand(() => Contents = "Blend");
            SetSportsDrinkCommand = new RelayCommand(() => Contents = "Sports Drink");
            SetHotTeaCommand = new RelayCommand(() => Contents = "Hot Tea");
            SetIceTeaCommand = new RelayCommand(() => Contents = "Ice Tea");

            NewFluid = new Models.Fluid();
            NewFluid.Id = Guid.NewGuid().ToString();
            NewFluid.Contents = "Water";
            NewFluid.Ounces = 12;
        }

        public Models.Fluid NewFluid
        {
            get; private set;
        }

        public async void Save()
        {
            var success = await PerformNetworkAction(async () =>
             {
                 NewFluid.PercentDrank = 0;
                 NewFluid.UserId = DataContext.CurrentUser.Id;
                 NewFluid.Start = DateTime.Now;
                 NewFluid.End = DateTime.MinValue;
                 NewFluid.Status = "Drinking";

                 DataContext.CurrentFluid = NewFluid;
                 await DataContext.AddFluid(DataContext.CurrentFluid);
                 var currentFluidJSON = JsonConvert.SerializeObject(DataContext.CurrentFluid);
                 await DeviceServices.PlatformServices.Current.PutKVPValueAsync("CURRENT_FLUID", currentFluidJSON);                 
             });

            if(success)
                Navigation.GoBack();
        }

        public void Cancel()
        {
            DataContext.CurrentFluid = null;
            Navigation.GoBack();
        }

        public int Ounces
        {
            get { return NewFluid.Ounces; }
            set
            {
                NewFluid.Ounces = value;
                RaisePropertyChanged(() => Ounces);
            }
        }

        public String Contents
        {
            get { return NewFluid.Contents; }
            set
            {
                NewFluid.Contents = value;
                RaisePropertyChanged(() => Contents);
            }
        }

        public RelayCommand SetSmallCommand { get; private set; }
        public RelayCommand SetMediumCommand { get; private set; }
        public RelayCommand SetLargeCommand { get; private set; }

        public RelayCommand SetWaterCommand { get; private set; }
        public RelayCommand SetBlendedCommand { get; private set; }
        public RelayCommand SetSportsDrinkCommand { get; private set; }
        public RelayCommand SetHotTeaCommand { get; private set; }
        public RelayCommand SetIceTeaCommand { get; private set; }

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
    }
}
